using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types.Multinames
{
    public class MKMultinameL : MultinameData, IBrowseable
    {
        public NamespaceSetInfo NamespaceSet { get; set; }

        public MKMultinameL()
        {
        }

        public MKMultinameL(ConstantPoolInfo cPool)
            :base(cPool)
        {
            NamespaceSet = cPool.GetNamespaceSetAt(1);
        }

        public new object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
