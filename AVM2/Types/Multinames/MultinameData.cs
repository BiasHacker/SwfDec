using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types.Multinames
{
    public abstract class MultinameData : IBrowseable
    {
        protected readonly ConstantPoolInfo _cPool;

        public MultinameData()
        {
        }

        public MultinameData(ConstantPoolInfo cPool)
        {
            _cPool = cPool;
        }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
