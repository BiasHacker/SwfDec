using SwfDec.AVM2.Types.Multinames;

namespace SwfDec.AVM2.ByteCode.Instructions.Call
{
    public interface ICall
    {
    }

    //AddValueInfo(Opcode.Call, MakeIns("U30"));
    //        AddValueInfo(Opcode.CallSuper, MakeIns("U30", "U30"));
    //        AddValueInfo(Opcode.CallSuperVoid, MakeIns("U30", "U30"));

    public class As3Call : As3Instruction, ICall
    {
        public override Opcode Opcode => Opcode.Call;

        public static new string Name => "as3_call";

        public uint ArgCount { get; set; }

        public As3Call(uint argCount)
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
           return Name + $"  {ArgCount}";
        }

        
    }

    public class As3CallPropLex : As3Instruction, ICall
    {
        public override Opcode Opcode => Opcode.CallPropLex;

        public static new string Name => "as3_callproplex";

        public MultinameInfo Multiname { get; set; }
        public uint ArgCount { get; set; }

        public As3CallPropLex(MultinameInfo multiname, uint argCount)
        {
            Multiname = multiname;
            ArgCount = argCount;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Multiname.Index);
            stream.WriteU30(ArgCount);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Multiname.Index} {ArgCount}";
        }

        
    }

    public class As3CallProperty : As3Instruction, ICall
    {
        public override Opcode Opcode => Opcode.CallProperty;

        public static new string Name => "as3_callproperty";

        public MultinameInfo Multiname { get; set; }
        public uint ArgCount { get; set; }

        public As3CallProperty(MultinameInfo multiname, uint argCount)
        {
            Multiname = multiname;
            ArgCount = argCount;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Multiname.Index);
            stream.WriteU30(ArgCount);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Multiname} {ArgCount}";
        }

        
    }

    public class As3CallPropVoid : As3Instruction, ICall
    {
        public override Opcode Opcode => Opcode.CallPropVoid;

        public static new string Name => "as3_callpropvoid";

        public MultinameInfo Multiname { get; set; }
        public uint ArgCount { get; set; }

        public As3CallPropVoid(MultinameInfo multiname, uint argCount)
        {
            Multiname = multiname;
            ArgCount = argCount;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Multiname.Index);
            stream.WriteU30(ArgCount);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Multiname} {ArgCount}";
        }

        
    }

    public class As3CallSuper : As3Instruction, ICall
    {
        public override Opcode Opcode => Opcode.CallSuper;

        public static new string Name => "as3_callsuper";

        public MultinameInfo Multiname { get; set; }
        public uint ArgCount { get; set; }

        public As3CallSuper(MultinameInfo multiname, uint argCount)
        {
            Multiname = multiname;
            ArgCount = argCount;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Multiname.Index);
            stream.WriteU30(ArgCount);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Multiname} {ArgCount}";
        }

        
    }

    public class As3CallSuperVoid : As3Instruction, ICall
    {
        public override Opcode Opcode => Opcode.CallSuperVoid;

        public static new string Name => "as3_callsupervoid";

        public MultinameInfo Multiname { get; set; }
        public uint ArgCount { get; set; }

        public As3CallSuperVoid(MultinameInfo multiname, uint argCount)
        {
            Multiname = multiname;
            ArgCount = argCount;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteU30(Multiname.Index);
            stream.WriteU30(ArgCount);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Multiname} {ArgCount}";
        }

        
    }
}
