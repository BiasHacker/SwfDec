namespace SwfDec.AVM2.ByteCode.Instructions.FastMemory
{
    public interface IFastMemory
    {
    }

    public interface IStore : IFastMemory
    {
    }

    public class As3SI8 : As3Instruction, IStore
    {
        public override Opcode Opcode => Opcode.SI8;

        public static new string Name => "as3_si8";

        public override string ToString()
        {
            return Name;
        }

        

    }

    public class As3SI16 : As3Instruction, IStore
    {
        public override Opcode Opcode => Opcode.SI16;

        public static new string Name => "as3_si16";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3SI32 : As3Instruction, IStore
    {
        public override Opcode Opcode => Opcode.SI32;

        public static new string Name => "as3_si32";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3SF32 : As3Instruction, IStore
    {
        public override Opcode Opcode => Opcode.SF32;

        public static new string Name => "as3_sf32";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public interface ILoad : IFastMemory
    {

    }

    public class As3LI8 : As3Instruction, ILoad
    {
        public override Opcode Opcode => Opcode.LI8;

        public static new string Name => "as3_li8";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3LI16 : As3Instruction, ILoad
    {
        public override Opcode Opcode => Opcode.LI16;

        public static new string Name => "as3_li16";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3LI32 : As3Instruction, ILoad
    {
        public override Opcode Opcode => Opcode.LI32;

        public static new string Name => "as3_li32";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3LF32 : As3Instruction, ILoad
    {
        public override Opcode Opcode => Opcode.LF32;

        public static new string Name => "as3_lf32";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3LF64 : As3Instruction, ILoad
    {
        public override Opcode Opcode => Opcode.LF64;

        public static new string Name => "as3_lf64";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public interface ISign : IFastMemory
    {
    }

    public class As3SX1 : As3Instruction, ISign
    {
        public override Opcode Opcode => Opcode.SXI1;

        public static new string Name => "as3_sx1";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3SXI8 : As3Instruction, ISign
    {
        public override Opcode Opcode => Opcode.SXI8;

        public static new string Name => "as3_sxi8";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3SX16 : As3Instruction, ISign
    {
        public override Opcode Opcode => Opcode.SXI16;

        public static new string Name => "as3_sxi16";

        public override string ToString()
        {
            return Name;
        }

        
    }
}
