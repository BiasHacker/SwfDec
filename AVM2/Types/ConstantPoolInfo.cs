using System;
using System.Collections.Generic;
using System.Linq;
using SwfDec.AVM2.Types.Extended;
using SwfDec.AVM2.Types.Multinames;

namespace SwfDec.AVM2.Types
{
    public class ConstantPoolInfo : IBrowseable
    {
        private List<int> IntArray;
        private List<uint> UIntArray;
        private List<double> DoubleArray;
        private List<StringInfo> StringArray;
        private List<NamespaceInfo> NamespaceArray;
        private List<NamespaceSetInfo> NamespaceSetArray;
        private List<MultinameInfo> MultinameArray;

        private Dictionary<int, uint> IntIndexes;
        private Dictionary<uint, uint> UIntIndexes;
        private Dictionary<double, uint> DoubleIndexes;
        private Dictionary<StringInfo, uint> StringIndexes;
        private Dictionary<NamespaceInfo, uint> NamespaceIndexes;
        private Dictionary<NamespaceSetInfo, uint> NamespaceSetIndexes;
        private Dictionary<MultinameInfo, uint> Multinames;

        #region GetCollection

        public int[] GetIntArray()
        {
            return (from n in IntArray select n).ToArray();
        }

        public uint[] GetUIntArray()
        {
            return (from n in UIntArray select n).ToArray();
        }

        public double[] GetDoubleArray()
        {
            return (from n in DoubleArray select n).ToArray();
        }

        public StringInfo[] GetStringArray()
        {
            return (from n in StringArray select n).ToArray();
        }

        public NamespaceInfo[] GetNamespaceArray()
        {
            return (from n in NamespaceArray select n).ToArray();
        }

        public NamespaceSetInfo[] GetNamespaceSetArray()
        {
            return (from n in NamespaceSetArray select n).ToArray();
        }

        public MultinameInfo[] GetMultinameArray()
        {
            return (from n in MultinameArray select n).ToArray();
        }

        #endregion

        public uint IntArrayLength => (uint)IntArray.Count + 1;

        public uint IntArrayCapacity
        {
            get { return (uint)IntArray.Capacity; }
            set { IntArray.Capacity = (int)value; }
        }

        public uint UIntArrayLength => (uint)UIntArray.Count + 1;

        public uint UIntArrayCapacity
        {
            get { return (uint)UIntArray.Capacity; }
            set { UIntArray.Capacity = (int)value; }
        }

        public uint StringArrayLength => (uint)StringArray.Count + 1;

        public uint StringArrayCapacity
        {
            get { return (uint)StringArray.Capacity; }
            set { StringArray.Capacity = (int)value; }
        }

        public uint DoubleArrayLength => (uint)DoubleArray.Count + 1;

        public uint DoubleArrayCapacity
        {
            get { return (uint)DoubleArray.Capacity; }
            set { DoubleArray.Capacity = (int)value; }
        }

        public uint NamespaceArrayLength => (uint)NamespaceArray.Count + 1;

        public uint NamespaceArrayCapacity
        {
            get { return (uint)NamespaceArray.Capacity; }
            set { NamespaceArray.Capacity = (int)value; }
        }

        public uint NamespaceSetArrayLength => (uint)NamespaceSetArray.Count + 1;

        public uint NamespaceSetArrayCapacity
        {
            get { return (uint)NamespaceSetArray.Capacity; }
            set { NamespaceSetArray.Capacity = (int)value; }
        }

        public uint MultinameArrayLength => (uint)MultinameArray.Count + 1;

        public uint MultinameArrayCapacity
        {
            get { return (uint)MultinameArray.Capacity; }
            set { MultinameArray.Capacity = (int)value; }
        }

        public ConstantPoolInfo()
        {
            IntArray = new List<int>();
            UIntArray = new List<uint>();
            DoubleArray = new List<double>();
            StringArray = new List<StringInfo>();
            NamespaceArray = new List<NamespaceInfo>();
            NamespaceSetArray = new List<NamespaceSetInfo>();
            MultinameArray = new List<MultinameInfo>();

            IntIndexes = new Dictionary<int, uint>();
            UIntIndexes = new Dictionary<uint, uint>();
            DoubleIndexes = new Dictionary<double, uint>();
            StringIndexes = new Dictionary<StringInfo, uint>();
            NamespaceIndexes = new Dictionary<NamespaceInfo, uint>();
            NamespaceSetIndexes = new Dictionary<NamespaceSetInfo, uint>();
            Multinames = new Dictionary<MultinameInfo, uint>();
        }

        ~ConstantPoolInfo()
        {
            IntArray = null;
            UIntArray = null;
            DoubleArray = null;
            StringArray = null;
            NamespaceArray = null;
            NamespaceSetArray = null;
            MultinameArray = null;

            IntIndexes = null;
            UIntIndexes = null;
            DoubleIndexes = null;
            StringIndexes = null;
            NamespaceIndexes = null;
            NamespaceSetIndexes = null;
            Multinames = null;
        }

        #region AddValueAtEnd
        public StringInfo AddStringAtEnd(StringInfo value)
        {
            uint indexOf = IndexOfString(value);
            if (IndexOfString(value) > 0)
            {
                return GetStringAt(indexOf);
            }
            uint s = (uint)StringIndexes.Count;
            StringInfo info = new StringInfo(s + 1, value);
            StringArray.Add(info);
            StringIndexes[info] = s;
            return info;
        }

        public uint AddIntAtEnd(int value)
        {
            uint s = (uint)IntIndexes.Count;
            IntArray.Add(value);
            IntIndexes[value] = s;
            return s;
        }

        public uint AddUIntAtEnd(uint value)
        {
            uint s = (uint)UIntIndexes.Count;
            UIntArray.Add(value);
            UIntIndexes[value] = s;
            return s;
        }

        public uint AddDoubleAtEnd(double value)
        {
            uint s = (uint)DoubleIndexes.Count;
            DoubleArray.Add(value);
            DoubleIndexes[value] = s;
            return s;
        }
        #endregion

        #region SetAt
        public void SetIntAt(int value, uint index)
        {
            index--;
            if (IntArray.Count > index && IntIndexes.ContainsKey(IntArray[(int)index]))
                IntIndexes.Remove(IntArray[(int) index]);

            if (IntArray.Count == index)
                IntArray.Add(value);
            else
                IntArray[(int) index] = value;
            IntIndexes[value] = index;
        }

        public void SetUIntAt(uint value, uint index)
        {
            index--;
            if (UIntArray.Count > index && UIntIndexes.ContainsKey(UIntArray[(int)index]))
                UIntIndexes.Remove(UIntArray[(int)index]);

            if (UIntArray.Count == index)
                UIntArray.Add(value);
            else
                UIntArray[(int)index] = value;
            UIntIndexes[value] = index;
        }

        public void SetDoubleAt(double value, uint index)
        {
            index--;
            if (DoubleArray.Count > index && DoubleIndexes.ContainsKey(DoubleArray[(int)index]))
                DoubleIndexes.Remove(DoubleArray[(int)index]);

            if (DoubleArray.Count == index)
                DoubleArray.Add(value);
            else
                DoubleArray[(int)index] = value;
            DoubleIndexes[value] = index;
        }

        public void SetStringAt(StringInfo value, uint index)
        {
            index--;
            if (StringArray.Count > index && StringIndexes.ContainsKey(StringArray[(int) index]))
                StringIndexes.Remove(StringArray[(int) index]);

            if (StringArray.Count == index)
                StringArray.Add(value);
            else
                StringArray[(int) index] = value;
            StringIndexes[value] = index;
        }

        public void SetNamespaceAt(NamespaceInfo value, uint index)
        {
            index--;
            if (NamespaceArray.Count > index && NamespaceIndexes.ContainsKey(NamespaceArray[(int)index]))
                NamespaceIndexes.Remove(NamespaceArray[(int)index]);

            if (NamespaceArray.Count == index)
                NamespaceArray.Add(value);
            else
                NamespaceArray[(int)index] = value;
            NamespaceIndexes[value] = index;
        }

        public void SetNamespaceSetAt(NamespaceSetInfo value, uint index)
        {
            index--;
            if (NamespaceSetArray.Count > index && NamespaceSetIndexes.ContainsKey(NamespaceSetArray[(int)index]))
                NamespaceSetIndexes.Remove(NamespaceSetArray[(int)index]);

            if (NamespaceSetArray.Count == index)
                NamespaceSetArray.Add(value);
            else
                NamespaceSetArray[(int)index] = value;
            NamespaceSetIndexes[value] = index;
        }

        public void SetMultinameAt(MultinameInfo value, uint index)
        {
            index--;
            if (MultinameArray.Count > index && Multinames.ContainsKey(MultinameArray[(int)index]))
                Multinames.Remove(MultinameArray[(int)index]);

            if (MultinameArray.Count == index)
                MultinameArray.Add(value);
            else
                MultinameArray[(int)index] = value;
            Multinames[value] = index;
        }
        #endregion

        #region GetAt
        public int GetIntAt(uint index)
        {
            return IntArray[(int) index - 1];
        }

        public uint GetUIntAt(uint index)
        {
            return UIntArray[(int)index - 1];
        }

        public double GetDoubleAt(uint index)
        {
            return DoubleArray[(int)index - 1];
        }

        public StringInfo GetStringAt(uint index)
        {
            if (index == 0)
                return new StringInfo(0, null);

            return StringArray[(int)index - 1];
        }

        public NamespaceInfo GetNamespaceAt(uint index)
        {
//if (index == 0)
                //throw new Exception();
            return NamespaceArray[(int)index - 1];
        }

        public NamespaceSetInfo GetNamespaceSetAt(uint index)
        {
            if (index == 0)
                throw new Exception();
            return NamespaceSetArray[(int)index - 1];
        }

        public MultinameInfo GetMultinameAt(uint index)
        {
            if (index == 0)
                return null;
            return MultinameArray[(int)index - 1];
        }
        #endregion

        #region Replace

        public void Replace(StringInfo value1, StringInfo value2)
        {
            uint indexOf = IndexOfString(value1) - 1;
            StringArray[(int)indexOf] = value2;
            StringIndexes.Remove(value1);
            StringIndexes[value2] = indexOf;
        }
        #endregion

        #region IndexOf
        public uint IndexOfInt(int value)
        {
            if (IntIndexes.ContainsKey(value))
                return IntIndexes[value] + 1;
            return 0;
        }

        public uint IndexOfUInt(uint value)
        {
            if (UIntIndexes.ContainsKey(value))
                return UIntIndexes[value] + 1;
            return 0;
        }

        public uint IndexOfDouble(double value)
        {
            if (DoubleIndexes.ContainsKey(value))
                return DoubleIndexes[value] + 1;
            return 0;
        }

        public uint IndexOfString(StringInfo value)
        {
            if (value == null)
                return 0;
            if (StringIndexes.ContainsKey(value))
                return StringIndexes[value] + 1;
            return 0;
        }

	    public uint IndexOfNamespace(NamespaceInfo value)
	    {
		    if (NamespaceIndexes.ContainsKey(value))
			    return NamespaceIndexes[value] + 1;
		    return 0;
	    }
        #endregion

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}