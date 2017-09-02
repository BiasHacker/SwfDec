using SwfDec.AVM2.Types.Multinames;

namespace SwfDec.AVM2.ByteCode.Instructions.Construct
{
    public interface IConstruct
    {
    }

    public class As3Construct : As3Instruction, IConstruct
    {
        public override Opcode Opcode => Opcode.Construct;

        public static new string Name => "as3_construct";

        public uint ArgCount { get; set; }

        public As3Construct(uint argCount)
        {
            ArgCount = argCount;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(ArgCount);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {ArgCount}";
        }

    }

    public class As3ConstructProp : As3Instruction, IConstruct
    {
        public override Opcode Opcode => Opcode.ConstructProp;

        public static new string Name => "as3_constructprop";

        public MultinameInfo Multiname { get; set; }
        public uint ArgCount { get; set; }

        public As3ConstructProp(MultinameInfo multiname, uint argCount)
        {
            Multiname = multiname;
            ArgCount = argCount;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Multiname.Index);
            stream.WriteU30(ArgCount);
            return stream;
        }

        public override string ToString()
        {
           return Name + string.Format(" {1}", Multiname, ArgCount);
        }
    }

    public class As3ConstructSuper : As3Instruction, IConstruct
    {
        public override Opcode Opcode => Opcode.ConstructSuper;

        public static new string Name => "as3_constructsuper";

        public uint ArgCount { get; set; }

        public As3ConstructSuper(uint argCount)
        {
            ArgCount = argCount;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(ArgCount);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {ArgCount}";
        }
    }
}
