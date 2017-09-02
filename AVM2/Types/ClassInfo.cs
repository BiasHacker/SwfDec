using System;
using System.Collections.Generic;
using SwfDec.AVM2.Types.Extended;
using SwfDec.AVM2.Types.Traits;

namespace SwfDec.AVM2.Types
{
    public class ClassInfo : IBrowseable
    {
        public uint Index { get; private set; }

        public MethodInfo StaticInitializer { get; set; }
        public TraitsArray TraitsArray { get; set; }

        public ClassInfo(uint index)
        {
            Index = index;
        }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }

        public override string ToString()
        {
            return $"ClassInfo:{Index}{{}}";
        }
    }
}
