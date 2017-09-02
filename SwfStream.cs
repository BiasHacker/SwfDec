using System.Collections;
using SwfDec.Types;
using System;
using System.IO;
using System.Text;

namespace SwfDec
{
    public sealed class SwfStream : IDisposable
    {
        private readonly MemoryStream _stream;

        public int Position
        {
            get { return (int) _stream.Position; }

            set { _stream.Position = value; }
        }

        public int Length => (int) _stream.Length;

        public SwfStream()
        {
            _stream = new MemoryStream();
        }

        public Byte[] ReadBytes(int count)
        {
            Byte[] result = new Byte[count];
            _stream.Read(result, 0, count);
            return result;
        }

        public void WriteBytes(Byte[] buffer)
        {
            _stream.Write(buffer, 0, buffer.Length);
        }

        public Byte[] ReadReversedBytes(int count)
        {
            Byte[] result = ReadBytes(count);
            Array.Reverse(result);
            return result;
        }

        public void WriteReversedBytes(Byte[] buffer)
        {
            Array.Reverse(buffer);
            WriteBytes(buffer);
        }

        public Byte ReadByte()
        {
            return (Byte) _stream.ReadByte();
        }

        public void WriteByte(Byte value)
        {
            _stream.WriteByte(value);
        }

        public short ReadShort()
        {
            return BitConverter.ToInt16(ReadBytes(2), 0);
        }

        public void WriteShort(short value)
        {
            WriteBytes(BitConverter.GetBytes(value));
        }

        public int ReadInt()
        {
            return BitConverter.ToInt32(ReadBytes(4), 0);
        }

        public void WriteInt(int value)
        {
            WriteBytes(BitConverter.GetBytes(value));
        }

        public String ReadString()
        {
            String result = "";
            Byte b;
            while ((b = ReadByte()) != 0)
            {
                result += Encoding.UTF8.GetString(new byte[] {b});
            } 
            return result;
        }

        public void WriteString(String value)
        {
            WriteBytes(Encoding.UTF8.GetBytes(value));
            WriteByte(0);
        }

        public String ReadStringBytes(int count)
        {
            return Encoding.UTF8.GetString(ReadBytes(count));
        }

        public void WriteStringBytes(String value)
        {
            WriteBytes(Encoding.UTF8.GetBytes(value));
        }

        public Byte[] ReadToEnd()
        {
            return ReadBytes(Length - Position);
        }

        public Rect ReadRect()
        {
            Rect result = new Rect();

            int nBitcount = 0, nCurrentValue = 0, nCurrentBit = 2;
            byte byTemp = ReadByte();
            byte mByNbits = (byte) (byTemp >> 3);
            byTemp &= 7;
            byTemp <<= 5;
            for (int nIndex = 0; nIndex < 4; nIndex++)
            {
                while (nBitcount < mByNbits)
                {
                    if ((byTemp & 128) == 128)
                    {
                        nCurrentValue += 1 << (mByNbits - nBitcount - 1);
                    }
                    byTemp <<= 1;
                    byTemp &= 255;
                    nCurrentBit--;
                    nBitcount++;
                    if (nCurrentBit < 0)
                    {
                        byTemp = ReadByte();
                        nCurrentBit = 7;
                    }
                }

                switch (nIndex)
                {
                    case 0:
                        result.XMin = nCurrentValue;
                        break;
                    case 1:
                        result.XMax = nCurrentValue;
                        break;
                    case 2:
                        result.YMin = nCurrentValue;
                        break;
                    case 3:
                        result.YMax = nCurrentValue;
                        break;
                }
                nBitcount = 0;
                nCurrentValue = 0;
            }

            return result;
        }

        public void WriteRect(Rect value)
        {
            int[] values = new int[4];
            int highest = 0;
            int highestTemp = 0;
            String[] bits = new String[4];
            String final = "";
            values[0] = value.XMin;
            values[1] = value.XMax;
            values[2] = value.YMin;
            values[3] = value.YMax;

            for (int i = 0; i < 4; i++)
            {
                int val = values[i];
                bits[i] = "";
                do
                {
                    bits[i] = (val & 1).ToString() + bits[i];
                } while ((val >>= 1) > 0);
                if (bits[i].Length > highest)
                    highest = bits[i].Length;
            }
            highest++;
            highestTemp = highest;
            do
            {
                final = (highestTemp & 1).ToString() + final;
            } while ((highestTemp >>= 1) > 0);

            while (final.Length < 5)
            {
                final = '0' + final;
            }

            for (int i = 0; i < 4; i++)
            {
                while (bits[i].Length < highest)
                    bits[i] = '0' + bits[i];
                final += bits[i];
            }

            while((final.Length % 8) != 0)
                final += '0';

            while (final != "")
            {
                WriteByte(Convert.ToByte(final.Substring(0, 8), 2));
                final = final.Substring(8);
            }
        }

        public double ReadFixed8()
        {
            int b = ReadByte();
            double result = ReadByte();
            b = (b & 0xF0) >> 4 | (b & 0x0F) << 4;
            b = (b & 0xCC) >> 2 | (b & 0x33) << 2;
            b = (b & 0xAA) >> 1 | (b & 0x55) << 1;
            result += b/255.0;
            return result;
        }

        public void WriteFixed8(double value)
        {
            byte bb = (byte) value;

            int b = (int) (value - (int) value)*255;
            b = (b & 0xF0) >> 4 | (b & 0x0F) << 4;
            b = (b & 0xCC) >> 2 | (b & 0x33) << 2;
            b = (b & 0xAA) >> 1 | (b & 0x55) << 1;
            WriteByte((byte) b);
            WriteByte((byte) value);
        }

        public static implicit operator byte[](SwfStream stream)
        {
            int pos = stream.Position;
            stream.Position = 0;
            byte[] result = stream.ReadToEnd();
            stream.Position = pos;
            return result;
        }

        public static implicit operator SwfStream(byte[] data)
        {
            SwfStream stream = new SwfStream();
            stream.WriteBytes(data);
            stream.Position = 0;
            return stream;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        ~SwfStream()
        {
            Dispose();
        }
    }
}
