using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cardiac
{
    public class Cpu
    {
        protected int pc;
        protected Form1 mainForm;
        protected int inst;
        public Boolean Halted { get; protected set; }

        public Cpu(Form1 mf)
        {
            mainForm = mf;
            pc = 0;
            inst = 0;
            Halted = true;
        }

        public void Run()
        {
            Halted = false;
        }

        public void SetPC(int address)
        {
            if (address < 0 || address > 99) return;
            pc = address;
        }

        public int GetPC()
        {
            return pc;
        }

        public int GetInst()
        {
            return inst;
        }

        public void Inst1Up()
        {
            int hi, lo;
            hi = inst / 100;
            lo = inst % 100;
            if (++hi > 9) hi = 9;
            inst = hi * 100 + lo;
            mainForm.UpdateFields();
        }

        public void Inst1Down()
        {
            int hi, lo;
            hi = inst / 100;
            lo = inst % 100;
            if (--hi < 0) hi = 0;
            inst = hi * 100 + lo;
            mainForm.UpdateFields();
        }

        public void Inst2Up()
        {
            int h, m, l;
            h = inst / 100;
            m = inst % 100;
            l = m % 10;
            m = m / 10;
            if (++m > 9) m = 9;
            inst = h * 100 + m * 10 + l;
            mainForm.UpdateFields();
        }

        public void Inst2Down()
        {
            int h, m, l;
            h = inst / 100;
            m = inst % 100;
            l = m % 10;
            m = m / 10;
            if (--m < 0) m = 0;
            inst = h * 100 + m * 10 + l;
            mainForm.UpdateFields();
        }

        public void Inst3Up()
        {
            int h, m, l;
            h = inst / 100;
            m = inst % 100;
            l = m % 10;
            m = m / 10;
            if (++l > 9) l = 9;
            inst = h * 100 + m * 10 + l;
            mainForm.UpdateFields();
        }

        public void Inst3Down()
        {
            int h, m, l;
            h = inst / 100;
            m = inst % 100;
            l = m % 10;
            m = m / 10;
            if (--l < 0) l = 0;
            inst = h * 100 + m * 10 + l;
            mainForm.UpdateFields();
        }

        public void Step()
        {
            int address;
            int value;
            int m, l;
            Halted = false;
            inst = mainForm.ReadMemory(pc);
            mainForm.UpdateFields();
            if (++pc > 99) pc = 0;
            mainForm.UpdatePC(pc);
            address = inst % 100;
            if (inst < -999)
            {
                Halted = true;
                return;
            }
            switch (inst / 100)
            {
                case 0:
                    value = mainForm.ReadCard();
                    if (value < -1000)
                    {
                        Halted = true;
                        pc = 0;
                        mainForm.UpdatePC(pc);
                        return;
                    }
                    mainForm.WriteMemory(address, value % 1000);
                    break;
                case 1:
                    value = mainForm.ReadMemory(address);
                    mainForm.WriteAccumulator(value);
                    break;
                case 2:
                    value = mainForm.ReadAccumulator();
                    value += mainForm.ReadMemory(address);
                    value = value % 1000;
                    mainForm.WriteAccumulator(value);
                    break;
                case 3:
                    value = mainForm.ReadAccumulator();
                    if (value < 0)
                    {
                        pc = address;
                        mainForm.UpdatePC(pc);
                    }
                    break;
                case 4:
                    m = address / 10;
                    l = address % 10;
                    value = mainForm.ReadAccumulator();
                    for (var i = 0; i < m; i++) value = (value * 10) % 10000;
                    for (var i = 0; i < l; i++) value /= 10;
                    value = value % 10000;
                    mainForm.WriteAccumulator(value);
                    break;
                case 5:
                    value = mainForm.ReadMemory(address);
                    mainForm.WriteCard(value);
                    break;
                case 6:
                    value = mainForm.ReadAccumulator();
                    mainForm.WriteMemory(address, value);
                    break;
                case 7:
                    value = mainForm.ReadAccumulator();
                    value -= mainForm.ReadMemory(address);
                    value = value % 1000;
                    mainForm.WriteAccumulator(value);
                    break;
                case 8:
                    mainForm.WriteMemory(99, 800 + pc);
                    pc = address;
                    mainForm.UpdatePC(pc);
                    break;
                case 9:
                    Halted = true;
                    pc = address;
                    mainForm.UpdatePC(pc);
                    break;
            }
        }
    }
}
