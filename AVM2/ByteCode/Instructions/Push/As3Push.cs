using SwfDec.AVM2.Types;
namespace SwfDec.AVM2.ByteCode.Instructions.Push
{
    public interface IPush
    {
        object GetValue(ConstantPoolInfo cPool);
    }

    public class As3PushScope : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushScope;

        public static new string Name => "as3_pushscope";

        public object GetValue(ConstantPoolInfo cPool)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3PushTrue : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushTrue;

        public static new string Name => "as3_pushtrue";

        public object GetValue(ConstantPoolInfo cPool = null)
        {
            return true;
        }

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3PushFalse : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushFalse;

        public static new string Name => "as3_pushfalse";

        public object GetValue(ConstantPoolInfo cPool = null)
        {
            return false;
        }

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3PushNull : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushNull;

        public static new string Name => "as3_pushnull";

        public object GetValue(ConstantPoolInfo cPool = null)
        {
            return null;
        }

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3PushUndefined : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushUndefined;

        public static new string Name => "as3_pushundefined";

        public object GetValue(ConstantPoolInfo cPool = null)
        {
            return "undefined";
        }

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3PushNan : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushNan;


        public static new string Name => "as3_pushnan";

        public object GetValue(ConstantPoolInfo cPool = null)
        {
            return double.NaN;
        }

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3PushWith : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushWith;

        public static new string Name => "as3_pushwith";

        public object GetValue(ConstantPoolInfo cPool)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3PushString : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushString;

        public static new string Name => "as3_pushstring";

        public StringInfo String { get; set; }

        public As3PushString(StringInfo @string)
        {
            String = @string;
        }

        public object GetValue(ConstantPoolInfo cPool)
        {
            return String.String;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(String.Index);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {String}";
        }

        
    }

    public class As3PushDouble : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushDouble;

        public static new string Name => "as3_pushdouble";

        public uint DoubleIndex { get; set; }

        public As3PushDouble(uint doubleIndex)
        {
            DoubleIndex = doubleIndex;
        }

        public object GetValue(ConstantPoolInfo cPool)
        {
            return cPool.GetDoubleAt(DoubleIndex);
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(DoubleIndex);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {DoubleIndex}";
        }

        
    }

    public class As3PushInt : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushInt;

        public static new string Name => "as3_pushint";

        public uint IntIndex { get; set; }

        public As3PushInt(uint intIndex)
        {
            IntIndex = intIndex;
        }

        public object GetValue(ConstantPoolInfo cPool)
        {
            return cPool.GetIntAt(IntIndex);
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(IntIndex);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {IntIndex}";
        }

        
    }

    public class As3PushUInt : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushUInt;

        public static new string Name => "as3_pushuint";

        public uint UIntIndex { get; set; }

        public As3PushUInt(uint uIntIndex)
        {
            UIntIndex = uIntIndex;
        }

        public object GetValue(ConstantPoolInfo cPool)
        {
            return cPool.GetUIntAt(UIntIndex);
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(UIntIndex);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {UIntIndex}";
        }

        
    }

    public class As3PushByte : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushByte;

        public static new string Name => "as3_pushbyte";

        public sbyte Byte { get; set; }

        public As3PushByte(sbyte @byte)
        {
            Byte = @byte;
        }

        public object GetValue(ConstantPoolInfo cPool = null)
        {
            return Byte;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteByte((byte)Byte);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Byte}";
        }

        
    }

    public class As3PushShort : As3Instruction, IPush
    {
        public override Opcode Opcode => Opcode.PushShort;

        public static new string Name => "as3_pushshort";

        public int Short { get; set; }

        public As3PushShort(uint @short)
        {
            Short = (int)@short;
        }

        public object GetValue(ConstantPoolInfo cPool)
        {
            return Short;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30((uint)Short);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Short}";
        }

        
    }

}
