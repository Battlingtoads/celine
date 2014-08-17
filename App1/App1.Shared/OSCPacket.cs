using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace gnow.util.osc
{
    abstract public class OSCPacket : OSCData
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
