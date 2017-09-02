using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types.Multinames
{
    public class MKRTQNameL : MultinameData, IBrowseable
    {
        public MKRTQNameL()
        {
        }

        public MKRTQNameL(ConstantPoolInfo cPool)
            :base(cPool)
        {    
        }

        public new object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
