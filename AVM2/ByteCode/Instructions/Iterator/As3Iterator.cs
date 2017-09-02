namespace SwfDec.AVM2.ByteCode.Instructions.Iterator
{
    public interface IIterator
    {
    }

    public class As3NextValue : As3Instruction, IIterator
    {
        public override Opcode Opcode => Opcode.NextValue;

        public static new string Name => "as3_nextvalue";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3NextName : As3Instruction, IIterator
    {
        public override Opcode Opcode => Opcode.NextName;

        public static new string Name => "as3_nextname";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3HasNext : As3Instruction, IIterator
    {
        public override Opcode Opcode => Opcode.HasNext;

        public static new string Name => "as3_hasnext";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3HasNext2 : As3Instruction, IIterator
    {
        public override Opcode Opcode => Opcode.HasNext2;

        public static new string Name => "as3_hasnext2";

        public uint ObjectRegisterIndex { get; set; }
        public uint IndexRegisterIndex { get; set; }

        public As3HasNext2(uint objectRegisterIndex, uint indexRegisterIndex)
        {
            ObjectRegisterIndex = objectRegisterIndex;
            IndexRegisterIndex = indexRegisterIndex;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(ObjectRegisterIndex);
            stream.WriteU30(IndexRegisterIndex);
            return stream;
        }

        public override string ToString()
        {
            return Name + $"{ObjectRegisterIndex}, {IndexRegisterIndex}";
        }

        
    }
}
