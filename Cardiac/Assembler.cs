using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cardiac
{
    public class Assembler
    {
        protected List<String> labels;
        protected List<int> values;
        public char OutputMode { get; set; }
        public String Results { get; protected set; }
        protected int pass;
        protected int address;
        protected Form1 mainForm;
        public List<String> CardList;
        protected int errors;
        protected int assemblyCount;
        protected Boolean ended;

        public Assembler(Form1 mf)
        {
            mainForm = mf;
            CardList = new List<String>();
        }

        protected int FindLabel(String label)
        {
            for (var i = 0; i < labels.Count; i++)
                if (labels[i].Equals(label)) return values[i];
            return -9999;
        }

        protected Boolean AddLabel(String label, int value)
        {
            if (FindLabel(label) > -9999) return false;
            labels.Add(label);
            values.Add(value);
            return true;
        }

        protected void setupLabels()
        {
            labels = new List<String>();
            values = new List<int>();
            AddLabel("INP",0);
            AddLabel("CLA", 100);
            AddLabel("ADD", 200);
            AddLabel("TAC", 300);
            AddLabel("SFT", 400);
            AddLabel("OUT", 500);
            AddLabel("STO", 600);
            AddLabel("SUB", 700);
            AddLabel("JMP", 800);
            AddLabel("HRS", 900);
            AddLabel("DATA", 0);
            AddLabel("END", -9001);
            AddLabel("RUN", -9001);
            AddLabel("ORG", -9002);
            AddLabel("EQU", -9003);
            AddLabel("HALT", -9004);
        }

        protected Boolean isNumber(String n)
        {
            if (n.Length > 0 && n[0] == '-')
            {
                n = n.Substring(1);
            }
            foreach (var c in n)
            {
                if (c < '0') return false;
                if (c > '9') return false;
            }
            return true;
        }

        protected List<String> parse(String line)
        {
            String token;
            List<String> ret;
            ret = new List<String>();
            if (line.Trim().Length < 1) return ret;
            line = line.ToUpper();
            if (line[0] == ' ' || line[0] == '\t') ret.Add("");
            token = "";
            while (line.Length > 0)
            {
                if (line[0] == ' ' || line[0] == '\t')
                {
                    if (token.Length > 0)
                    {
                        ret.Add(token);
                        token = "";
                    }
                }
                else if (line[0] == ';') line = " ";
                else token += line[0];
                line = line.Substring(1);
            }
            if (token.Length > 0) ret.Add(token);
            return ret;
        }

        protected void assembleLine(String line)
        {
            String src;
            String field;
            List<String> tokens;
            int pseudoCode;
            int opcode;
            int value;
            int pos;
            src = line;
            if (line.Length < 1)
            {
                if (pass == 2) Results += "        " + src + "\r\n";
                return;
            }
            tokens = parse(line);
            if (tokens.Count < 1)
            {
                if (pass == 2) Results += "        " + src + "\r\n";
                return;
            }
            assemblyCount++;
            opcode = 0;
            pseudoCode = 0;
            if (tokens[0].Length > 0)
            {
                if (pass == 1)
                {
                    if (AddLabel(tokens[0], address) == false)
                    {
                        Results += "ERROR: Label multiply defined: " + tokens[0] + "\r\n";
                        errors++;
                    }
                }
            }
            if (tokens.Count < 2)
            {
                if (pass == 2) Results += "        " + src + "\r\n";
                return;
            }
            pos = 1;
            while (pos < tokens.Count)
            {
                if (isNumber(tokens[pos]))
                {
                    opcode += Convert.ToInt32(tokens[pos]);
                }
                else
                {
                    value = FindLabel(tokens[pos]);
                    if (value == -9999 && pass == 2)
                    {
                        Results += "ERROR: Label not found: " + tokens[pos] + "\r\n";
                        errors++;
                    }
                    if (value < -9000 && value > -9999) pseudoCode = value;
                    if (value > -1000) opcode += value;
                }
                pos++;
            }
            if ((opcode > 999 || opcode < -999) && pass == 2)
            {
                Results += "ERROR: Opcode exceeds 3 digits: " + opcode.ToString() + "\r\n";
                errors++;
            }
            opcode = opcode % 1000;
            if (pseudoCode != 0)
            {
                if (pseudoCode == -9002) address = opcode;
                if (pseudoCode == -9001 && pass == 2 && OutputMode == 'C')
                {
                    CardList.Add("002");
                    CardList.Add((800 + (opcode % 100)).ToString("d3"));
                    ended = true;
                }
                if (pseudoCode == -9004 && pass == 2 && OutputMode == 'C')
                {
                    CardList.Add("002");
                    CardList.Add((900 + (opcode % 100)).ToString("d3"));
                    ended = true;
                }
                if (pseudoCode == -9003 && pass == 1) values[values.Count - 1] = opcode;
            }
            else
            {
                if (pass == 2)
                {
                    if (address < 0 || address > 99)
                    {
                        Results += "ERROR: Address exceeds memory boundaries: " + address.ToString() + "\r\n";
                        errors++;
                    }
                    if (OutputMode == 'M') mainForm.WriteMemory(address, opcode);
                    if (OutputMode == 'C')
                    {
                        if (ended)
                        {
                            CardList.Add(opcode.ToString("d3"));
                        }
                        else
                        {
                            CardList.Add(address.ToString("d3"));
                            CardList.Add(opcode.ToString("d3"));
                        }
                    }
                }
            }
            if (pass == 2) Results += address.ToString("d2") + " " + opcode.ToString("d3") + "  " + src + "\r\n";
            if (pseudoCode != 0) pseudoCode = 0;
            else address++;
        }

        public String Assemble(String[] lines)
        {
            CardList = new List<String>();
            Results = "";
            setupLabels();
            pass = 1;
            address = 0;
            errors = 0;
            assemblyCount = 0;
            ended = false;
            Results += "Pass 1:\r\n";
            foreach (var line in lines) assembleLine(line);
            pass = 2;
            ended = false;
            assemblyCount = 0;
            if (OutputMode == 'C')
            {
                CardList.Add("002");
                CardList.Add("800");
            }
            address = 0;
            Results += "\r\n";
            Results += "Pass 2:\r\n";
            foreach (var line in lines) assembleLine(line);
            Results += "\r\n";
            Results += "Errors         : " + errors.ToString() + "\r\n";
            Results += "Lines Assembled: " + assemblyCount.ToString() + "\r\n";
            return Results;
        }
    }
}
