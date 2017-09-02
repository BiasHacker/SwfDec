using System;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types
{
    public class StringInfo : IEquatable<StringInfo>, IBrowseable
    {
        public char this[int index] => String[index];

        public uint Index { get; private set; }
        public String String { get; set; }

        public StringInfo(uint index, string @string)
        {
            Index = index;
            String = @string;
        }

		public StringInfo(string @string)
		{
			String = @string;
		}

        public static implicit operator String(StringInfo stringInfo)
        {
            return stringInfo?.String;
        }

	    public override string ToString()
        {
            return $"StringInfo:{Index}{{{String}}}";
        }

		public bool Equals(StringInfo other)
		{
			return other != null && String == other.String;
		}

	    public override bool Equals(object obj)
	    {
		    return Equals(obj as StringInfo);
	    }

	    public override int GetHashCode()
	    {
	        if (String == null)
	            return -1;
		    return String.GetHashCode();
	    }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
