namespace SwfDec.AVM2.ByteCode.Instructions.Debug
{
    // 用途が不明なためハリボテ実装

    class As3DebugFile : As3Instruction
    {
        public override Opcode Opcode => Opcode.DebugLine;

        public As3DebugFile(uint obj)
        {

        }

        public override string ToString()
        {
            return "";
        }
    }
}
