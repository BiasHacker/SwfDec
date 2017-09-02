using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types.Traits
{
    public class TraitClass : TraitBase, IBrowseable
    {
        public uint SlotId { get; set; }
        public ClassInfo Class { get; set; }

        public new object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
