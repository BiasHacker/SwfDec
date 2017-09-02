using SwfDec.AVM2.Types.Multinames;

namespace SwfDec.AVM2.ByteCode.Instructions.Find
{
    public interface IFind
    {
    }

    //AddValueInfo(Opcode.FindProperty, MakeIns("U30"));
    //        AddValueInfo(Opcode.FindPropStrict, MakeIns("U30"));

    public class As3FindProperty : As3Instruction, IFind
    {
        public override Opcode Opcode => Opcode.FindProperty;

        public static new string Name => "as3_findproperty";

        public MultinameInfo Multiname { get; set; }

        public As3FindProperty(MultinameInfo multiname)
        {
            Multiname = multiname;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Multiname.Index);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Multiname}";
        }
    }

    public class As3FindPropStrict : As3Instruction, IFind
    {
        public override Opcode Opcode => Opcode.FindPropStrict;

        public static new string Name => "as3_findpropstrict";

        public MultinameInfo Multiname { get; set; }

        public As3FindPropStrict(MultinameInfo multiname)
        {
            Multiname = multiname;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Multiname.Index);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Multiname}";
        }
    }

    public class As3In : As3Instruction, IFind
    {
        public override Opcode Opcode => Opcode.In;

        public override string ToString()
        {
            return string.Format("as3_in");
        }
    }
}
