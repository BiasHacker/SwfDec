using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types.Multinames
{
    public class MKMultiname : MultinameData, IBrowseable
    {
        public StringInfo Name { get; set; }
        public NamespaceSetInfo NamespaceSet { get; set; }

        public MKMultiname()
        {
        }

        public MKMultiname(ConstantPoolInfo cPool)
            :base(cPool)
        {
            Name = cPool.GetStringAt(1);
            NamespaceSet = cPool.GetNamespaceSetAt(1);
        }

        public new object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
