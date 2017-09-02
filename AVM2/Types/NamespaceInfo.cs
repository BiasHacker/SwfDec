using System;
using System.Text;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types
{
    public enum NamespaceKind : byte
    {
        None = 0,
        Namespace = 0x08,
        PackageNamespace = 0x16,
        PackageInternalNs = 0x17,
        ProtectedNamespace = 0x18,
        ExplicitNamespace = 0x19,
        StaticProtectedNs = 0x1A,
        PrivateNs = 0x05,
    }

    public class NamespaceInfo : IEquatable<NamespaceInfo>, IBrowseable
    {
        private ConstantPoolInfo _constantPool { get; set; }
        public uint Index { get; private set; }

        public NamespaceKind Kind { get; set; }
        public StringInfo Name { get; set; }

        public NamespaceInfo(ConstantPoolInfo constantPool, uint index)
        {
            _constantPool = constantPool;
            Index = index;
        }

		public NamespaceInfo(NamespaceKind kind, StringInfo name)
		{
			Kind = kind;
			Name = name;
		}

		public string GetKindModifiers()
        {
            switch (Kind)
            {
                case NamespaceKind.PackageNamespace:
                    return "public";

                case NamespaceKind.PrivateNs:
                    return "private";

                case NamespaceKind.StaticProtectedNs:
                    return "protected";

                case NamespaceKind.PackageInternalNs:
                    return "internal";
            }

            return Kind.ToString();
        }

	  

	    public override string ToString()
        {
            return $"NamespaceInfo:{Index}{{{Kind}, {Name}}}";
        } 

		public bool Equals(NamespaceInfo other)
		{
			return (other != null && other.Name.Equals(Name) && other.Kind == Kind);
		}

	    public override bool Equals(object obj)
	    {

		    return (Equals(obj as NamespaceInfo));
	    }

	    public override int GetHashCode()
	    {
		    return Name.GetHashCode() ^ Kind.GetHashCode();
	    }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
