namespace SwfDec.AVM2.ByteCode.Instructions.Debug
{
    // 用途が不明なためハリボテ実装

    class As3DebugLine : As3Instruction
    {
        public override Opcode Opcode => Opcode.DebugLine;

        public As3DebugLine(uint obj)
        {

        }

        public override string ToString()
        {
            return "";
        }
    }
}
