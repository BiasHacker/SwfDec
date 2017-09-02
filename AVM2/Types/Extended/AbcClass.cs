using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SwfDec.AVM2.ByteCode.Instructions.Call;
using SwfDec.AVM2.ByteCode.Instructions.Construct;
using SwfDec.AVM2.ByteCode.Instructions.Find;
using SwfDec.AVM2.ByteCode.Instructions.Get;
using SwfDec.AVM2.ByteCode.Instructions.Set;
using SwfDec.AVM2.Types.Multinames;
using SwfDec.AVM2.Types.Traits;

namespace SwfDec.AVM2.Types.Extended
{
    public class AbcClass : IBrowseable
    {
        public Abc Abc { get; private set; }
        public ClassInfo ClassInfo { get; set; }
        public InstanceInfo InstanceInfo { get; set; }

        public string Name => InstanceInfo.Name.MKQName.Name == "" ? "[empty]" : @InstanceInfo.Name.MKQName.Name;

        public string SuperName => InstanceInfo.Supername?.MKQName.Name;

        public string Package => InstanceInfo.Name.MKQName.Namespace.Name;

        public string FullName => (Package == "" ? Name : Package + '.' + Name);

        public uint Index { get; private set; }

        public MethodInfo InstanceInitializer => InstanceInfo.InstanceInitializer;

        public AbcClass(Abc abc, uint index)
        {
            Abc = abc;
            Index = index;
        }

        public IEnumerable<TraitsInfo> GetInstanceTraitsByKind(TraitKind kind)
        {
            return InstanceInfo.TraitsArray.GetTraitsByKind(kind);
        }

        public TraitsInfo[] GetClassTraitsByKind(TraitKind kind)
        {
            return ClassInfo.TraitsArray.GetTraitsByKind(kind);
        }

        public TraitsInfo GetTraitByName(TraitKind kind, string name)
        {
            foreach (TraitsInfo t in ClassInfo.TraitsArray.GetTraitsByKind(kind).Where(t => t.Name.MKQName.Name == name))
                return t;

            return InstanceInfo.TraitsArray.GetTraitsByKind(kind).FirstOrDefault(t => t.Name.MKQName.Name == name);
        }

        public TraitsInfo GetTraitByName(As3GetProperty name)
        {
            return GetTraitByName(name.Multiname.MKQName.Name);
        }

        public TraitsInfo GetTraitByName(As3InitProperty name)
        {
            return GetTraitByName(name.Multiname.MKQName.Name);
        }

        public TraitsInfo GetTraitByName(As3SetProperty name)
        {
            return GetTraitByName(name.Multiname.MKQName.Name);
        }

        public TraitsInfo GetTraitByName(As3CallProperty name)
        {
            return GetTraitByName(name.Multiname.MKQName.Name);
        }

        public TraitsInfo GetTraitByName(As3FindProperty name)
        {
            return GetTraitByName(name.Multiname.MKQName.Name);
        }

        public TraitsInfo GetTraitByName(string name)
        {
            foreach (TraitsInfo t in ClassInfo.TraitsArray.Where(t => t.Name.MKQName.Name == name))
            {
                return t;
            }

            return InstanceInfo.TraitsArray.FirstOrDefault(t => t.Name.MKQName.Name == name);
        }

        public string GetModifiers()
        {
            InstanceFlags flags = InstanceInfo.Flags;
            string modifiers = "";
            if (flags.HasFlag(InstanceFlags.ProtectedNs))
                modifiers += "protected ";
            if (flags.HasFlag(InstanceFlags.Sealed))
                modifiers += "sealed ";
            if (flags.HasFlag(InstanceFlags.Final))
                modifiers += "final ";
            if (flags.HasFlag(InstanceFlags.Interface))
                modifiers += "interface";
            else
                modifiers += "class";

            return modifiers;
        }

        public void ChangeName(string newName)
        {
            InstanceInfo.Name.MKQName.Name.String = newName;
        }

        public object this[String name]
        {
            get
            {
                return PropertyHelper.GetProperty(name, this);
            }
            set
            {
                PropertyHelper.SetProeprty(name, this, value);
            }
        }
    }
}
