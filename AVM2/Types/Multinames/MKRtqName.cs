using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types.Multinames
{
    public class MKRTQName : MultinameData, IBrowseable
    {
        public StringInfo Name { get; set; }

        public MKRTQName()
        {
        }

        public MKRTQName(ConstantPoolInfo cPool)
            :base(cPool)
        {
            Name = _cPool.GetStringAt(1);
        }

        public override string ToString()
        {
            return $"RTQName{{{Name}}}";
        }

        public new object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}