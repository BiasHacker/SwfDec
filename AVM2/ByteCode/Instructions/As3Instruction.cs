using System;

namespace SwfDec.AVM2.ByteCode.Instructions
{
    public abstract class As3Instruction
    {
        public abstract Opcode Opcode { get; }

        public static string Name
        {
            get
            {
                throw new Exception("NAME NOT IMPLEMENTED.");
            }
        }

        public int Length => ToBytes().Length;

        protected virtual byte[] InsBytes()
        {
            return new byte[0];
        }

        public byte[] ToBytes()
        {
           AbcStream stream = new AbcStream();
            stream.WriteByte((byte)Opcode);
            stream.WriteBytes(InsBytes());
            return stream;
        }

        public abstract override string ToString();
    }
}
