using System;
using SwfDec.AVM2.Types.Extended;
using SwfDec.AVM2.Types.Multinames;

namespace SwfDec.AVM2.Types.Traits
{
    [Flags]
    public enum TraitAttributes : byte
    {
        Final = 0x1,
        Override = 0x2,
        Metadata = 0x4
    }

    public enum TraitKind
    {
        Slot = 0,
        Method = 1,
        Getter = 2,
        Setter = 3,
        Class = 4,
        Function = 5,
        Const = 6
    }

    public class TraitsInfo : IBrowseable
    {
        public MultinameInfo Name { get; set; }
        public TraitAttributes Attributes { get; set; }
        public TraitKind Kind { get; set; }
        public TraitBase Trait { get; set; }
        public uint[] MetadataArray { get; set; }

        public TraitField TraitField => Trait as TraitField;
        public TraitMethod TraitMethod => Trait as TraitMethod;
        public  TraitClass TraitClass => Trait as TraitClass;

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
