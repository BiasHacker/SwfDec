using System;
using SwfDec.AVM2.Types.Extended;
using SwfDec.AVM2.Types.Traits;
using SwfDec.AVM2.ByteCode;

namespace SwfDec.AVM2.Types
{
    public class MethodBodyInfo : IBrowseable
    {
        public uint Index { get; private set; }

        public MethodInfo Method { get; set; }
        public uint MaxStack { get; set; }
        public uint LocalCount { get; set; }
        public uint MinScopeDepth { get; set; }
        public uint MaxScopeDepth { get; set; }
        public byte[] Code { get; set; }

        public ExceptionInfo[] ExceptionArray { get; set; }
        public TraitsArray TraitsArray { get; set; }

        public MethodBodyInfo(uint index)
        {
            Index = index;
        }

        ~MethodBodyInfo()
        {
            Code = null;
            ExceptionArray = null;
            TraitsArray = null;
        }

        public ByteCode.ByteCode GetBytecode(Abc abc)
        {
              return new ByteCode.ByteCode(Code, abc);   
        }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }

    public class ExceptionInfo : IBrowseable 
    {
        public uint From { get; set; }
        public uint To { get; set; }
        public uint Target { get; set; }
        public uint Type { get; set; }
        public uint VariableName { get; set; }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}