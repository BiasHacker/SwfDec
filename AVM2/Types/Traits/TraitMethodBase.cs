using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types.Traits
{
    public class TraitMethodBase : TraitBase, IBrowseable
    {
        public uint DispId { get; set; }
        public MethodInfo Method { get; set; }

        public new object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }

    public class TraitMethod : TraitMethodBase, IBrowseable
    {
        public new object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }

    public class TraitGetter : TraitMethodBase, IBrowseable
    {
        public new object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }

    public class TraitSetter : TraitMethodBase, IBrowseable
    {
        public new object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
