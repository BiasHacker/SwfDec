using System;
using System.Linq;
using SwfDec.AVM2.ByteCode.Instructions;
using SwfDec.AVM2.ByteCode.Instructions.Push;
using SwfDec.AVM2.Types.Extended;
using SwfDec.AVM2.Types.Multinames;

namespace SwfDec.AVM2.Types.Traits
{
    public class TraitField : TraitBase, IBrowseable
    {
        public uint SlotId { get; set; }
        public MultinameInfo TypeName { get; set; }
        public uint ValueIndex { get; set; }
        public ValueKind ValueKind { get; set; }

        public object GetValue(ConstantPoolInfo constantPoolInfo)
        {
            object result;

            switch (ValueKind)
            {
                case ValueKind.Int:
                    result = constantPoolInfo.GetIntAt(ValueIndex);
                    break;

                case ValueKind.UInt:
                    result = constantPoolInfo.GetUIntAt(ValueIndex);
                    break;

                case ValueKind.Double:
                    result = constantPoolInfo.GetDoubleAt(ValueIndex);
                    break;

                case ValueKind.Utf8:
                    result = '"' + constantPoolInfo.GetStringAt(ValueIndex) + '"';
                    break;

                case ValueKind.False:
                    result = "false";
                    break;

                case ValueKind.True:
                    result = "true";
                    break;

                case ValueKind.Null:
                    result = "null";
                    break;

                case ValueKind.Undefined:
                    result = "undefined";
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public new object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }

        public As3Instruction GetPushInstruction(ConstantPoolInfo cPool)
        {
            if (ValueIndex != 0)
                switch (ValueKind)
                {
                    case ValueKind.Double:
                        double doubleValue = (double)GetValue(cPool);
                        if (doubleValue % 1 == 0)
                        {
                            if (doubleValue >= sbyte.MinValue && doubleValue <= sbyte.MaxValue)
                                return new As3PushByte((sbyte)doubleValue);
                            else if (doubleValue >= short.MinValue && doubleValue <= short.MaxValue)
                                return new As3PushShort((uint)doubleValue);
                        }
                        return new As3PushDouble(ValueIndex);

                    case ValueKind.Utf8:
                        return new As3PushString(cPool.GetStringAt(ValueIndex));

                    case ValueKind.UInt:
                          uint uintValue = (uint)GetValue(cPool);
                          if (uintValue % 1 == 0)
                          {
                              if (uintValue <= sbyte.MaxValue)
                                  return new As3PushByte((sbyte)uintValue);
                              else if (uintValue <= short.MaxValue)
                                  return new As3PushShort((uint)uintValue);
                          }
                        return new As3PushUInt(ValueIndex);

                    case ValueKind.Int:
                          int intValue = (int)GetValue(cPool);
                          if (intValue % 1 == 0)
                        {
                            if (intValue >= sbyte.MinValue && intValue <= sbyte.MaxValue)
                                return new As3PushByte((sbyte)intValue);
                            else if (intValue >= short.MinValue && intValue <= short.MaxValue)
                                return new As3PushShort((uint)intValue);
                        }
                        return new As3PushInt(ValueIndex);

                    case ValueKind.True:
                        return new As3PushTrue();

                    case ValueKind.False:
                        return new As3PushFalse();

                    case ValueKind.Undefined:
                        return new As3PushUndefined();
                }

            throw new Exception();
        }

        public string GetTypeName()
        {
            if (TypeName == null)
                return "*";

            if (TypeName.Kind == MultinameKind.GenericName)
                return "Vector<" + string.Join(", ", (from n in (TypeName.Data as MKGenericName).ParamArray select n.MKQName)) + ">";
            

            return TypeName.MKQName.Name;
        }

        public void SetValue(As3Instruction ins, ConstantPoolInfo cPool = null)
        {
            switch(ins.Opcode)
            {
                case Opcode.PushTrue:
                    ValueKind = Types.ValueKind.True;
                    ValueIndex = 0;
                    break;

                case Opcode.PushFalse:
                    ValueKind = Types.ValueKind.False;
                    ValueIndex = 0;
                    break;

                case Opcode.PushString:
                    ValueKind = Types.ValueKind.Utf8;
                    ValueIndex = (ins as As3PushString).String.Index;
                    break;
                
                case Opcode.PushShort:
                    ValueKind = Types.ValueKind.Int;
                    int pushShortValue = (ins as As3PushShort).Short;
                    uint intIndex = cPool.IndexOfInt(pushShortValue);
                    if (intIndex == 0)
                        ValueIndex = cPool.AddIntAtEnd(pushShortValue);
                    else
                        ValueIndex = intIndex;
                    break;

                case Opcode.PushByte:
                    ValueKind = Types.ValueKind.Int;
                    sbyte pushByte = (ins as As3PushByte).Byte;
                    uint byteIndex = cPool.IndexOfInt(pushByte);
                    if (byteIndex == 0)
                        ValueIndex = cPool.AddIntAtEnd(pushByte);
                    else
                        ValueIndex = byteIndex;
                    break;

                case Opcode.PushDouble:
                    ValueKind = Types.ValueKind.Double;
                    ValueIndex = (ins as As3PushDouble).DoubleIndex;
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
