using System;
using System.Linq;
using SwfDec.AVM2.Types.Extended;
using SwfDec.AVM2.Types.Multinames;

namespace SwfDec.AVM2.Types
{
    [Flags]
    public enum MethodFlags
    {
        NEED_ARGUMENTS = 0x01,
        NEED_ACTIVATION = 0x02,
        NEED_REST = 0x04,
        HAS_OPTIONAL = 0x08,
        SET_DXNS = 0x40,
        HAS_PARAM_NAMES = 0x80

    }

    public enum ValueKind : byte
    {
        Int = 0x03,
        UInt = 0x04,
        Double = 0x06,
        Utf8 = 0x01,
        True = 0x0B,
        False = 0x0A,
        Null = 0x0C,
        Undefined = 0x00,
        Namespace = 0x08,
        PackageNamespace = 0x16,
        PackageInternalNs = 0x17,
        ProtectedNamespace = 0x18,
        ExplicitNamespace = 0x19,
        StaticProtectedNs = 0x1A,
        PrivateNs = 0x05
    }

    public class MethodInfo : IBrowseable
    {
        public uint Index { get; private set; }

        public MultinameInfo ReturnType { get; set; }
        public MultinameInfo[] ParamTypeArray { get; set; }
        public StringInfo Name { get; set; }
        public MethodFlags Flags { get; set; }
        public OptionalParam[] OptionalParamArray { get; set; }
        public uint[] ParamNameArray { get; set; }

        public MethodBodyInfo MethodBody { get; set; }

        public MethodInfo(uint index)
        {
            Index = index;
        }

        public String GetReturnTypeName()
        {
            return (ReturnType == null ? "*" : ReturnType.MKQName.Name);
        }

        public override string ToString()
        {
            return
                $"{{{string.Join(", ", from n in ParamTypeArray select n.MKQName.Name)}, R:{{{GetReturnTypeName()}}}}}";
        }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }

    public class OptionalParam : IBrowseable
    {
        public uint Value { get; set; }
        public ValueKind Kind { get; set; }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}