using System;
using System.Linq;
using SwfDec.AVM2.Types.Extended;

namespace SwfDec.AVM2.Types.Multinames
{
    public class MKGenericName : MultinameData, IBrowseable
    {
	    public MultinameInfo TypeDefinition => _cPool.GetMultinameAt(TypeDefinitionIndex);
        public uint TypeDefinitionIndex;

	    public MultinameInfo[] ParamArray => (from n in ParamArrayIndexes select _cPool.GetMultinameAt(n)).ToArray();
        public uint[] ParamArrayIndexes;

        public MKGenericName()
        {
        }
        public MKGenericName(ConstantPoolInfo cPool)
            :base(cPool)
        {
	        TypeDefinitionIndex = 1;
            ParamArrayIndexes = new uint[0];
        }

        public new object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
