using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using SwfDec.AVM2.Types;
using SwfDec.AVM2.Types.Extended;
using SwfDec.AVM2.Types.Multinames;
using SwfDec.AVM2.Types.Traits;

namespace SwfDec.AVM2
{
    public sealed class AbcStream : IDisposable
    {
        private readonly MemoryStream _stream;

        public int Position
        {
            get { return (int) _stream.Position; }

            set { _stream.Position = value; }
        }

        public int Length => (int) _stream.Length;

        public AbcStream()
        {
            _stream = new MemoryStream();
        }

        public Byte[] ReadBytes(int count)
        {
            Byte[] result = new Byte[count];
            _stream.Read(result, 0, count);
            return result;
        }

        public void WriteBytes(Byte[] buffer)
        {
            _stream.Write(buffer, 0, buffer.Length);
        }

        public Byte[] ReadReversedBytes(int count)
        {
            Byte[] result = ReadBytes(count);
            Array.Reverse(result);
            return result;
        }

        public void WriteReversedBytes(Byte[] buffer)
        {
            Array.Reverse(buffer);
            WriteBytes(buffer);
        }

        public Byte ReadByte()
        {
            return (Byte) _stream.ReadByte();
        }

        public void WriteByte(Byte value)
        {
            _stream.WriteByte(value);
        }

        public short ReadShort()
        {
            return BitConverter.ToInt16(ReadBytes(2), 0);
        }

        public void WriteShort(short value)
        {
            WriteBytes(BitConverter.GetBytes(value));
        }

        public int ReadInt()
        {
            return BitConverter.ToInt32(ReadBytes(4), 0);
        }

        public void WriteInt(int value)
        {
            WriteBytes(BitConverter.GetBytes(value));
        }

        public uint ReadU30()
        {
            uint num1 = 0;
            int num2 = 0;
            while (num2 != 35)
            {
                byte num3 = ReadByte();
                num1 |= (num3 & (uint) sbyte.MaxValue) << num2;
                num2 += 7;
                if ((num3 & 128) == 0)
                    return num1;
            }

            return num1;
        }

        public void WriteU30(uint value)
        {
            uint num = value;
            while (num >= 128U)
            {
                WriteByte((byte) (num | 128U));
                num >>= 7;
            }
            WriteByte((byte) num);
        }

        public uint[] ReadU30Array(uint count)
        {
            uint[] result = new uint[count];
            for (int i = 0; i < count; i++)
                result[i] = ReadU30();
            return result;
        }

        public void WriteU30Array(uint[] value, bool writeLength)
        {
            if (writeLength)
                WriteU30((uint) value.Length);
            foreach (uint t in value)
                WriteU30(t);
        }

        public int ReadS24()
        {
            byte[] data = ReadBytes(3);
            int result = (data[2] << 16) | (data[1] << 8) | data[0];
            if ((result & 0x800000) != 0) 
                result = (int)((uint)result | 0xff000000);
            return result;
        }

        public void WriteS24(int value)
        {
            WriteByte((Byte)(value & 0xff));
            WriteByte((Byte)((value >> 8) & 0xff));
            WriteByte((Byte)((value >> 16) & 0xff));
        }

        public int ReadS32()
        {
            return (int) ReadU30();
        }

        public void WriteS32(int value)
        {
            WriteU30((uint) value);
        }

        public double ReadDouble()
        {
            return BitConverter.ToDouble(ReadBytes(8), 0);
        }

        public void WriteDouble(double value)
        {
            WriteBytes(BitConverter.GetBytes(value));
        }

        public String ReadString()
        {
            return Encoding.UTF8.GetString(ReadBytes(ReadS32()));
        }

        public void WriteString(String value)
        {
            byte[] data = Encoding.UTF8.GetBytes(value);
            WriteS32(data.Length);
            WriteBytes(data);
        }

        public Byte[] ReadToEnd()
        {
            return ReadBytes(Length - Position);
        }

        public ConstantPoolInfo ReadConstantPoolInfo()
        {
            ConstantPoolInfo result = new ConstantPoolInfo();
            result.IntArrayCapacity = ReadU30();
            for (uint i = 1; i < result.IntArrayCapacity; i++)
                result.SetIntAt(ReadS32(), i);
            
            result.UIntArrayCapacity = ReadU30();
            for (uint i = 1; i < result.UIntArrayCapacity; i++)
                result.SetUIntAt(ReadU30(), i);

            result.DoubleArrayCapacity = ReadU30();
            for (uint i = 1; i < result.DoubleArrayCapacity; i++)
                result.SetDoubleAt(ReadDouble(), i);

            result.StringArrayCapacity = ReadU30();
            for (uint i = 1; i < result.StringArrayCapacity; i++)
                result.SetStringAt(new StringInfo(i, ReadString()), i);

            result.NamespaceArrayCapacity = ReadU30();
            for (uint i = 1; i < result.NamespaceArrayCapacity; i++)
            {
                NamespaceInfo namespaceInfo = new NamespaceInfo(result, i);
                namespaceInfo.Kind = (NamespaceKind)ReadByte();
                namespaceInfo.Name = result.GetStringAt(ReadU30());
                result.SetNamespaceAt(namespaceInfo, i);
            }

            result.NamespaceSetArrayCapacity = ReadU30();
            for (uint i = 1; i < result.NamespaceSetArrayCapacity; i++)
            {
                NamespaceSetInfo namespaceSetInfo = new NamespaceSetInfo(i);
                namespaceSetInfo.NamespaceArray = new NamespaceInfo[ReadU30()];
                for (int j = 0; j < namespaceSetInfo.NamespaceArray.Length; j++)
                    namespaceSetInfo.NamespaceArray[j] = result.GetNamespaceAt(ReadU30());
                
                result.SetNamespaceSetAt(namespaceSetInfo, i);
            }

            result.MultinameArrayCapacity = ReadU30();
            for (uint i = 1; i < result.MultinameArrayCapacity; i++)
                result.SetMultinameAt(ReadMultinameInfo(result, i), i);
            return result;
        }

        public void WriteConstantPool(ConstantPoolInfo value)
        {
            WriteU30(value.IntArrayLength);
            for (uint i = 1; i < value.IntArrayLength; i++)
                WriteS32(value.GetIntAt(i));

            WriteU30(value.UIntArrayLength);
            for (uint i = 1; i < value.UIntArrayLength; i++)
                WriteU30(value.GetUIntAt(i));

            WriteU30(value.DoubleArrayLength);
            for (uint i = 1; i < value.DoubleArrayLength; i++)
                WriteDouble(value.GetDoubleAt(i));

            WriteU30(value.StringArrayLength);
            for (uint i = 1; i < value.StringArrayLength; i++) {
                StringInfo stringInfo = value.GetStringAt(i);
  
                 WriteString(stringInfo);
            }
               

            WriteU30(value.NamespaceArrayLength);
            for (uint i = 1; i < value.NamespaceArrayLength; i++)
            {
                var namespaceInfo = value.GetNamespaceAt(i);
                WriteByte((Byte)namespaceInfo.Kind);
                WriteU30(namespaceInfo.Name?.Index ?? 0);
            }

            WriteU30(value.NamespaceSetArrayLength);
            for (uint i = 1; i < value.NamespaceSetArrayLength; i++)
                WriteU30Array((from n in value.GetNamespaceSetAt(i).NamespaceArray select n.Index).ToArray(), true);

            WriteU30(value.MultinameArrayLength);
            for (uint i = 1; i < value.MultinameArrayLength; i++)
                WriteMultinameInfo(value.GetMultinameAt(i));
        }

        public MultinameInfo ReadMultinameInfo(ConstantPoolInfo constantPool, uint index)
        {
            MultinameInfo result = new MultinameInfo(index);
            result.Kind = (MultinameKind) ReadByte();
            switch (result.Kind)
            {
                case MultinameKind.QName:
                case MultinameKind.QNameA:
                    var mkQName = new MKQName();
                    mkQName.Namespace = constantPool.GetNamespaceAt(ReadU30());
                    mkQName.Name = constantPool.GetStringAt(ReadU30());
                    result.Data = mkQName;
                    break;

                case MultinameKind.RTQName:
                case MultinameKind.RTQNameA:
                    var mkRtkName = new MKRTQName();
                    mkRtkName.Name = constantPool.GetStringAt(ReadU30());
                    result.Data = mkRtkName;
                    break;

                case MultinameKind.RTQNameL:
                case MultinameKind.RTQNameLA:
                    var mkRtqNameL = new MKRTQNameL();
                    result.Data = mkRtqNameL;
                    break;

                case MultinameKind.Multiname:
                case MultinameKind.MultinameA:
                    var mkMultiname = new MKMultiname();
                 
                    mkMultiname.Name = constantPool.GetStringAt(ReadU30());
                    mkMultiname.NamespaceSet = constantPool.GetNamespaceSetAt(ReadU30());
                    result.Data = mkMultiname;
                    break;

                case MultinameKind.MultinameL:
                case MultinameKind.MultinameLA:
                    var mkMultinameL = new MKMultinameL();
                    mkMultinameL.NamespaceSet = constantPool.GetNamespaceSetAt(ReadU30());
                    result.Data = mkMultinameL;
                    break;

                case MultinameKind.GenericName:
                    var mkGenericName = new MKGenericName(constantPool);
                    mkGenericName.TypeDefinitionIndex = ReadU30();
		            mkGenericName.ParamArrayIndexes = ReadU30Array(ReadU30());
                    result.Data = mkGenericName;
                    break;

                default:
                    throw new Exception();
            }
            return result;
        }

        public void WriteMultinameInfo(MultinameInfo value)
        {
            WriteByte((Byte) value.Kind);

            switch (value.Kind)
            {
                case MultinameKind.QName:
                case MultinameKind.QNameA:
                    WriteU30((value.Data as MKQName).Namespace.Index);
                    WriteU30((value.Data as MKQName).Name.Index);
                    break;

                case MultinameKind.RTQName:
                case MultinameKind.RTQNameA:
                    WriteU30((value.Data as MKRTQName).Name.Index);
                    break;

                case MultinameKind.RTQNameL:
                case MultinameKind.RTQNameLA:
                    break;

                case MultinameKind.Multiname:
                case MultinameKind.MultinameA:
                    WriteU30((value.Data as MKMultiname).Name.Index);
                    WriteU30((value.Data as MKMultiname).NamespaceSet.Index);
                    break;

                case MultinameKind.MultinameL:
                case MultinameKind.MultinameLA:
                    WriteU30((value.Data as MKMultinameL).NamespaceSet.Index);
                    break;

                case MultinameKind.GenericName:
                    WriteU30((value.Data as MKGenericName).TypeDefinition.Index);
                    WriteU30Array((from n in (value.Data as MKGenericName).ParamArray select n.Index).ToArray(), true);
                    break;
            }
        }

        public MethodInfo ReadMethodInfo(ConstantPoolInfo constantPool, uint index)
        {
            MethodInfo result = new MethodInfo(index);
            uint paramCount = ReadU30();
            result.ReturnType = constantPool.GetMultinameAt(ReadU30());
            result.ParamTypeArray = (from n in ReadU30Array(paramCount) select constantPool.GetMultinameAt(n)).ToArray();
            result.Name = constantPool.GetStringAt(ReadU30());
            result.Flags = (MethodFlags) ReadByte();
            if (result.Flags.HasFlag(MethodFlags.HAS_OPTIONAL))
            {
                result.OptionalParamArray = new OptionalParam[ReadU30()];
                for (int i = 0; i < result.OptionalParamArray.Length; i++)
                {
                    result.OptionalParamArray[i] = new OptionalParam();
                    result.OptionalParamArray[i].Value = ReadU30();
                    result.OptionalParamArray[i].Kind = (ValueKind) ReadByte();
                }
            }

            if (result.Flags.HasFlag(MethodFlags.HAS_PARAM_NAMES))
                result.ParamNameArray = ReadU30Array(paramCount);
                  
            return result;
        }

        public void WriteMethodInfo(MethodInfo value)
        {
            WriteU30((uint) value.ParamTypeArray.Length);
            WriteU30(value.ReturnType == null ? 0 : value.ReturnType.Index);

            WriteU30Array((from n in value.ParamTypeArray select n == null ? 0 : n.Index).ToArray(), false);

            WriteU30(value.Name == null ? 0 : value.Name.Index);

            WriteByte((Byte) value.Flags);
            if (value.Flags.HasFlag(MethodFlags.HAS_OPTIONAL))
            {
                WriteU30((uint)value.OptionalParamArray.Length);
                foreach (OptionalParam t in value.OptionalParamArray)
                {
                    WriteU30(t.Value);
                    WriteByte((Byte) t.Kind);
                }
            }

            if (value.Flags.HasFlag(MethodFlags.HAS_PARAM_NAMES))
                WriteU30Array(value.ParamNameArray, false);
        }

        public MetadataInfo ReadMetadataInfo(ConstantPoolInfo constantPool)
        {
            MetadataInfo result = new MetadataInfo();
            result.Name = ReadU30();
            uint count = ReadU30();
            result.KeyArray = (from n in ReadU30Array(count) select constantPool.GetStringAt(n)).ToArray();
            result.ValueArray = (from n in ReadU30Array(count) select constantPool.GetStringAt(n)).ToArray();
            return result;
        }

        public void WriteMetadataInfo(MetadataInfo value)
        {
            WriteU30(value.Name);
            WriteU30Array((from n in value.KeyArray select n.Index).ToArray(), true);
            WriteU30Array((from n in value.ValueArray select n.Index).ToArray(), false);
        }

        public InstanceInfo ReadInstanceInfo(Abc abc, uint index)
        {
            InstanceInfo result = new InstanceInfo(index);
            result.Name = abc.ConstantPool.GetMultinameAt(ReadU30());
            result.Supername = abc.ConstantPool.GetMultinameAt(ReadU30());

            result.Flags = (InstanceFlags) ReadByte();
            if (result.Flags.HasFlag(InstanceFlags.ProtectedNs))
                result.ProtectedNamespace = abc.ConstantPool.GetNamespaceAt(ReadU30());
            result.InterfaceArray = ReadU30Array(ReadU30());
            result.InstanceInitializer = abc.MethodArray[ReadU30()];
            TraitsInfo[] traits = new TraitsInfo[ReadU30()];
            for (uint i = 0; i < traits.Length; i++)
                traits[i] = ReadTraitsInfo(abc);
            result.TraitsArray = new TraitsArray(traits.ToList());
            return result;
        }

        public void WriteInstanceInfo(InstanceInfo value)
        {
            WriteU30(value.Name.Index);
            WriteU30(value.Supername == null ? 0 : value.Supername.Index);
            WriteByte((Byte) value.Flags);
            if (value.Flags.HasFlag(InstanceFlags.ProtectedNs))
                WriteU30(value.ProtectedNamespace.Index);
            WriteU30Array(value.InterfaceArray, true);
            WriteU30(value.InstanceInitializer.Index);
            WriteU30((uint) value.TraitsArray.Count);
            foreach (TraitsInfo traitsInfo in value.TraitsArray.GetTraits())
                WriteTraitsInfo(traitsInfo);
        }

        public ClassInfo ReadClassInfo(Abc abc, uint index)
        {
            ClassInfo result = new ClassInfo(index);
            result.StaticInitializer = abc.MethodArray[ReadU30()];
            TraitsInfo[] traits = new TraitsInfo[ReadU30()];
            for (uint i = 0; i < traits.Length; i++)
               traits[i] = ReadTraitsInfo(abc);
            result.TraitsArray = new TraitsArray(traits.ToList());
            return result;
        }

        public void WriteClassInfo(ClassInfo value)
        {
            WriteU30(value.StaticInitializer.Index);
            WriteU30((uint)value.TraitsArray.Count);
            foreach (TraitsInfo traitsInfo in value.TraitsArray.GetTraits())
                WriteTraitsInfo(traitsInfo);
        }

        public ScriptInfo ReadScriptInfo(Abc abc, uint index)
        {
            ScriptInfo result = new ScriptInfo(index);
            result.Initializer = ReadU30();
            result.TraitsArray = new TraitsInfo[ReadU30()];
            for (int i = 0; i < result.TraitsArray.Length; i++)
                result.TraitsArray[i] = ReadTraitsInfo(abc);
            return result;
        }

        public void WriteScriptInfo(ScriptInfo value)
        {
            WriteU30(value.Initializer);
            WriteU30((uint)value.TraitsArray.Length);
            foreach (TraitsInfo traitsInfo in value.TraitsArray)
                WriteTraitsInfo(traitsInfo);
        }

        public MethodBodyInfo ReadMethodBodyInfo(Abc abc, uint index)
        {
            MethodBodyInfo result = new MethodBodyInfo(index);
            result.Method = abc.MethodArray[ReadU30()];
            result.MaxStack = ReadU30();
            result.LocalCount = ReadU30();
            result.MinScopeDepth = ReadU30();
            result.MaxScopeDepth = ReadU30();
            result.Code = ReadBytes((int) ReadU30());
            result.ExceptionArray = new ExceptionInfo[ReadU30()];
            for (int i = 0; i < result.ExceptionArray.Length; i++)
                result.ExceptionArray[i] = ReadExceptionInfo();

            TraitsInfo[] traits = new TraitsInfo[ReadU30()];
            for (int i = 0; i < traits.Length; i++)
                traits[i] = ReadTraitsInfo(abc);
            result.TraitsArray = new TraitsArray(traits);
            return result;
        }

        public void WriteMethodBodyInfo(MethodBodyInfo value)
        {
            WriteU30(value.Method.Index);
            WriteU30(value.MaxStack);
            WriteU30(value.LocalCount);
            WriteU30(value.MinScopeDepth);
            WriteU30(value.MaxScopeDepth);
            WriteU30((uint) value.Code.Length);
            WriteBytes(value.Code);
            WriteU30((uint) value.ExceptionArray.Length);
            foreach (ExceptionInfo exceptionInfo in value.ExceptionArray)
                WriteExceptionInfo(exceptionInfo);
            WriteU30((uint)value.TraitsArray.Count);
            foreach (TraitsInfo traitsInfo in value.TraitsArray)
                WriteTraitsInfo(traitsInfo);
        }

        public TraitsInfo ReadTraitsInfo(Abc abc)
        {
            TraitsInfo result = new TraitsInfo();
            result.Name = abc.ConstantPool.GetMultinameAt(ReadU30());
            byte kindAndAttribute = ReadByte();
            result.Kind = (TraitKind)(kindAndAttribute & 0xf);
            result.Attributes = (TraitAttributes)( kindAndAttribute >> 4);

            switch (result.Kind)
            {
                case TraitKind.Slot:
                case TraitKind.Const:
                    var traitField = new TraitField();
                    traitField.SlotId = ReadU30();
                    traitField.TypeName = abc.ConstantPool.GetMultinameAt(ReadU30());
                    traitField.ValueIndex = ReadU30();
                    if (traitField.ValueIndex > 0)
                        traitField.ValueKind = (ValueKind)ReadByte();
                    result.Trait = traitField;
                    break;

                case TraitKind.Method:
                    var traitMethod = new TraitMethod();
                    traitMethod.DispId = ReadU30();
                    traitMethod.Method = abc.MethodArray[ReadU30()];
                    result.Trait = traitMethod;
                    break;

                case TraitKind.Getter:
                    var traitGetter = new TraitGetter();
                    traitGetter.DispId = ReadU30();
                    traitGetter.Method = abc.MethodArray[ReadU30()];
                    result.Trait = traitGetter;
                    break;

                case TraitKind.Setter:
                    var traitSetter = new TraitSetter();
                    traitSetter.DispId = ReadU30();
                    traitSetter.Method = abc.MethodArray[ReadU30()];
                    result.Trait = traitSetter;
                    break;

                case TraitKind.Class:
                    var traitClass = new TraitClass();
                    traitClass.SlotId = ReadU30();
                    traitClass.Class = abc.AbcClassArray[ReadU30()].ClassInfo;
                    result.Trait = traitClass;
                    break;

                case TraitKind.Function:
                    var traitFunction = new TraitFunction();
                    traitFunction.SlotId = ReadU30();
                    traitFunction.Function = abc.MethodArray[ReadU30()];
                    result.Trait = traitFunction;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (result.Attributes.HasFlag(TraitAttributes.Metadata))
                result.MetadataArray = ReadU30Array(ReadU30());
            return result;
        }

        public void WriteTraitsInfo(TraitsInfo value)
        {
            WriteU30(value.Name.Index);
            WriteByte((Byte) ((Byte) (value.Attributes) << 4 | (Byte) value.Kind));

            switch (value.Kind)
            {
                case TraitKind.Slot:
                case TraitKind.Const:
                    TraitField traitField = value.TraitField;
                    WriteU30(traitField.SlotId);
                    WriteU30(traitField.TypeName == null ? 0 : traitField.TypeName.Index);
                    WriteU30(traitField.ValueIndex);
                    if (traitField.ValueIndex > 0)
                        WriteByte((Byte)traitField.ValueKind);
                    break;

                case TraitKind.Method:
                    TraitMethod traitMethod = value.TraitMethod;
                    WriteU30(traitMethod.DispId);
                    WriteU30(traitMethod.Method.Index);
                    break;

                case TraitKind.Getter:
                    TraitGetter traitGetter = value.Trait as TraitGetter;
                    WriteU30(traitGetter.DispId);
                    WriteU30(traitGetter.Method.Index);
                    break;

                case TraitKind.Setter:
                    TraitSetter traitSetter = value.Trait as TraitSetter;
                    WriteU30(traitSetter.DispId);
                    WriteU30(traitSetter.Method.Index);
                    break;

                case TraitKind.Class:
                    TraitClass traitClass = value.Trait as TraitClass;
                    WriteU30(traitClass.SlotId);
                    WriteU30(traitClass.Class.Index);
                    break;

                case TraitKind.Function:
                    TraitFunction traitFunction = value.Trait as TraitFunction;
                    WriteU30(traitFunction.SlotId);
                    WriteU30(traitFunction.Function.Index);
                    break;

                

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (value.Attributes.HasFlag(TraitAttributes.Metadata))
                WriteU30Array(value.MetadataArray, true);
        }

        public ExceptionInfo ReadExceptionInfo()
        {
            ExceptionInfo result = new ExceptionInfo();
            result.From = ReadU30();
            result.To = ReadU30();
            result.Target = ReadU30();
            result.Type = ReadU30();
            result.VariableName = ReadU30();
            return result;
        }

        public void WriteExceptionInfo(ExceptionInfo value)
        {
            WriteU30(value.From);
            WriteU30(value.To);
            WriteU30(value.Target);
            WriteU30(value.Type);
            WriteU30(value.VariableName);
        }

        public static implicit operator byte[](AbcStream stream)
        {
            int pos = stream.Position;
            stream.Position = 0;
            byte[] result = stream.ReadToEnd();
            stream.Position = pos;
            return result;
        }

        public static implicit operator AbcStream(byte[] data)
        {
            AbcStream stream = new AbcStream();
            stream.WriteBytes(data);
            stream.Position = 0;
            return stream;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        ~AbcStream()
        {
            Dispose();
        }
    }
}