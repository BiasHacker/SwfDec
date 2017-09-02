using SwfDec.AVM2.Types.Multinames;

namespace SwfDec.AVM2.ByteCode.Instructions.Conditions
{
    interface ICondition
    {
    }

    public interface IJump
    {
        int Offset { get; set; }
    }

    public class As3Jump : As3Instruction, IJump
    {
        public override Opcode Opcode => Opcode.Jump;

        public static new string Name => "as3_jump";

        public int Offset { get; set; }

        public As3Jump(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
           AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }
    }

    public class As3IfEqual : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfEqual;

        public static new string Name => "as3_ifequal";

        public int Offset { get; set; }

        public As3IfEqual(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
            return Name + $" {Offset}";
        }
    }

    public class As3IfFalse : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfFalse;

        public static new string Name => "as3_iffalse";

        public int Offset { get; set; }

        public As3IfFalse(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }
    }

    public class As3IfGreaterEqual: As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfGreaterEqual;

        public static new string Name => "as3_ifgreatereq";

        public int Offset { get; set; }

        public As3IfGreaterEqual(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }
    }

    public class As3IfGreaterThan : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfGreaterThan;

        public static new string Name => "as3_ifgreaterthan";

        public int Offset { get; set; }

        public As3IfGreaterThan(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }
    }

    public class As3IfLessEqual : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfLessEqual;

        public static new string Name => "as3_iflesseq";

        public int Offset { get; set; }

        public As3IfLessEqual(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }
    }

    public class As3IfLessThan : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfLessThan;

        public static new string Name => "as3_iflessthan";

        public int Offset { get; set; }

        public As3IfLessThan(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }
    }

    public class As3IfNotEqual : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfNotEqual;

        public static new string Name => "as3_ifnoteq";

        public int Offset { get; set; }

        public As3IfNotEqual(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }
    }

    public class As3IfNotGreaterEqual : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfNotGreaterEqual;

        public static new string Name => "as3_ifnotgreatereq";

        public int Offset { get; set; }

        public As3IfNotGreaterEqual(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }
    }

    public class As3IfNotGreaterThan : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfNotGreaterThan;

        public static new string Name => "as3_ifnotgreaterthan";

        public int Offset { get; set; }

        public As3IfNotGreaterThan(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }

        
    }

    public class As3IfNotLessEqual : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfNotLessEqual;

        public static new string Name => "as3_ifnotlesseq";

        public int Offset { get; set; }

        public As3IfNotLessEqual(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }

        
    }

    public class As3IfNotLessThan : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfNotLessThan;

        public static new string Name => "as3_ifnotlessthan";

        public int Offset { get; set; }

        public As3IfNotLessThan(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }

        
    }

    public class As3IfStrictEqual : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfStrictEqual;

        public static new string Name => "as3_ifstricteq";

        public int Offset { get; set; }

        public As3IfStrictEqual(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }

        
    }

    public class As3IfStrictNotEqual : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfStrictNotEqual;

        public static new string Name => "as3_ifstrictnoteq";

        public int Offset { get; set; }

        public As3IfStrictNotEqual(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }

        
    }

    public class As3IfTrue : As3Instruction, ICondition, IJump
    {
        public override Opcode Opcode => Opcode.IfTrue;

        public static new string Name => "as3_iftrue";

        public int Offset { get; set; }

        public As3IfTrue(int offset)
        {
            Offset = offset;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(Offset);
            return stream;
        }

        public override string ToString()
        {
           return Name + $" {Offset}";
        }

        
    }

    public interface ICaseSwitch
    {
        int DefaultOffset { get; set; }
        int[] CaseOffsets { get; set; }
    }

    public class As3LookupSwitch : As3Instruction, ICondition, ICaseSwitch
    {
        public override Opcode Opcode => Opcode.LookupSwitch;

        public static new string Name => "as3_lookupswitch";

        public int DefaultOffset { get; set; }
        public int[] CaseOffsets { get; set; }

        public As3LookupSwitch(int defaultOffset, int[] caseOffsets)
        {
            DefaultOffset = defaultOffset;
            CaseOffsets = caseOffsets;
        }

        protected override byte[] InsBytes()
        {
            AbcStream stream = new AbcStream();
            stream.WriteS24(DefaultOffset);
            stream.WriteU30((uint)CaseOffsets.Length - 1);
            for (int i = 0; i < CaseOffsets.Length; ++i )
                stream.WriteS24(CaseOffsets[i]);
            return stream;
        }

        public override string ToString()
        {
           return Name + string.Format(" {1}", DefaultOffset, string.Join(", ", CaseOffsets));
        }

        
    }


    public class As3Equals : As3Instruction, ICondition
    {
        public override Opcode Opcode => Opcode.Equals;

        public static new string Name => "as3_equals";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3GreaterEquals : As3Instruction, ICondition
    {
        public override Opcode Opcode => Opcode.GreaterEquals;

        public static new string Name => "as3_greaterequals";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3GreaterThan : As3Instruction, ICondition
    {
        public override Opcode Opcode => Opcode.GreaterThan;

        public static new string Name => "as3_greaterthan";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3StrictEquals : As3Instruction, ICondition
    {
        public override Opcode Opcode => Opcode.StrictEquals;

        public static new string Name => "as3_strictequals";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3LessEquals : As3Instruction, ICondition
    {
        public override Opcode Opcode => Opcode.LessEquals;

        public static new string Name => "as3_lessequals";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3LessThan : As3Instruction, ICondition
    {
        public override Opcode Opcode => Opcode.LessThan;

        public static new string Name => "as3_lessthan";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3IsTypeLate : As3Instruction, ICondition
    {
        public override Opcode Opcode => Opcode.IsTypeLate;

        public static new string Name => "as3_as3TypeLate";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3IsType : As3Instruction, ICondition
    {
        public override Opcode Opcode => Opcode.IsType;

        public static new string Name => "as3_istype";

        public MultinameInfo Multiname { get; set; }

        public As3IsType(MultinameInfo multiname)
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
}
