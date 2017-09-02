using System;
using SwfDec.AVM2.Types.Extended;
using SwfDec.AVM2.Types.Traits;

namespace SwfDec.AVM2.Types
{
    public class ScriptInfo : IBrowseable
    {
        public uint Index { get; private set; }

        public uint Initializer { get; set; }
        public TraitsInfo[] TraitsArray { get; set; }

        public ScriptInfo(uint index)
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
