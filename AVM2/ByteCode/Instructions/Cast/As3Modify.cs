using SwfDec.AVM2.Types.Multinames;

namespace SwfDec.AVM2.ByteCode.Instructions.Cast
{
    public class As3Nop : As3Instruction
    {
        public override Opcode Opcode => Opcode.Nop;

        public static new string Name => "as3_nop";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public interface ICast
    {
    }

    public interface IModifyScope
    {
    }

    public class As3Dup : As3Instruction, IModifyScope
    {
        public override Opcode Opcode => Opcode.Dup;

        public static new string Name => "as3_dup";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3Pop : As3Instruction, IModifyScope
    {
        public override Opcode Opcode => Opcode.Pop;

        public static new string Name => "as3_pop";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3Swap : As3Instruction, IModifyScope
    {
        public override Opcode Opcode => Opcode.Swap;

        public static new string Name => "as3_swap";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3PopScope : As3Instruction, IModifyScope
    {
        public override Opcode Opcode => Opcode.PopScope;

        public static new string Name => "as3_popscope";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public interface IConvert : ICast
    {
    }

    public class As3ConvertS : As3Instruction, IConvert
    {
        public override Opcode Opcode => Opcode.ConvertS;

        public static new string Name => "as3_convert_s";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3ConvertB : As3Instruction, IConvert
    {
        public override Opcode Opcode => Opcode.ConvertB;

        public static new string Name => "as3_convert_b";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3ConvertD : As3Instruction, IConvert
    {
        public override Opcode Opcode => Opcode.ConvertD;

        public static new string Name => "as3_convert_d";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3ConvertI : As3Instruction, IConvert
    {
        public override Opcode Opcode => Opcode.ConvertI;

        public static new string Name => "as3_convert_i";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3ConvertO : As3Instruction, IConvert
    {
        public override Opcode Opcode => Opcode.ConvertO;

        public static new string Name => "as3_convert_o";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3ConvertU : As3Instruction, IConvert
    {
        public override Opcode Opcode => Opcode.ConvertU;

        public static new string Name => "as3_convert_u";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public interface ICoerce : ICast
    {
    }

    public class As3Coerce : As3Instruction, ICoerce
    {
        public override Opcode Opcode => Opcode.Coerce;

        public static new string Name => "as3_coerce";

        public MultinameInfo Multiname { get; set; }

        public As3Coerce(MultinameInfo multiname)
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

    public class As3CoerceA : As3Instruction, ICoerce
    {
        public override Opcode Opcode => Opcode.CoerceA;

        public static new string Name => "as3_coerce_a";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3CoerceS : As3Instruction, ICoerce
    {
        public override Opcode Opcode => Opcode.CoerceS;

        public static new string Name => "as3_coerce_s";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3CoerceB : As3Instruction, ICoerce
    {
        public override Opcode Opcode => Opcode.CoerceB;

        public static new string Name => "as3_coerce_b";

        public override string ToString()
        {
            return Name;
        }
        
    }

    public class As3CoerceD : As3Instruction, ICoerce
    {
        public override Opcode Opcode => Opcode.CoerceD;

        public static new string Name => "as3_coerce_d";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3CoerceI : As3Instruction, ICoerce
    {
        public override Opcode Opcode => Opcode.CoerceI;

        public static new string Name => "as3_coerce_i";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3CoerceO : As3Instruction, ICoerce
    {
        public override Opcode Opcode => Opcode.CoerceO;

        public static new string Name => "as3_coerce_o";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3CoerceU : As3Instruction, ICoerce
    {
        public override Opcode Opcode => Opcode.CoerceU;

        public static new string Name => "as3_coerce_u";

        public override string ToString()
        {
            return Name;
        }

        
    }
}

