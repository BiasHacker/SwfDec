using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types
{
    public class MetadataInfo : IBrowseable
    {
        public uint Name { get; set; }
        public StringInfo[] KeyArray { get; set; }
        public StringInfo[] ValueArray { get; set; }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
