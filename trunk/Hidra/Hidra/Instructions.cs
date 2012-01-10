using System.Collections.Generic;

namespace Hidra
{
    public class Instructions
    {
        Dictionary<int, string> instructions;

        public Instructions()
        {
            this.instructions = new Dictionary<int,string>();
        }

        private void initBasicInstructions()
        {
            this.instructions.Add(0, "NOP");
            this.instructions.Add(48, "ADD");
            this.instructions.Add(64, "OR");
            this.instructions.Add(80, "AND");
            this.instructions.Add(96, "NOT");
            this.instructions.Add(128, "JMP");
            this.instructions.Add(144, "JN");
            this.instructions.Add(160, "JZ");
            this.instructions.Add(240, "HLT");
        }

        public void initNeanderInstructions()
        {
            initBasicInstructions();

            this.instructions.Add(16, "STA");
            this.instructions.Add(32, "LDA");            
        }

        private void initAhmesRamsesCommumInstr()
        {
            this.instructions.Add(112, "SUB");
            this.instructions.Add(176, "JC");
            this.instructions.Add(224, "SHR");
        }


        public void initAhmesInstructions()
        {
            initNeanderInstructions();

            this.instructions.Add(148, "JP");
            this.instructions.Add(152, "JV");
            this.instructions.Add(156, "JNV");
            this.instructions.Add(164, "JNZ");
            this.instructions.Add(180, "JNC");
            this.instructions.Add(184, "JB");
            this.instructions.Add(188, "JNB");
            this.instructions.Add(225, "SHL");
            this.instructions.Add(226, "ROR");
            this.instructions.Add(227, "ROL");

            initAhmesRamsesCommumInstr();
        }


        public void initRamsesInstructions()
        {
            initBasicInstructions();

            this.instructions.Add(16, "STR");
            this.instructions.Add(32, "LDR");
            this.instructions.Add(180, "JSR");
            this.instructions.Add(208, "NEG");

            initAhmesRamsesCommumInstr();
        }

        public string getInstructionCode(int value)
        {
            if (this.instructions.ContainsKey(value))
                return this.instructions[value];
            else
                return value.ToString();
        }
    }
}
