namespace SwfDec.AVM2.ByteCode.Instructions.Return
{
    public interface IReturn
    {
    }

    //AddValueInfo(Opcode.ReturnValue);
    //        AddValueInfo(Opcode.ReturnVoid);

    public class As3ReturnValue : As3Instruction, IReturn
    {
        public override Opcode Opcode => Opcode.ReturnValue;

        public static new string Name => "as3_returvalue";

        public override string ToString()
        {
            return Name;
        }

        
    }

    public class As3ReturnVoid : As3Instruction, IReturn
    {
        public override Opcode Opcode => Opcode.ReturnVoid;

        public static new string Name => "as3_returnvoid";

        public override string ToString()
        {
            return Name;
        }

        
    }
}
