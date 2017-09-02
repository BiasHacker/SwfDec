using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfDec.AVM2.Types.Multinames;

namespace SwfDec.AVM2.ByteCode.Instructions.Type
{
    public interface IType
    {
    }

    //        AddValueInfo(Opcode.TypeOf);

    public class As3ApplyType : As3Instruction, IType
    {
        public override Opcode Opcode => Opcode.ApplyType;

        public static new String Name => "as3_applytype";

        public uint ArgCount { get; set; }

        public As3ApplyType(uint argCount)
        {
            ArgCount = argCount;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(ArgCount);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {ArgCount}";
        }

        
    }

    public class As3AsType : As3Instruction, IType
    {
        public override Opcode Opcode => Opcode.AsType;

        public static new String Name => "as3_astype";

        public MultinameInfo Multiname { get; set; }

        public As3AsType(MultinameInfo multiname)
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

    public class As3AsTypeLate : As3Instruction, IType
    {
        public override Opcode Opcode => Opcode.AsTypeLate;

        public static new string Name => "as3_typelate";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3TypeOf : As3Instruction, IType
    {
        public override Opcode Opcode => Opcode.TypeOf;

        public static new string Name => "as3_typeof";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3CheckFilter : As3Instruction, IType
    {
        public override Opcode Opcode => Opcode.CheckFilter;

        public static new string Name => "as3_checkfilter";

        public override string ToString()
        {
            return Name;
        }

        
    }
}
