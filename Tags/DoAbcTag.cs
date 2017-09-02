using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfDec.AVM2;

namespace SwfDec.Tags
{
    public enum DoAbcTagFlags : int
    {
        None = 0,
        DoAbcLazyInitializeFlag = 1
    }
    public sealed class DoAbcTag : BaseTag
    {
        public DoAbcTagFlags Flags { get;  set; }
        public string Name { get; set; }
        public Abc AbcData { get; set; }

        public DoAbcTag(byte[] data)
            :base(82, data)
        {
        }

        protected override void DecompileBody(byte[] data)
        {
            SwfStream stream = data;
            Flags = (DoAbcTagFlags)stream.ReadInt();
            Name = stream.ReadString();
            AbcData = new Abc();
            AbcData.Decompile(stream.ReadToEnd());
        }

        protected override byte[] CompileBody()
        {
            SwfStream stream = new SwfStream();
            stream.WriteInt((int)Flags);
            stream.WriteString(Name);
            byte[] abcData = AbcData.Compile();
            stream.WriteBytes(abcData);
            byte[] result = stream;
            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
