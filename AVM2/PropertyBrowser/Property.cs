using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwfDec.AVM2.Types;
using SwfDec.AVM2.Types.Extended;
using SwfDec.AVM2.Types.Multinames;
using SwfDec.AVM2.Types.Traits;

namespace SwfDec.AVM2.PropertyBrowser
{
    public class Property
    {
        public String Type { get; set; }

        public List<String> PropertyNames { get; set; } 
        public Dictionary<string, Property> Properties { get; set; }
                       

        public Property(string type)
        {   Type = type;
            PropertyNames = new List<String>();
            Properties = new Dictionary<String, Property>();
        }

        public void AddProperty(string name, Property property)
        {
            PropertyNames.Add(name);
            Properties[name] = property;
        }
                     
        public static implicit operator Property(AbcClass value)
        {
            if (value == null) return null;
            Property result = new Property("AbcClass");
            result.AddProperty("ClassInfo", value.ClassInfo);
            result.AddProperty("InstanceInfo", value.InstanceInfo);

            return result;
        }

        public static implicit operator Property(ClassInfo value)
        {
            if (value == null) return null;
            Property result = new Property("InstanceInfo");
            result.AddProperty("StaticInitializer", value.StaticInitializer);
            result.AddProperty("TraitsArray", value.TraitsArray);
            return result;
        }

        public static implicit operator Property(InstanceInfo value)
        {
            if (value == null) return null;
            Property result = new Property("InstanceInfo");
            result.AddProperty("Name", value.Name);
            result.AddProperty("Supername", value.Supername);
            result.AddProperty("Flags", value.Flags);
            result.AddProperty("ProtectedNamespace", value.ProtectedNamespace);
            result.AddProperty("InterfaceArray", value.InterfaceArray);
            result.AddProperty("InstanceInitializer", value.InstanceInitializer);
            return result;
        }

        public static implicit operator Property(MethodInfo value)
        {
            if (value == null) return null;
            Property result = new Property("MethodInfo");
            result.AddProperty("ReturnType", value.ReturnType);
            result.AddProperty("ParamTypeArray", value.ParamTypeArray);
            result.AddProperty("Name", value.Name);
            result.AddProperty("Flags", value.Flags);
            result.AddProperty("OptionalParamArray", value.OptionalParamArray);
            result.AddProperty("ParamNameArray", value.ParamNameArray);
            //result.AddProperty("MethodBody", value.MethodBody);
            return result;
        }

        public static implicit operator Property(MethodBodyInfo value)
        {
            if (value == null) return null;
            Property result = new Property("MethodBodyInfo");
            result.AddProperty("Method", value.Method);
            result.AddProperty("MaxStack", value.MaxStack);
            result.AddProperty("LocalCount", value.LocalCount);
            result.AddProperty("MinScopeDepth", value.MinScopeDepth);
            result.AddProperty("MaxScopeDepth", value.MaxScopeDepth);
            result.AddProperty("ExceptionArray", value.ExceptionArray);
            result.AddProperty("TraitsArray", value.TraitsArray);
            return result;
        }

        public static implicit operator Property(TraitsArray value)
        {
            if (value == null) return null;
            Property result = new Property("TraitsArray");
            for (int i = 0; i < value.Count; i++)
            {
                result.AddProperty($"{i}", value[i]);
            }
            return result;
        }

        public static implicit operator Property(TraitsInfo value)
        {
            if (value == null) return null;
            Property result = new Property("TraitsInfo");
            result.AddProperty("Name", value.Name);
            result.AddProperty("Attributes", value.Attributes);
            result.AddProperty("Kind", value.Kind);
            switch (value.Kind)
            {
                case TraitKind.Slot:
                case TraitKind.Const:
                    result.AddProperty("TraitField", value.TraitField);
                    break;

                case TraitKind.Method:
                case TraitKind.Getter:
                case TraitKind.Setter:
                case TraitKind.Function:
                    result.AddProperty("TraitMethod", value.TraitMethod);
                    break;

                case TraitKind.Class:
                    result.AddProperty("TraitClass", value.TraitClass);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            result.AddProperty("MetadataArray", value.MetadataArray);
            return result;
        }

        public static implicit operator Property(TraitField value)
        {
            if (value == null) return null;
            Property result = new Property("TraitField");
            result.AddProperty("SlotId", value.SlotId);
            result.AddProperty("TypeName", value.TypeName);
            result.AddProperty("ValueIndex", value.ValueIndex);
            result.AddProperty("ValueKind", value.ValueKind);
            return result;
        }

        public static implicit operator Property(TraitClass value)
        {
            if (value == null) return null;
            Property result = new Property("TraitField");
            result.AddProperty("SlotId", value.SlotId);
            result.AddProperty("Class", value.Class);
            return result;
        }

        public static implicit operator Property(TraitMethod value)
        {
            if (value == null) return null;
            Property result = new Property("TraitField");
            result.AddProperty("DispId", value.DispId);
            result.AddProperty("Method", value.Method);
            return result;
        }

        public static implicit operator Property(ExceptionInfo[] value)
        {
            if (value == null) return null;
            Property result = new Property("ExceptionInfo[]");
            for (int i = 0; i < value.Length; i++)
            {
                result.AddProperty($"{i}", value[i]);
            }
            return result;
        }

        public static implicit operator Property(ExceptionInfo value)
        {
            if (value == null) return null;
            Property result = new Property("ExceptionInfo");
            result.AddProperty("From", value.From);
            result.AddProperty("To", value.To);
            result.AddProperty("Target", value.Target);
            result.AddProperty("Type", value.Type);
            result.AddProperty("VariableName", value.VariableName);
            return result;
        }

        public static implicit operator Property(OptionalParam[] value)
        {
            if (value == null) return null;
            Property result = new Property("int[]");
            for (int i = 0; i < value.Length; i++)
            {
                result.AddProperty($"{i}", value[i]);
            }
            return result;
        }

        public static implicit operator Property(OptionalParam value)
        {
            if (value == null) return null;
            Property result = new Property("int[]");
            result.AddProperty("Kind", value.Kind);
            result.AddProperty("Value", value.Value);
            return result;
        }

        public static implicit operator Property(uint[] value)
        {
            if (value == null) return null;
            Property result = new Property("int[]");
            for (int i = 0; i < value.Length; i++)
            {
                result.AddProperty($"{i}", value[i]);
            }
            return result;
        }

        public static implicit operator Property(uint value)
        {
            Property result = new Property("int");
            return result;
        }

        public static implicit operator Property(MultinameInfo value)
        {
            if (value == null) return null;
            Property result = new Property("MultinameInfo");
            result.AddProperty("Kind", value.Kind);
            switch (value.Kind)
            {
                case MultinameKind.QName:
                case MultinameKind.QNameA:
                    result.AddProperty("MKQName", value.MKQName);
                    break;

                case MultinameKind.RTQName:
                case MultinameKind.RTQNameA:

                    result.AddProperty("MKRTQName", value.MKRTQName);
                    break;

                case MultinameKind.RTQNameL:
                case MultinameKind.RTQNameLA:
                    result.AddProperty("MKRTQNameL", value.MKRTQNameL);
                    break;

                case MultinameKind.Multiname:
                case MultinameKind.MultinameA:
                    result.AddProperty("MKMultiname", value.MKMultiname);
                    break;

                case MultinameKind.MultinameL:
                case MultinameKind.MultinameLA:
                    result.AddProperty("MKMultinameL", value.MKMultinameL);
                    break;

                case MultinameKind.GenericName:
                    result.AddProperty("MKGenericName", value.MKGenericName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return result;
        }

        public static implicit operator Property(MKQName value)
        {
            if (value == null) return null;
            Property result = new Property("MKQName");
            result.AddProperty("Namespace", value.Namespace);
            result.AddProperty("Name", value.Name);
            return result;
        }

        public static implicit operator Property(MKRTQName value)
        {
            if (value == null) return null;
            Property result = new Property("MKRTQName");
            result.AddProperty("Name", value.Name);
            return result;
        }

        public static implicit operator Property(MKRTQNameL value)
        {
            if (value == null) return null;
            Property result = new Property("MKRTQNameL");
            return result;
        }

        public static implicit operator Property(MKMultiname value)
        {
            if (value == null) return null;
            Property result = new Property("MKMultiname");
            result.AddProperty("Name", value.Name);
            result.AddProperty("NamespaceSet", value.NamespaceSet);
            return result;
        }

        public static implicit operator Property(MKMultinameL value)
        {
            if (value == null) return null;
            Property result = new Property("MKMultinameL");
            result.AddProperty("NamespaceSet", value.NamespaceSet);
            return result;
        }

        public static implicit operator Property(MKGenericName value)
        {
            if (value == null) return null;
            Property result = new Property("MKGenericName");
            result.AddProperty("TypeDefinition", value.TypeDefinition);
            result.AddProperty("ParamArray", value.ParamArray);
            return result;
        }

        public static implicit operator Property(MultinameInfo[] value)
        {
            if (value == null) return null;
            Property result = new Property("MultinameInfo[]");
            for (int i = 0; i < value.Length; i++)
            {
                result.AddProperty($"{i}", value[i]);
            }
            return result;
        }

        public static implicit operator Property(Enum value)
        {
            if (value == null) return null;
            Property result = new Property("Enum");
            return result;
        }

        public static implicit operator Property(NamespaceSetInfo value)
        {
            if (value == null) return null;
            Property result = new Property("NamespaceSetInfo");
            result.AddProperty("NamespaceArray", value.NamespaceArray);
            return result;
        }

        public static implicit operator Property(NamespaceInfo[] value)
        {
            if (value == null) return null;
            Property result = new Property("NamespaceInfo[]");
            for (int i = 0; i < value.Length; i++)
            {
                result.AddProperty($"{i}", value[i]);
            }
            return result;
        }

        public static implicit operator Property(NamespaceInfo value)
        {
            if (value == null) return null;
            Property result = new Property("NamespaceInfo");
            result.AddProperty("Kind", value.Kind);
            result.AddProperty("Name", value.Name);
            return result;
        }

        public static implicit operator Property(StringInfo value)
        {
            if (value == null) return null;
            Property result = new Property("StringInfo");
            result.AddProperty("String", value.String);
            return result;
        }

        public static implicit operator Property(String value)
        {
            if (value == null) return null;Property result = new Property("String");
            return result;
        }
    }
}
