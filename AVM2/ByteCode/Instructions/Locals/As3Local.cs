namespace SwfDec.AVM2.ByteCode.Instructions.Locals
{
    public interface  ILocal
    {
    }

    public class As3Kill : As3Instruction, ILocal
    {
        public override Opcode Opcode => Opcode.Kill;

        public static new string Name => "as3_kill";

        public uint LocalIndex { get; set; }

        public As3Kill(uint localIndex)
        {
            LocalIndex = localIndex;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(LocalIndex);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {LocalIndex}";
        }

        
    }

    public interface IGetLocal : ILocal
    {
    }

    public class As3GetLocal : As3Instruction, IGetLocal
    {
        public override Opcode Opcode => Opcode.GetLocal;

        public static new string Name => "as3_getlocal";

        public uint LocalIndex { get; set; }

        public As3GetLocal(uint localIndex)
        {
            LocalIndex = localIndex;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(LocalIndex);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {LocalIndex}";
        }

        
    }

    public class As3GetLocal0 : As3Instruction, IGetLocal
    {
        public override Opcode Opcode => Opcode.GetLocal0;

        public static new string Name => "as3_getlocal0";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3GetLocal1 : As3Instruction, IGetLocal
    {
        public override Opcode Opcode => Opcode.GetLocal1;

        public static new string Name => "as3_getlocal1";


        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3GetLocal2 : As3Instruction, IGetLocal
    {
        public override Opcode Opcode => Opcode.GetLocal2;

        public static new string Name => "as3_getlocal2";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3GetLocal3 : As3Instruction, IGetLocal
    {
        public override Opcode Opcode => Opcode.GetLocal3;

        public static new string Name => "as3_getlocal3";

        public override string ToString()
        {
            return Name;
        }

        
    }


    public interface ISetLocal : ILocal
    {
    }

    public class As3SetLocal : As3Instruction, ISetLocal
    {
        public override Opcode Opcode => Opcode.SetLocal;

        public static new string Name => "as3_setlocal";

        public uint LocalIndex { get; set; }

        public As3SetLocal(uint localIndex)
        {
            LocalIndex = localIndex;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(LocalIndex);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {LocalIndex}";
        }

        
    }

    public class As3SetLocal0 : As3Instruction, ISetLocal
    {
        public override Opcode Opcode => Opcode.SetLocal0;

        public static new string Name => "as3_setlocal0";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3SetLocal1 : As3Instruction, ISetLocal
    {
        public override Opcode Opcode => Opcode.SetLocal1;

        public static new string Name => "as3_setlocal1";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3SetLocal2 : As3Instruction, ISetLocal
    {
        public override Opcode Opcode => Opcode.SetLocal2;

        public static new string Name => "as3_setlocal2";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3SetLocal3 : As3Instruction, ISetLocal
    {
        public override Opcode Opcode => Opcode.SetLocal3;

        public static new string Name => "as3_setlocal3";

        public override string ToString()
        {
            return Name;
        }

        
    }
}
