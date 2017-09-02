using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwfDec.Tags
{
    public class DefineBinaryDataTag : BaseTag
    {
        public short Tag { get; set; }
        public Byte[] Data { get; set; }

        public DefineBinaryDataTag(byte[] data)
            :base(87, data)
        {
        }

        protected override void DecompileBody(byte[] data)
        {
            SwfStream stream = data;
            Tag = stream.ReadShort();
            stream.ReadBytes(4);
            Data = stream.ReadToEnd();
        }

        protected override byte[] CompileBody()
        {
            SwfStream stream = new SwfStream();
            stream.WriteShort(Tag);
            stream.WriteInt(0);
            stream.WriteBytes(Data);
            return stream;
        }

        public override string ToString() {
            return $"DefineBinaryDataTag Tag:{Tag} Length:{Data.Length}";
        }
    }
}
