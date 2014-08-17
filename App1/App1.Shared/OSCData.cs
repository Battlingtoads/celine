using System;
using System.Collections.Generic;
using System.Text;
using gnow.util.osc;
using System.IO;

namespace gnow.util.osc
{

    partial class Utilities
    {
        public static byte[] swapEndian(byte[] data)
        {
            byte[] swapped = new byte[data.Length];
            for(int i = data.Length - 1, j = 0 ; i >= 0 ; i--, j++)
            {
                swapped[j] = data[i];
            }
            return swapped;
        }
    }
    public interface OSCData
    {
        byte[] pack();
        char GetTag();
        OSCData getValue();
    }

    class oscInt : OSCData
    {
        public int value;
        public oscInt(int i) { value = i; }
        public byte[] pack()
        {
            byte[] data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) data = Utilities.swapEndian(data);
            return data;
        }
        public static oscInt unpack(byte[] bytes, ref int start)
        {
			byte[] data = new byte[4];
			for(int i = 0 ; i < 4 ; i++, start++) data[i] = bytes[start];
			if(BitConverter.IsLittleEndian) data = Utilities.swapEndian(data);
			return (oscInt)BitConverter.ToInt32(data, 0);
        }
        public static implicit operator oscInt(int i)
        {
            return new oscInt(i);
        }
        public static implicit operator int(oscInt i)
        {
            return i.value;
        }
        public char GetTag() { return 'i'; }
        public OSCData getValue() { return (oscInt)value; }
        public override string ToString() { return value.ToString(); }
    }
    class oscLong : OSCData
    {
        public long value;
        public oscLong(long l) { value = l; }
        public byte[]  pack()
        {
            byte[] data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) data = Utilities.swapEndian(data);
            return data;
        }
        public static oscLong unpack(byte[] bytes, ref int start)
        {
			byte[] data = new byte[8];
			for(int i = 0 ; i < 8 ; i++, start++) data[i] = bytes[start];
			if(BitConverter.IsLittleEndian) data = Utilities.swapEndian(data);
			return (oscLong)BitConverter.ToInt64(data, 0);
        }
        public static implicit operator oscLong(long i)
        {
            return new oscLong(i);
        }
        public static implicit operator long(oscLong i)
        {
            return i.value;
        }
        public char GetTag() { return 'h'; }
        public OSCData getValue() { return (oscInt)value; }
        public override string ToString() { return value.ToString(); }
    }
    class oscFloat : OSCData
    {
        public float value;
        public oscFloat(float f){value = f;}
        public byte[] pack()
        {
            byte[] data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) data = Utilities.swapEndian(data);
            return data;
        }
        public static oscFloat unpack(byte[] bytes, ref int start)
        {
			byte[] data = new byte[4];
			for(int i = 0 ; i < 4 ; i++, start++) data[i] = bytes[start];
			if(BitConverter.IsLittleEndian) data = Utilities.swapEndian(data);
			return (oscFloat)BitConverter.ToSingle(data, 0);
        }
        public static implicit operator oscFloat(float i)
        {
            return new oscFloat(i);
        }
        public static implicit operator float(oscFloat i)
        {
            return i.value;
        }
        public char GetTag() { return 'f'; }
        public OSCData getValue() { return (oscFloat)value; }
        public override string ToString() { return value.ToString(); }
    }
    class oscDouble : OSCData
    {
        public double value;
        public oscDouble(double d) { value = d; }
        public byte[] pack()
        {
            byte[] data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) data = Utilities.swapEndian(data);
            return data;
        }
        public static oscDouble unpack(byte[] bytes, ref int start)
        {
			byte[] data = new byte[8];
			for(int i = 0 ; i < 8 ; i++, start++) data[i] = bytes[start];
			if(BitConverter.IsLittleEndian) data = Utilities.swapEndian(data);
			return (oscDouble)BitConverter.ToDouble(data, 0);
        }
        public static implicit operator oscDouble(double i)
        {
            return new oscDouble(i);
        }
        public static implicit operator double(oscDouble i)
        {
            return i.value;
        }
        public char GetTag() { return 'd'; }
        public OSCData getValue() { return (oscDouble)value; }
        public override string ToString() { return value.ToString(); }
    }
    class oscString : OSCData
    {
        public oscString(string s){value = s;}
        public string value;
        public byte[] pack()
        {
            return Encoding.UTF8.GetBytes(value);
        }
        public static oscString unpack(byte[] bytes, ref int start)
        {
			int count= 0;
			for(int index = start ; bytes[index] != 0 ; index++, count++) ;
			oscString s = Encoding.UTF8.GetString(bytes, start, count);
			start += count+1;
			start = (start + 3) / 4 * 4;
			return s;
        }
        public static implicit operator oscString(string s)
        {
            return new oscString(s);
        }
        public static implicit operator string(oscString s)
        {
            return s.value;
        }
        public char GetTag() { return 's'; }
        public OSCData getValue() { return (oscString)value; }
        public override string ToString() { return value.ToString(); }
    }

    class oscChar : OSCData
    {
        public oscChar(char c) { value = c; }
        public char value;
        public byte[] pack()
        {
            byte[] data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) data = Utilities.swapEndian(data);
            return data;
        }
        public static oscChar unpack(byte[] bytes, ref int start)
        {
            byte[] data = {bytes[start]};
            return (oscChar)BitConverter.ToChar(data, 0);
        }
        public static implicit operator oscChar(char s)
        {
            return new oscChar(s);
        }
        public static implicit operator char(oscChar s)
        {
            return s.value;
        }
        public char GetTag() { return 'c'; }
        public OSCData getValue() { return (oscChar)value; }
        public override string ToString() { return value.ToString(); }
    }

    class oscStream : OSCData
    {
        public Stream value;
        public oscStream(Stream s) { value = s; }
        public byte[] pack()
        {
            var mem = new MemoryStream();
            value.Seek(0, SeekOrigin.Begin);
            value.CopyTo(mem);

            byte[] valueData = mem.ToArray();

            var lData = new List<byte>();

            oscInt i = valueData.Length;

            var length = i.pack();

            lData.AddRange(length);
            lData.AddRange(valueData);

            return (byte[])lData.ToArray();
        }
        public static oscStream unpack(byte[] bytes, ref int start)
        {
            oscInt length = (oscInt)oscInt.unpack(bytes, ref start);

            byte[] buffer = new byte[length];
            Array.Copy(bytes, start, buffer, 0, length);
            
            start += length;
            start = (start + 3) / 4 * 4;
            return (oscStream)new MemoryStream(buffer);
        }
        public static implicit operator oscStream(Stream s)
        {
            return new oscStream(s);
        }
        public static implicit operator Stream(oscStream s)
        {
            return s.value;
        }
        public char GetTag() { return 'b'; }
        public OSCData getValue() { return (oscStream)value;}
        public override string ToString() { return value.ToString(); }
    }

    class oscDateTime : OSCData
    {
        public DateTime value;
        public oscDateTime(DateTime d) { value = d; }
        public byte[] pack()
        {
            var tag = new OscTimeTag();
            tag.Set(value);
            
            return tag.ToByteArray(); ;
        }
        public static oscDateTime unpack(byte[] bytes, ref int start)
        {
            byte[] data = new byte[8];
            for (int i = 0; i < 8; i++, start++) data[i] = bytes[start];
            var tag = new OscTimeTag(data);

            return (oscDateTime)tag.DateTime;
        }
        public char GetTag() { return 't'; }
        public static implicit operator oscDateTime(DateTime s)
        {
            return new oscDateTime(s);
        }
        public static implicit operator DateTime(oscDateTime s)
        {
            return s.value;
        }
        public OSCData getValue() { return (oscDateTime)value;}
        public override string ToString() { return value.ToString(); }
    }
 

}
