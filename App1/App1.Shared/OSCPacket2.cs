using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace gnow.util.osc
{
    abstract public class OSCPacket
    {
		public static readonly Encoding ASCIIEncoding8Bit;

        static OSCPacket()
        {
            ASCIIEncoding8Bit = Encoding.UTF8;
        }
        
		public OSCPacket()
		{
            this.values = new List<OSCData>();
		}

		protected static void addBytes(List<byte> data, byte[] bytes)
		{
			foreach(byte b in bytes)
			{
				data.Add(b);
			}
		}

		protected static void padNull(List<byte> data)
		{
			byte zero = 0;
			int pad = 4 - (data.Count % 4);
			for (int i = 0; i < pad; i++)
			{
				data.Add(zero);
			}
		}

		internal static byte[] swapEndian(byte[] data)
		{
			byte[] swapped = new byte[data.Length];
			for(int i = data.Length - 1, j = 0 ; i >= 0 ; i--, j++)
			{
				swapped[j] = data[i];
			}
			return swapped;
		}

		protected static byte[] pack(int value)
		{
			byte[] data = BitConverter.GetBytes(value);
			if(BitConverter.IsLittleEndian)	data = swapEndian(data);
			return data;
		}

		protected static byte[] pack(long value)
		{
			byte[] data = BitConverter.GetBytes(value);
			if(BitConverter.IsLittleEndian) data = swapEndian(data);
			return data;
		}

		protected static byte[] pack(float value)
		{
			byte[] data = BitConverter.GetBytes(value);
			if(BitConverter.IsLittleEndian) data = swapEndian(data);
			return data;
		}

		protected static byte[] pack(double value)
		{
			byte[] data = BitConverter.GetBytes(value);
			if(BitConverter.IsLittleEndian) data = swapEndian(data);
			return data;
		}

		protected static byte[] pack(string value)
		{
			return ASCIIEncoding8Bit.GetBytes(value);
		}


        protected static byte[] pack(char value)
        {
            byte[] data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) data = swapEndian(data);
            return data;
        }

        protected static byte[] pack(Stream value)
        {
            var mem = new MemoryStream();
            value.Seek(0, SeekOrigin.Begin);
            value.CopyTo(mem);

            byte[] valueData = mem.ToArray();

            var lData = new List<byte>();

            var length = pack(valueData.Length);

            lData.AddRange(length);
            lData.AddRange(valueData);

            return (byte[])lData.ToArray();
        }

        protected static byte[] pack(DateTime value)
        {
            var tag = new OscTimeTag();
            tag.Set(value);
            
            return tag.ToByteArray(); ;
        }
        protected static byte[] pack(object val)
        {
            return null;
        }


		abstract protected void pack();
		protected byte[] binaryData;
		public byte[] BinaryData
		{
			get
			{
				pack();
				return binaryData;
			}
		}

		protected static int unpackInt(byte[] bytes, ref int start)
		{
			byte[] data = new byte[4];
			for(int i = 0 ; i < 4 ; i++, start++) data[i] = bytes[start];
			if(BitConverter.IsLittleEndian) data = swapEndian(data);
			return BitConverter.ToInt32(data, 0);
		}

		protected static long unpackLong(byte[] bytes, ref int start)
		{
			byte[] data = new byte[8];
			for(int i = 0 ; i < 8 ; i++, start++) data[i] = bytes[start];
			if(BitConverter.IsLittleEndian) data = swapEndian(data);
			return BitConverter.ToInt64(data, 0);
		}

		protected static float unpackFloat(byte[] bytes, ref int start)
		{
			byte[] data = new byte[4];
			for(int i = 0 ; i < 4 ; i++, start++) data[i] = bytes[start];
			if(BitConverter.IsLittleEndian) data = swapEndian(data);
			return BitConverter.ToSingle(data, 0);
		}

		protected static double unpackDouble(byte[] bytes, ref int start)
		{
			byte[] data = new byte[8];
			for(int i = 0 ; i < 8 ; i++, start++) data[i] = bytes[start];
			if(BitConverter.IsLittleEndian) data = swapEndian(data);
			return BitConverter.ToDouble(data, 0);
		}

		protected static string unpackString(byte[] bytes, ref int start)
		{
			int count= 0;
			for(int index = start ; bytes[index] != 0 ; index++, count++) ;
			string s = ASCIIEncoding8Bit.GetString(bytes, start, count);
			start += count+1;
			start = (start + 3) / 4 * 4;
			return s;
		}

        protected static char unpackChar(byte[] bytes, ref int start)
        {
            byte[] data = {bytes[start]};
            return BitConverter.ToChar(data, 0);
        }

        protected static Stream unpackBlob(byte[] bytes, ref int start)
        {
            int length = unpackInt(bytes, ref start);

            byte[] buffer = new byte[length];
            Array.Copy(bytes, start, buffer, 0, length);
            
            start += length;
            start = (start + 3) / 4 * 4;
            return new MemoryStream(buffer);
        }

        protected static DateTime unpackTimeTag(byte[] bytes, ref int start)
        {
            byte[] data = new byte[8];
            for (int i = 0; i < 8; i++, start++) data[i] = bytes[start];
            var tag = new OscTimeTag(data);

            return tag.DateTime;
        }

		public static OSCPacket Unpack(byte[] bytes)
		{
			int start = 0;
			return Unpack(bytes, ref start, bytes.Length);
		}

		public static OSCPacket Unpack(byte[] bytes, ref int start, int end)
		{
		//	if(bytes[start] == '#') return OSCBundle.Unpack(bytes, ref start, end);
			//else 
            return OSCMessage.Unpack(bytes, ref start);
		}


		protected string address;
		public string Address
		{
			get { return address; }
			set 
			{
				// TODO: validate
				address = value;
			}
		}

		protected List<OSCData> values;
		public List<OSCData> Values
		{
            get { return (List<OSCData>)values; }
		}
		abstract public void Append(OSCData value);

		abstract public bool IsBundle();
    }
}
