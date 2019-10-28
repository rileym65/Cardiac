using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Cardiac
{
    public partial class Form1 : Form
    {
        protected TextBox[] textBoxes;
        protected List<TextBox> outputCardTextBoxes;
        protected List<TextBox> inputCardTextBoxes;
        protected RadioButton[] radioButtons;
        protected Boolean changing;
        protected List<int> InputCards;
        protected List<int> OutputCards;
        protected Cpu cpu;
        protected int CardPosition;
        protected Boolean noCheck;
        protected Boolean running;

        public Form1()
        {
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            InitializeComponent();
            textBoxes = new TextBox[100];
            radioButtons = new RadioButton[100];
            outputCardTextBoxes = new List<TextBox>();
            inputCardTextBoxes = new List<TextBox>();
            InputCards = new List<int>();
            OutputCards = new List<int>();
            ClassicMode.Checked = true;
            RunButton.Visible = false;
            StepButton.Visible = false;
            SpeedGroup.Visible = false;
            mediumRadioButton.Checked = true;
            AssembleToCardStrip.Checked = true;
            noCheck = false;
            running = false;
            cpu = new Cpu(this);
            setupForm();
            UpdateFields();
        }

        protected void setupForm()
        {
            TextBox tb;
            Label lb;
            RadioButton rb;
            int x;
            int y;
            x = 750;
            y = 35;
            for (var i = 0; i < 100; i++)
            {
                tb = new TextBox();
                tb.Width = 32;
                tb.Height = 20;
                tb.Left = x;
                tb.Top = y;
                tb.Visible = true;
                tb.Tag = i.ToString();
                tb.Text = (i == 0) ? "001" : "";
                if (i == 0) tb.ReadOnly = true;
                tb.TextChanged += new EventHandler(MemoryChangedHandler);
                if (i == 99) tb.Text = "800";
                tb.TextAlign = HorizontalAlignment.Center;
                textBoxes[i] = tb;
                tabPage1.Controls.Add(tb);
                rb = new RadioButton();
                rb.Width = 14;
                rb.Height = 13;
                rb.Left = x - 20;
                rb.Top = y+3;
                rb.Text = "";
                rb.CheckedChanged += new EventHandler(BugMoved);
                rb.Tag = i.ToString();
                if (i == 0) rb.Checked = true;
                radioButtons[i] = rb;
                tabPage1.Controls.Add(rb);
                lb = new Label();
                lb.Text = i.ToString("d2");
                lb.Width = 50;
                lb.Left = x - 40;
                lb.Top = y+2;
                lb.ForeColor = Color.FromArgb(0, 138, 138);
                tabPage1.Controls.Add(lb);
                y += 26;
                if (y > 475)
                {
                    y = 35;
                    x += 80;
                }
                AccumulatorTestWindow.Image = Images144x35.Images[0];
            }
            outputCardTextBoxes.Add(OutputCard1);
            outputCardTextBoxes.Add(OutputCard2);
            outputCardTextBoxes.Add(OutputCard3);
            outputCardTextBoxes.Add(OutputCard4);
            outputCardTextBoxes.Add(OutputCard5);
            outputCardTextBoxes.Add(OutputCard6);
            outputCardTextBoxes.Add(OutputCard7);
            outputCardTextBoxes.Add(OutputCard8);
            outputCardTextBoxes.Add(OutputCard9);
            outputCardTextBoxes.Add(OutputCard10);
            outputCardTextBoxes.Add(OutputCard11);
            outputCardTextBoxes.Add(OutputCard12);
            inputCardTextBoxes.Add(InputCard1);
            inputCardTextBoxes.Add(InputCard2);
            inputCardTextBoxes.Add(InputCard3);
            inputCardTextBoxes.Add(InputCard4);
            inputCardTextBoxes.Add(InputCard5);
            inputCardTextBoxes.Add(InputCard6);
            inputCardTextBoxes.Add(InputCard7);
        }

        public void BugMoved(Object sender, EventArgs args)
        {
            int tag;
            if (((RadioButton)sender).Checked)
            {
                tag = Convert.ToInt32(((RadioButton)sender).Tag);
                cpu.SetPC(tag);
            }
        }

        public String ValidateMemoryContents(String input)
        {
            String ret;
            int count;
            ret = "";
            if (input.StartsWith("-"))
            {
                ret = "-";
                input = input.Substring(1);
            }
            while (input.Length > 0)
            {
                if (input[0] >= '0' && input[0] <= '9') ret += input[0];
                input = input.Substring(1);
            }
            if (ret.Length > 4 && ret[0] == '-')
            {
                ret = "-" + ret.Substring(ret.Length - 3);
            }
            else if (ret.Length > 3 && ret[0] != '-')
            {
                ret = ret.Substring(ret.Length - 3);
            }
            return ret;
        }

        public String ValidateMemory99Contents(String input)
        {
            char key;
            String ret;
            int count;
            try
            {
                key = input[input.Length - 1];
                if (key < '0' || key > '9') return input.Substring(0, 3);
                ret = "8" + input.Substring(2, 2);
            }
            catch
            {
                ret = "800";
            }
            return ret;
        }

        public void MemoryChangedHandler(Object sender, EventArgs args)
        {
            int tag;
            int value;
            String text;
            TextBox tb;
            if (changing) return;
            if (noCheck) return;
            changing = true;
            tb = (TextBox)sender;
            tag = Convert.ToInt32(tb.Tag);
            if (tag == 0)
            {
                tb.Text = "001";
                return;
            }
            text = (tag != 99) ? ValidateMemoryContents(tb.Text) : ValidateMemory99Contents(tb.Text);
            tb.Clear();
            tb.AppendText(text);
            try
            {
                value = Convert.ToInt32(tb.Text);
            }
            catch
            {
                value = 0;
            }
            changing = false;
        }

        public void UpdateFields()
        {
            int inst;
            int i, hi, lo;
            inst = cpu.GetInst();
            i = inst / 100;
            inst = inst % 100;
            Instruction1.Text = i.ToString();
            hi = inst / 10;
            lo = inst % 10;
            Instruction2.Text = hi.ToString();
            Instruction3.Text = lo.ToString();
//            AccumulatorSign.Text = cpu.AccumulatorSign.ToString();
            switch (i)
            {
                case 0: 
                    InstructionDecode.Text = "COPY INPUT CARD INTO CELL " + hi.ToString() + lo.ToString() + ", ADVANCE CARD";
                    AccumulatorTestWindow.Image = Images144x35.Images[0];
                    break;
                case 1:
                    InstructionDecode.Text = " ERASE ACCUMULATOR\r\n COPY CONTENTS OF CELL " + hi.ToString() + lo.ToString() + " INTO ACCUMULATOR";
                    AccumulatorTestWindow.Image = Images144x35.Images[2];
                    break;
                case 2:
                    InstructionDecode.Text = "ADD CONTENTS OF CELL " + hi.ToString() + lo.ToString() + " INTO ACCUMULATOR";
                    AccumulatorTestWindow.Image = Images144x35.Images[2];
                    break;
                case 3:
                    InstructionDecode.Text = "MOVE BUG TO CELL " + hi.ToString() + lo.ToString();
                    AccumulatorTestWindow.Image = (AccumulatorSign.Text.Equals("-")) ? Images144x35.Images[2] : Images144x35.Images[1];
                    break;
                case 4:
                    InstructionDecode.Text = "SHIFT ACCUMULATOR LEFT " + hi.ToString() + " PLACES ...\r\nTHEN RIGHT " + lo.ToString() + " PLACES";
                    AccumulatorTestWindow.Image = Images144x35.Images[2];
                    break;
                case 5:
                    InstructionDecode.Text = "COPY CONTENTS OF CELL " + hi.ToString() + lo.ToString() + " TO OUTPUT CARD AND ADVANCE CARD";
                    AccumulatorTestWindow.Image = Images144x35.Images[2];
                    break;
                case 6:
                    InstructionDecode.Text = "COPY ACCUMULATOR INTO CELL " + hi.ToString() + lo.ToString();
                    AccumulatorTestWindow.Image = Images144x35.Images[2];
                    break;
                case 7:
                    InstructionDecode.Text = "SUBTRACT CONTENTS OF CELL " + hi.ToString() + lo.ToString() + " FROM ACCUMULATOR";
                    AccumulatorTestWindow.Image = Images144x35.Images[2];
                    break;
                case 8:
                    InstructionDecode.Text = "WRITE BUG'S CELL NO. IN CELL 99\r\nMOVE BUG TO CELL " + hi.ToString() + lo.ToString();
                    AccumulatorTestWindow.Image = Images144x35.Images[2];
                    break;
                case 9:
                    InstructionDecode.Text = "MOVE BUG TO CELL " + hi.ToString() + lo.ToString() + ", STOP";
                    AccumulatorTestWindow.Image = Images144x35.Images[2];
                    break;
            }
        }

        private void Inst1Up_Click(object sender, EventArgs e)
        {
            cpu.Inst1Up();
        }

        private void Inst1Down_Click(object sender, EventArgs e)
        {
            cpu.Inst1Down();
        }

        private void AccumulatorPlus_Click(object sender, EventArgs e)
        {
            AccumulatorSign.Text = "+";
            UpdateFields();
        }

        private void AccumulatorMinus_Click(object sender, EventArgs e)
        {
            AccumulatorSign.Text = "-";
            UpdateFields();
        }

        private void Acc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || e.KeyChar > '9') return;
            ((TextBox)sender).Text = e.KeyChar.ToString();
        }

        private void Inst2Up_Click(object sender, EventArgs e)
        {
            cpu.Inst2Up();
        }

        private void Inst2Down_Click(object sender, EventArgs e)
        {
            cpu.Inst2Down();
        }

        private void Inst3Up_Click(object sender, EventArgs e)
        {
            cpu.Inst3Up();
        }

        private void Inst3Down_Click(object sender, EventArgs e)
        {
            cpu.Inst3Down();
        }

        private void InputAdvance_Click(object sender, EventArgs e)
        {
            if (CardPosition < InputCards.Count)
            {
                CardPosition++;
                UpdateInputCards();
            }
        }

        private void doOutputCard()
        {
            int value;
            int pos;
            int card;
            try
            {
                value = Convert.ToInt32(OutputCard.Text);
            }
            catch
            {
                value = 0;
            }
            OutputCards.Add(value);
            OutputCard.Text = "";
            pos = OutputCards.Count - 1;
            card = 0;
            while (card < 12 && pos >= 0)
            {
                outputCardTextBoxes[card++].Text = OutputCards[pos--].ToString();
            }
        }

        private void OutputAdvance_Click(object sender, EventArgs e)
        {
            doOutputCard();
        }

        protected void UpdateInputCards()
        {
            int count;
            count = 0;
            InputCard.Text = "";
            for (var i = 0; i < 7; i++) inputCardTextBoxes[i].Text = "";
            if (CardPosition < InputCards.Count) InputCard.Text = InputCards[CardPosition].ToString("d3");
            while (CardPosition + 1 + count < InputCards.Count && count < 7)
            {
                inputCardTextBoxes[count].Text = InputCards[CardPosition + 1 + count].ToString("d3");
                count++;
            }
        }

        private void LoadReaderButton_Click(object sender, EventArgs e)
        {
            int value;
            InputCards.Clear();
            foreach (var line in InputCardsTextBox.Lines)
            {
                try
                {
                    value = Convert.ToInt32(line);
                }
                catch
                {
                    value = 0;
                }
                InputCards.Add(value);
            }
            CardPosition = 0;
            UpdateInputCards();
        }

        private void SaveCardsButton_Click(object sender, EventArgs e)
        {
            StreamWriter file;
            saveFileDialog1.Filter = "Card Files (*.crd)|*.crd|All Files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = new StreamWriter(saveFileDialog1.FileName);
                foreach (var card in InputCardsTextBox.Lines)
                {
                    file.WriteLine(card.ToString());
                }
                file.Close();
            }
        }

        private void LoadCardsButton_Click(object sender, EventArgs e)
        {
            StreamReader file;
            String line;
            openFileDialog1.Filter = "Card Files (*.crd)|*.crd|All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = new StreamReader(openFileDialog1.FileName);
                InputCardsTextBox.Clear();
                while (!file.EndOfStream)
                {
                    line = file.ReadLine();
                    InputCardsTextBox.AppendText(line + "\r\n");
                }
                file.Close();
            }
        }

        private void ClassicMode_CheckedChanged(object sender, EventArgs e)
        {
            if (ClassicMode.Checked)
            {
                StepButton.Visible = false;
                RunButton.Visible = false;
                SpeedGroup.Visible = false;
            }
            else
            {
                StepButton.Visible = true;
                RunButton.Visible = true;
                SpeedGroup.Visible = true;
            }
        }

        public int ReadMemory(int address)
        {
            int value;
            if (address < 0 || address > 99) return 900;
            try
            {
                value = Convert.ToInt32(textBoxes[address].Text);
            }
            catch
            {
                value = 0;
            }
            return value;
        }

        public void WriteMemory(int address, int value)
        {
            if (address < 1 || address > 99) return;
            noCheck = true;
            value = value % 1000;
            textBoxes[address].Text = value.ToString("d3");
            noCheck = false;
        }

        public int ReadCard()
        {
            int value;
            if (CardPosition < InputCards.Count)
            {
                try
                {
                    value = Convert.ToInt32(InputCard.Text);
                }
                catch
                {
                    value = -9999;
                }
                CardPosition++;
                UpdateInputCards();
            }
            else value = -9999;
            return value;
        }

        public void WriteCard(int value)
        {
            OutputCard.Text = value.ToString("d3");
            doOutputCard();
        }

        public void UpdatePC(int address)
        {
            if (address < 0 || address > 99) return;
            radioButtons[address].Checked = true;
        }

        public void WriteAccumulator(int value)
        {
            int v, h, m, l;
            AccumulatorSign.Text = (value >= 0) ? "+" : "-";
            if (value < 0) value = -value;
            v = value / 1000;
            value -= (v * 1000);
            h = value / 100;
            m = value % 100;
            l = m % 10;
            m = m / 10;
            Acc0.Text = v.ToString();
            Acc1.Text = h.ToString();
            Acc2.Text = m.ToString();
            Acc3.Text = l.ToString();
        }

        public int ReadAccumulator()
        {
            int v, h, m, l;
            try
            {
                v = Convert.ToInt32(Acc0.Text);
            }
            catch
            {
                v = 0;
            }
            try
            {
                h = Convert.ToInt32(Acc1.Text);
            }
            catch
            {
                h = 0;
            }
            try
            {
                m = Convert.ToInt32(Acc2.Text);
            }
            catch
            {
                m = 0;
            }
            try
            {
                l = Convert.ToInt32(Acc3.Text);
            }
            catch
            {
                l = 0;
            }
            h = (v * 1000) + (h * 100) + (m * 10) + l;
            if (AccumulatorSign.Text.Equals("-")) h = -h;
            return h;
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            cpu.Step();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            if (running)
            {
                RunButton.Text = "Run";
                running = false;
                timer1.Enabled = false;
            }
            else
            {
                RunButton.Text = "Stop";
                running = true;
                timer1.Enabled = true;
                cpu.Run();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!running) return;
            if (cpu.Halted) return;
            cpu.Step();
            if (cpu.Halted)
            {
                RunButton.Text = "Run";
                running = false;
                timer1.Enabled = false;
            }
        }

        private void ClearInputButton_Click(object sender, EventArgs e)
        {
            InputCardsTextBox.Clear();
        }

        private void SlowRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SlowRadioButton.Checked) timer1.Interval = 1000;
            if (mediumRadioButton.Checked) timer1.Interval = 100;
            if (FastRadioButton.Checked) timer1.Interval = 10;
        }

        private void RetrieveCardsButton_Click(object sender, EventArgs e)
        {
            OutputCardsTextBox.Clear();
            foreach (var entry in OutputCards)
            {
                OutputCardsTextBox.AppendText(entry + "\r\n");
            }
        }

        private void AssembleButton_Click(object sender, EventArgs e)
        {
            Assembler assembler;
            assembler = new Assembler(this);
            assembler.OutputMode = (AssembleToCardStrip.Checked) ? 'C' : 'M';
            AssemblyResults.Text = assembler.Assemble(AssemblySource.Lines);
            if (AssembleToCardStrip.Checked)
            {
                InputCardsTextBox.Clear();
                foreach (var line in assembler.CardList)
                    InputCardsTextBox.AppendText(line + "\r\n");
            }
        }

        private void SaveSourceButton_Click(object sender, EventArgs e)
        {
            StreamWriter file;
            saveFileDialog1.Filter = "Assembly Files (*.asm)|*.asm|All Files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = new StreamWriter(saveFileDialog1.FileName);
                foreach (var line in AssemblySource.Lines) file.WriteLine(line);
                file.Close();
            }
        }

        private void LoadSourceButton_Click(object sender, EventArgs e)
        {
            StreamReader file;
            openFileDialog1.Filter = "Assembly Files (*.asm)|*.asm|All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = new StreamReader(openFileDialog1.FileName);
                AssemblySource.Clear();
                while (!file.EndOfStream)
                {
                    AssemblySource.AppendText(file.ReadLine() + "\r\n");
                }
                file.Close();
            }
        }

        private void AssemblerNewButton_Click(object sender, EventArgs e)
        {
            AssemblySource.Clear();
        }

        private void SaveOuputCardsButton_Click(object sender, EventArgs e)
        {
            StreamWriter file;
            saveFileDialog1.Filter = "Card Files (*.crd)|*.crd|All Files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = new StreamWriter(saveFileDialog1.FileName);
                foreach (var card in OutputCardsTextBox.Lines)
                {
                    file.WriteLine(card.ToString());
                }
                file.Close();
            }
        }

        private void ClearOutputCardsButton_Click(object sender, EventArgs e)
        {
            OutputCardsTextBox.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (var i = 1; i < 99; i++) textBoxes[i].Text = "";
            textBoxes[99].Text = "800";
        }

        private void ClearOutputButton_Click(object sender, EventArgs e)
        {
            OutputCards.Clear();
            foreach (var entry in outputCardTextBoxes) entry.Clear();
        }

    }
}
