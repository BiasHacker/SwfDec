using System;
using System.Collections.Generic;
using SwfDec.AVM2.Types.Extended;
using SwfDec.AVM2.Types.Multinames;
using SwfDec.AVM2.Types.Traits;

namespace SwfDec.AVM2.Types
{
    [Flags]
    public enum InstanceFlags
    {
        Sealed = 0x01,
        Final = 0x02,
        Interface = 0x04,
        ProtectedNs = 0x08
    }

    public class InstanceInfo : IBrowseable
    {
        public uint Index { get; private set; }

        public MultinameInfo Name { get; set; }
        public MultinameInfo Supername { get; set; }
        public InstanceFlags Flags { get; set; }
        public NamespaceInfo ProtectedNamespace { get; set; }
        public uint[] InterfaceArray { get; set; }
        public MethodInfo InstanceInitializer { get; set; }
        public TraitsArray TraitsArray { get; set; }

        public InstanceInfo(uint index)
        {
            Index = index;
            Flags |= InstanceFlags.Final;
        }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }

        public override string ToString()
        {
            return $"InstanceInfo:{Index}{{}}";
        }
    }
}
