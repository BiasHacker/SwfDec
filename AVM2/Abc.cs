using System;
using System.Collections.Generic;
using System.Linq;
using SwfDec.AVM2.Types;
using SwfDec.AVM2.Types.Extended;
using SwfDec.AVM2.Types.Multinames;
using SwfDec.AVM2.Types.Traits;

namespace SwfDec.AVM2
{
    public sealed class Abc : IBrowseable
    {
        public short MinorVersion { get; set; }
        public short MajorVersion { get; set; }
        public ConstantPoolInfo ConstantPool { get; set; }
        public MethodInfo[] MethodArray { get; set; }
        public MetadataInfo[] MetadataArray { get; set; }

        public AbcClass[] AbcClassArray { get; set; }

        public ScriptInfo[] ScriptArray { get; set; }
        public MethodBodyInfo[] MethodBodyArray { get; set; }

        public Abc()
        {
            MajorVersion = 46;
            MinorVersion = 16;
            ConstantPool = new ConstantPoolInfo();
            MethodArray = new MethodInfo[0];
            MetadataArray = new MetadataInfo[0];
            AbcClassArray = new AbcClass[0];
            ScriptArray = new ScriptInfo[0];
            MethodBodyArray = new MethodBodyInfo[0];
        }

        ~Abc()
        {
            ConstantPool = null;
            MethodArray = null;
            MetadataArray = null;
            ScriptArray = null;
            MethodBodyArray = null;
        }

        public void Decompile(byte[] data)
        {
            AbcStream stream = data;
            MinorVersion = stream.ReadShort();
            MajorVersion = stream.ReadShort();
            ConstantPool = stream.ReadConstantPoolInfo();

            MethodArray = new MethodInfo[stream.ReadU30()];
            for (uint i = 0; i < MethodArray.Length; i++)
                MethodArray[i] = stream.ReadMethodInfo(ConstantPool, i);

            MetadataArray = new MetadataInfo[stream.ReadU30()];
            for (int i = 0; i < MetadataArray.Length; i++)
                MetadataArray[i] = stream.ReadMetadataInfo(ConstantPool);

            AbcClassArray = new AbcClass[stream.ReadU30()];

            for (uint i = 0; i < AbcClassArray.Length; i++)
            {
                AbcClassArray[i] = new AbcClass(this, i);
                AbcClassArray[i].InstanceInfo = stream.ReadInstanceInfo(this, i);
            }
            for (uint i = 0; i < AbcClassArray.Length; i++)
                AbcClassArray[i].ClassInfo = stream.ReadClassInfo(this, i);

            ScriptArray = new ScriptInfo[stream.ReadU30()];
            for (uint i = 0; i < ScriptArray.Length; i++)
                ScriptArray[i] = stream.ReadScriptInfo(this, i);

            MethodBodyArray = new MethodBodyInfo[stream.ReadU30()];
            for (uint i = 0; i < MethodBodyArray.Length; i++)
            {
                MethodBodyArray[i] = stream.ReadMethodBodyInfo(this, i);
                MethodArray[MethodBodyArray[i].Method.Index].MethodBody = MethodBodyArray[i];
            }
        }

        public byte[] Compile()
        {
            AbcStream stream = new AbcStream();
            stream.WriteShort(MinorVersion);
            stream.WriteShort(MajorVersion);
            stream.WriteConstantPool(ConstantPool);
            stream.WriteU30((uint) MethodArray.Length);
            foreach (MethodInfo methodInfo in MethodArray)
                stream.WriteMethodInfo(methodInfo);
            stream.WriteU30((uint) MetadataArray.Length);
            foreach (MetadataInfo metadataInfo in MetadataArray)
                stream.WriteMetadataInfo(metadataInfo);
            stream.WriteU30((uint)AbcClassArray.Length);
            foreach (InstanceInfo instanceInfo in AbcClassArray.Select(x => x.InstanceInfo))
                stream.WriteInstanceInfo(instanceInfo);
            foreach (ClassInfo classInfo in AbcClassArray.Select(x => x.ClassInfo))
                stream.WriteClassInfo(classInfo);
            stream.WriteU30((uint) ScriptArray.Length);
            foreach (ScriptInfo scriptInfo in ScriptArray)
                stream.WriteScriptInfo(scriptInfo);
            stream.WriteU30((uint) MethodBodyArray.Length);
            foreach (MethodBodyInfo methodBodyInfo in MethodBodyArray)
                stream.WriteMethodBodyInfo(methodBodyInfo);
            return stream;
        }

        public uint IndexOfMethodBody(MethodBodyInfo value)
        {
            int index = Array.IndexOf(MethodBodyArray, value, 0);
            if (index == -1) throw new Exception("Couldn't find element in array");
            return (uint) index;
        }

        public int FindClass(string name)
        {
            for (int i = 0; i < AbcClassArray.Length; i++)
            {
                if (AbcClassArray[i].Name == name)
                    return i;
            }

            return -1;
        }

        public int FindClass(MultinameInfo name)
        {
            for (int i = 0; i < AbcClassArray.Length; i++)
            {
                if (AbcClassArray[i].InstanceInfo.Name.Index == name.Index)
                    return i;
            }
            return -1;
        }

        public AbcClass FindClass(TraitDescription[] insTraits, TraitDescription[] staTraits) {
            for (uint i = 0; i < AbcClassArray.Length; i++)
            {
                AbcClass cls = AbcClassArray[i];
                bool ret = true;
                if (insTraits != null)
                    if (insTraits.Length <= cls.InstanceInfo.TraitsArray.Count)
                        for (int j = 0; j < insTraits.Length; j++) {
                            if (!insTraits[j].IsMatch(cls.InstanceInfo.TraitsArray[j]))
                                ret = false;
                        }
                    else {
                        ret = false;
                    }

                if (staTraits != null)
                    if (insTraits.Length <= cls.ClassInfo.TraitsArray.Count)
                        for (int j = 0; j < staTraits.Length; j++)
                        {
                            if (staTraits[j].IsMatch(cls.ClassInfo.TraitsArray[j]))
                                ret = false;
                        }
                    else {
                        ret = false;
                    }
                if (ret)
                    return cls;
            }

            return null;
        }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
