using SwfDec.AVM2.Types.Multinames;

namespace SwfDec.AVM2.ByteCode.Instructions.Set
{
    public interface ISet
    {
    }

    public class As3SetSlot : As3Instruction, ISet
    {
        public override Opcode Opcode => Opcode.SetSlot;

        public static new string Name => "as3_setslot";

        public uint SlotIndex { get; set; }

        public As3SetSlot(uint slotIndex)
        {
            SlotIndex = slotIndex;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(SlotIndex);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {SlotIndex}";
        }
    }

    public class As3SetProperty : As3Instruction, ISet
    {
        public override Opcode Opcode => Opcode.SetProperty;

        public static new string Name => "as3_setproperty";

        public MultinameInfo Multiname { get; set; }

        public As3SetProperty(MultinameInfo multiname)
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

    public class As3SetSuper : As3Instruction, ISet
    {
        public override Opcode Opcode => Opcode.SetSuper;

        public static new string Name => "as3_setsuper";

        public MultinameInfo Multiname { get; set; }

        public As3SetSuper(MultinameInfo multiname)
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

    public interface IInit
    {
    }

    public class As3InitProperty : As3Instruction, IInit
    {
        public override Opcode Opcode => Opcode.InitProperty;

        public static new string Name => "as3_initproperty";

        public MultinameInfo Multiname { get; set; }

        public As3InitProperty(MultinameInfo multiname)
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
}
