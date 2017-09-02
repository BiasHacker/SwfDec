namespace SwfDec.AVM2.ByteCode.Instructions.Slots
{
    public interface ISlot
    {
        uint SlotIndex { get; set; }
    }

    public class As3GetSlot : As3Instruction, ISlot
    {
        public override Opcode Opcode
        {
            get { return Opcode.GetSlot; }
        }

        public uint SlotIndex { get; set; }

        public As3GetSlot(uint slotIndex)
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
            return string.Format("as3_getslot {0}", SlotIndex);
        }
    }

    public class As3SetSlot : As3Instruction, ISlot
    {
        public override Opcode Opcode
        {
            get { return Opcode.SetSlot; }
        }

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
            return string.Format("as3_setslot {0}", SlotIndex);
        }
    }

    //AddValueInfo(Opcode.GetSlot, MakeIns("U30"));
    //        AddValueInfo(Opcode.SetSlot, MakeIns("U30"));


}
