using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zlib;
using SevenZip.Compression.LZMA;
using SwfDec.Tags;
using SwfDec.Types;

namespace SwfDec
{
    public class Swf
    {
        public String Signature { get; private set; }
        public Byte Version { get; private set; }
        public int Length { get; private set; }
        public Rect FrameSize { get; private set; }
        public double FrameRate { get; private set; }
        public short FrameCount { get; private set; }

        public List<BaseTag> TagList { get; private set; }
        public List<DoAbcTag> DoAbcTagList { get; private set; }
        public List<SymbolClassTag> SymbolClassTagList { get; private set; }
        public List<DefineBinaryDataTag> DefineBinaryDataTagList { get; private set; }

        public Swf()
        {
            
        }

        public byte[] GetBinaryData(string name)
        {
            foreach (SymbolClassTag t1 in SymbolClassTagList)
            {
                for (int i = 0; i < t1.NameArray.Length; i++)
                {
                    if (t1.NameArray[i] == name)
                    {
                        foreach (DefineBinaryDataTag t in DefineBinaryDataTagList)
                        {
                            if (t.Tag == t1.TagArray[i])
                                return t.Data;
                        }
                    }
                }
            }

            return null;
        }

        public void SetBinaryData(string name, byte[] data)
        {
            foreach (SymbolClassTag t1 in SymbolClassTagList)
            {
                for (int i = 0; i < t1.NameArray.Length; i++)
                {
                    if (t1.NameArray[i] == name)
                    {
                        foreach (DefineBinaryDataTag t in DefineBinaryDataTagList)
                        {
                            if (t.Tag == t1.TagArray[i])
                                t.Data = data;
                        }
                    }
                }
            }
        }

        public string GetSymbolName(short tag)
        {
            for (int k = 0; k < SymbolClassTagList.Count; k++)
            {
                for (int i = 0; i < SymbolClassTagList[k].NameArray.Length; i++)
                {
                    if (SymbolClassTagList[k].TagArray[i] == tag)
                    {
                        return SymbolClassTagList[k].NameArray[i];
                    }
                }
            }

            throw new Exception();
        }

        public void ChangeSymbolName(string oldString, string newString)
        {
            foreach (var symbolClassTag in SymbolClassTagList)
            {
                for (int index = 0; index < symbolClassTag.NameArray.Length; index++)
                {
                    var entry = symbolClassTag.NameArray[index];
                    if (entry == oldString)
                        symbolClassTag.NameArray[index] = newString;
                }
            }
        }

        public void Decompile(Byte[] data)
        {
            SwfStream stream = data;

            Signature = stream.ReadStringBytes(3);
            Version = stream.ReadByte();
            Length = stream.ReadInt();
			
            int startTime = Environment.TickCount;
            if (Signature == "CWS")
            {
                stream = ZlibStream.UncompressBuffer(stream.ReadToEnd());
                Signature = "FWS";
            }

			if (Signature == "ZWS")
			{
				Signature = "FWS";
				int length = stream.ReadInt();
				byte[] properties = stream.ReadBytes(5);
				byte[] compressed = stream.ReadBytes(length);
				Decoder coder = new Decoder();
				MemoryStream output = new MemoryStream(Length - 8);

				coder.SetDecoderProperties(properties);
				coder.Code(new MemoryStream(compressed), output, length, Length - 8, null);
				byte[] d = new byte[Length - 8];
				output.Position = 0;
				output.Read(d, 0, Length - 8);
				stream = d;
			}

            int time = Environment.TickCount - startTime;
 
            if (Signature != "FWS" )
                throw new Exception("Unsupported signature: " + Signature);

            FrameSize = stream.ReadRect();
            FrameRate = stream.ReadFixed8();
            FrameCount = stream.ReadShort();

            TagList = new List<BaseTag>();
            DoAbcTagList = new List<DoAbcTag>();
            SymbolClassTagList = new List<SymbolClassTag>();
            DefineBinaryDataTagList = new List<DefineBinaryDataTag>();

            

            while (stream.Position < stream.Length)
            {
                short tagHeader = stream.ReadShort();
                short code = (short)(tagHeader >> 6);
                int length = tagHeader & 63;
                if (length == 63)
                    length = stream.ReadInt();
                byte[] tagBody = stream.ReadBytes(length);

                BaseTag tag = null;
                switch (code)
                {
                    case 82:
                        tag = new DoAbcTag(tagBody);
                        DoAbcTagList.Add(tag as DoAbcTag);
                        break;

                    case 76:
                        tag = new SymbolClassTag(tagBody);
                        SymbolClassTagList.Add(tag as SymbolClassTag);
                        break;

                    case 87:
                        tag = new DefineBinaryDataTag(tagBody);
                        DefineBinaryDataTagList.Add(tag as DefineBinaryDataTag);
                        break;

                    default:
                        tag = new BaseTag(code, tagBody);
                        break;
                }
                TagList.Add(tag);
            }
        }

        public byte[] Compile()
        {
            SwfStream stream = new SwfStream();

            stream.WriteStringBytes(Signature);
            stream.WriteByte(Version);
            stream.WriteInt(0);
            stream.WriteRect(FrameSize);
            stream.WriteFixed8(FrameRate);
            stream.WriteShort(FrameCount);
            foreach (BaseTag baseTag in TagList)
                stream.WriteBytes(baseTag.Compile());

            stream.Position = 4;
            stream.WriteInt(stream.Length);
            return stream;
        }
    }
}
