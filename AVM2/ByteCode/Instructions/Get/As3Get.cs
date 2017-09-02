using SwfDec.AVM2.Types.Extended;
using SwfDec.AVM2.Types.Multinames;
using SwfDec.AVM2.Types.Traits;

namespace SwfDec.AVM2.ByteCode.Instructions.Get
{
    public interface IGet
    {
    }

    public class As3GetSlot : As3Instruction, IGet
    {
        public override Opcode Opcode => Opcode.GetSlot;

        public static new string Name => "as3_getslot";

        public uint SlotIndex { get; set; }

        public As3GetSlot(uint slotIndex)
        {
            SlotIndex = slotIndex;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(SlotIndex);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {SlotIndex}";
        }
    }

    public class As3GetProperty : As3Instruction, IGet
    {
        public override Opcode Opcode => Opcode.GetProperty;

        public static new string Name => "as3_getproperty";

        public MultinameInfo Multiname { get; set; }

        public As3GetProperty(MultinameInfo multiname)
        {
            Multiname = multiname;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Multiname.Index);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Multiname}";
        }
    }

    public class As3GetLex : As3Instruction, IGet
    {
        public override Opcode Opcode => Opcode.GetLex;

        public static new string Name => "as3_getlex";

        public MultinameInfo Multiname { get; set; }

        public As3GetLex(MultinameInfo multiname)
        {
            Multiname = multiname;
        }

        public AbcClass GetAbcClass(Abc abc)
        {
            return new AbcClass(abc, (uint)abc.FindClass(abc.ConstantPool.GetMultinameAt(Multiname.Index).MKQName.Name));
        }

        public TraitsInfo GetPropertyTrait(Abc abc, As3GetProperty as3GetProperty)
        {
            AbcClass abcClass = GetAbcClass(abc);
            return abcClass.GetTraitByName(abc.ConstantPool.GetMultinameAt(as3GetProperty.Multiname.Index).MKQName.Name);
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Multiname.Index);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Multiname}";
        }
    }

    public class As3GetSuper : As3Instruction, IGet
    {
        public override Opcode Opcode => Opcode.GetSuper;

        public static new string Name => "as3_getsuper";

        public MultinameInfo Multiname { get; set; }

        public As3GetSuper(MultinameInfo multiname)
        {
            Multiname = multiname;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Multiname.Index);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Multiname}";
        }
    }

    public class As3GetScopeObject : As3Instruction, IGet
    {
        public override Opcode Opcode => Opcode.GetScopeObject;

        public static new string Name => "as3_getscopeobject";

        public uint ObjectIndex { get; set; }

        public As3GetScopeObject(uint objectIndex)
        {
            ObjectIndex = objectIndex;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(ObjectIndex);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {ObjectIndex}";
        }
    }

    public class As3GetDescendants : As3Instruction, IGet
    {
        public override Opcode Opcode => Opcode.GetDescendants;

        public static new string Name => "as3_getdescendants";

        public MultinameInfo Multiname { get; set; }

        public As3GetDescendants(MultinameInfo multiname)
        {
            Multiname = multiname;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Multiname.Index);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Multiname}";
        }
    }

    public class As3GetGlobalScope : As3Instruction, IGet
    {
        public override Opcode Opcode => Opcode.GetGlobalScope;

        public static new string Name => "as3_getglobalscope";

        public override string ToString()
        {
            return Name;
        }
    }
}
