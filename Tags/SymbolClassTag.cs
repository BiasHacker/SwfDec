using System;

namespace SwfDec.Tags
{
    public  sealed class SymbolClassTag : BaseTag
    {
        public short[] TagArray { get; private set; }
        public string[] NameArray { get; private set; }

        public SymbolClassTag(byte[] data)
            : base(76, data)
        {
        }

        protected override void DecompileBody(byte[] data)
        {
            SwfStream stream = data;
            short count = stream.ReadShort();
            TagArray = new short[count];
            NameArray = new string[count];
            for (int i = 0; i < count; i++)
            {
                TagArray[i] = stream.ReadShort();
                NameArray[i] = stream.ReadString();
            }
        }

        protected override byte[] CompileBody()
        {
            SwfStream stream = new SwfStream();

            if (TagArray.Length != NameArray.Length)
                throw new Exception("Names length and Tags length are not equal in SymbolClassTag");

            stream.WriteShort((short) TagArray.Length);
            for (int i = 0; i < TagArray.Length; i++)
            {
                stream.WriteShort(TagArray[i]);
                stream.WriteString(NameArray[i]);
            }
            return stream;
        }
    }
}
