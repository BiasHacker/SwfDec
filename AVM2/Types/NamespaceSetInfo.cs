using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types
{
    public class NamespaceSetInfo : IBrowseable
    {
        public uint Index { get; set; }

        public NamespaceInfo[] NamespaceArray { get; set; }

        public NamespaceSetInfo(uint index)
        {
            Index = index;
        }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
