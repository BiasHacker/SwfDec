using System;
using System.Runtime.InteropServices;
namespace SwfDec.Tags
{
    public class BaseTag
    {
        private byte[] _data;

        protected short Code;

        public BaseTag(short code, byte[] data)
        {
            Code = code;
            DecompileBody(data);
        }

        protected virtual void DecompileBody(byte[] data)
        {
            _data = data;
        }

        protected virtual byte[] CompileBody()
        {
            return _data;
        }

        public byte[] Compile()
        {
            SwfStream stream = new SwfStream();

            byte[] body = CompileBody();
            if (body.Length >= 63)
            {
                stream.WriteShort((short)((Code << 6) | 63));
                stream.WriteInt(body.Length);
            }
            else
                stream.WriteShort((short)((Code << 6) | body.Length));

            stream.WriteBytes(body);

            return stream;
        }
    }
}
