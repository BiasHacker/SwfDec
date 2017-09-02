using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types.Traits
{
    public class TraitBase : IBrowseable
    {
        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
