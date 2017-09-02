using SwfDec.AVM2.Types.Multinames;

namespace SwfDec.AVM2.ByteCode.Instructions.Delete
{
    public interface IDelete
    {
    }

            //    AddValueInfo(Opcode.DeleteProperty, MakeIns("U30"));
            //AddValueInfo(Opcode.DeletePropertyLate);

    public class As3DeleteProperty : As3Instruction, IDelete
    {
        public override Opcode Opcode => Opcode.DeleteProperty;

        public static new string Name => "as3_deleteproperty";

        public MultinameInfo Multiname { get; set; }

        public As3DeleteProperty(MultinameInfo multiname)
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

    public class As3DeletePropertyLate : As3Instruction, IDelete
    {
        public override Opcode Opcode => Opcode.DeleteProperty;

        public static new string Name => "as3_deletepropertylate";

        public override string ToString()
        {
            return Name;
        }
    }
}
