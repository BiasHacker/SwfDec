using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types.Multinames
{
    public class MKQName : MultinameData , IBrowseable
    {
        public NamespaceInfo Namespace { get; set; }
        public StringInfo Name { get; set; }

        public MKQName()
        {
        }

        public MKQName(ConstantPoolInfo cPool)
            :base(cPool)
        {
            Namespace = _cPool.GetNamespaceAt(1);
            Name = _cPool.GetStringAt(1);
        }

        public override string ToString()
        {
            return $"QName{{{Namespace}, {Name}}}";
        }

        public new object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
