namespace SwfDec.AVM2.ByteCode.Instructions.Create
{
    public class As3Label : As3Instruction
    {
        public override Opcode Opcode => Opcode.Label;

        public static new string Name => "as3_label";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3Throw : As3Instruction
    {
        public override Opcode Opcode => Opcode.Throw;

        public static new string Name => "as3_throw";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public interface INew
    {
    }
    public class As3NewCatch : As3Instruction, INew
    {
        public override Opcode Opcode => Opcode.NewCatch;

        public static new string Name => "as3_newcatch";

        public uint ExceptionIndex { get; set; }

        public As3NewCatch(uint exceptionIndex)
        {
            ExceptionIndex = exceptionIndex;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(ExceptionIndex);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {ExceptionIndex}";
        }

        
    }

    public class As3NewClass : As3Instruction, INew
    {
        public override Opcode Opcode => Opcode.NewClass;

        public static new string Name => "as3_newclass";

        public uint ClassIndex { get; set; }

        public As3NewClass(uint classIndex)
        {
            ClassIndex = classIndex;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(ClassIndex);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {ClassIndex}";
        }

        
    }

    public class As3NewFunction : As3Instruction, INew
    {
        public override Opcode Opcode => Opcode.NewFunction;

        public static new string Name => "as3_newfunction";

        public uint MethodIndex { get; set; }

        public As3NewFunction(uint methodIndex)
        {
            MethodIndex = methodIndex;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(MethodIndex);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {MethodIndex}";
        }

        
    }

    public class As3NewObject : As3Instruction, INew
    {
        public override Opcode Opcode => Opcode.NewObject;

        public static new string Name => "as3_newobject";

        public uint ArgCount { get; set; }

        public As3NewObject(uint argCount)
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

    public class As3NewArray : As3Instruction, INew
    {
        public override Opcode Opcode => Opcode.NewArray;

        public static new string Name => "as3_newarray";

        public uint ArgCount { get; set; }

        public As3NewArray(uint argCount)
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

    public class As3NewActivation : As3Instruction, INew
    {
        public override Opcode Opcode => Opcode.NewActivation;

        public static new string Name => "as3_newactivation";

        public override string ToString()
        {
            return Name;
        }

        
    }
}
