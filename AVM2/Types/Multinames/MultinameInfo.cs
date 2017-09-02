using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types.Multinames
{
    public enum MultinameKind
    {
        QName = 0x07,
        QNameA = 0x0D,
        RTQName = 0x0F,
        RTQNameA = 0x10,
        RTQNameL = 0x11,
        RTQNameLA = 0x12,
        Multiname = 0x09,
        MultinameA = 0x0E,
        MultinameL = 0x1B,
        MultinameLA = 0x1C,
        GenericName = 0x1D
    }

    public class MultinameInfo : IBrowseable
    {
        public uint Index { get; private set; }

        public MultinameKind Kind { get; set; }
        public MultinameData Data { get; set; }

        public MKQName MKQName => Data as MKQName;

        public MKRTQName MKRTQName => Data as MKRTQName;

        public MKRTQNameL MKRTQNameL => Data as MKRTQNameL;

        public MKMultiname MKMultiname => Data as MKMultiname;

        public MKMultinameL MKMultinameL => Data as MKMultinameL;

        public MKGenericName MKGenericName => Data as MKGenericName;

        public MultinameInfo(uint index)
        {
            Index = index;
        }

        public override string ToString()
        {
            return $"MultinameInfo:{Index}{{{Kind}, {Data}}}";
        }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
