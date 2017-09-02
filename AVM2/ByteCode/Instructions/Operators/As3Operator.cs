using SwfDec.AVM2.ByteCode.Instructions.Push;
using SwfDec.AVM2.Types;
namespace SwfDec.AVM2.ByteCode.Instructions.Operators
{
    public interface IOperator
    {
    }

    public class As3Not : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.Not;

        public static new string Name => "as3_not";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3Add : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.Add;

        public static new string Name => "as3_add";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3AddI : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.AddI;

        public static new string Name => "as3_add_i";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3AddD : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.AddD;

        public static new string Name => "as3_add_d";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3Subtract : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.Subtract;

        public static new string Name => "as3_substract";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3SubtractI : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.SubtractI;

        public static new string Name => "as3_substract_i";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3Divide : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.Divide;

        public static new string Name => "as3_divide";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3Multiply : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.Multiply;

        public static new string Name => "as3_multiply";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3MultiplyI : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.MultiplyI;

        public static new string Name => "as3_multiply_i";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3Negate : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.Negate;

        public static new string Name => "as3_negate";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3NegateI : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.NegateI;

        public static new string Name => "as3_negate_i";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3Modulo : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.Modulo;

        public static new string Name => "as3_modulo";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3Increment : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.Increment;

        public static new string Name => "as3_increment";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3IncrementI : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.IncrementI;

        public static new string Name => "as3_increment_i";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3Decrement : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.Decrement;

        public static new string Name => "as3_decrement";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3DecrementI : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.DecrementI;

        public static new string Name => "as3_decrement_i";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3IncLocal : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.Inclocal;

        public static new string Name => "as3_inclocal";

        public uint Index { get; set; }

        public As3IncLocal(uint index)
        {
            Index = index;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Index);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Index}";
        }

        
    }

    public class As3IncLocalI : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.InclocalI;

        public static new string Name => "as3_inclocal_i";

        public uint Index { get; set; }

        public As3IncLocalI(uint index)
        {
            Index = index;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Index);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Index}";
        }

        
    }

    public class As3DecLocal : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.DecLocal;

        public static new string Name => "as3_declocal";

        public uint Index { get; set; }

        public As3DecLocal(uint index)
        {
            Index = index;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Index);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Index}";

        }
             
    }

    public class As3DecLocalI : As3Instruction, IOperator
    {
        public override Opcode Opcode => Opcode.DecLocalI;

        public static new string Name => "as3_declocal_i";

        public uint Index { get; set; }

        public As3DecLocalI(uint index)
        {
            Index = index;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Index);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Index}";
        }

        
    }

    public interface IBitOperator : IOperator
    {
    }

    public class As3BitAnd : As3Instruction, IBitOperator
    {
        public override Opcode Opcode => Opcode.BitAnd;

        public static new string Name => "as3_bit_and";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3BitNot : As3Instruction, IBitOperator
    {
        public override Opcode Opcode => Opcode.BitNot;

        public static new string Name => "as3_bit_not";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3BitOr : As3Instruction, IBitOperator
    {
        public override Opcode Opcode => Opcode.BitOr;

        public static new string Name => "as3_bit_or";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3BitXor : As3Instruction, IBitOperator
    {
        public override Opcode Opcode => Opcode.BitXor;

        public static new string Name => "as3_bit_xor";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3RShift : As3Instruction, IBitOperator
    {
        public override Opcode Opcode => Opcode.RShift;

        public static new string Name => "as3_rshift";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3URShift : As3Instruction, IBitOperator
    {
        public override Opcode Opcode => Opcode.URShift;

        public static new string Name => "as3_rushift";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3LShift : As3Instruction, IBitOperator
    {
        public override Opcode Opcode => Opcode.LShift;

        public static new string Name => "as3_lshift";

        public override string ToString()
        {
            return Name;
        }

        
    }
}