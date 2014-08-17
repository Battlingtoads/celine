using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace gnow.util.osc
{
	/// <summary>
	/// Base class for OSC packets.
	/// </summary>
    abstract public class OSCPacket : OSCData
    {
		public static readonly Encoding ASCIIEncoding8Bit;

        static OSCPacket()
        {
            ASCIIEncoding8Bit = Encoding.UTF8;
        }
        
		/// <summary>
		/// Creates a new OSC packet.
		/// </summary>
		public OSCPacket()
		{
            this.values = new List<OSCData>();
		}

		/// <summary>
		/// Adds an array of bytes to a list of bytes.
		/// </summary>
		/// <param name="data">List to add bytes to</param>
		/// <param name="bytes">Array of bytes to add</param>
		protected static void addBytes(List<byte> data, byte[] bytes)
		{
			foreach(byte b in bytes)
			{
				data.Add(b);
			}
		}

		/// <summary>
		///	Pads correct amount of null bytes for proper osc framing.
		///	</summary>
		///	<param name="data">List of bytes to pad</param>
		protected static void padNull(List<byte> data)
		{
			byte zero = 0;
			int pad = 4 - (data.Count % 4);
			for (int i = 0; i < pad; i++)
			{
				data.Add(zero);
			}
		}

		/// <summary>
		/// Changes Endianness of an object.
		/// </summary>
		/// <param name="data">Data to be swapped</param>
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

		/// <summary>
		/// Array of bytes which uses proper osc framing and standards.
		/// </summary>
		public byte[] BinaryData
		{
			get
			{
				pack();
				return binaryData;
			}
		}

		/// <summary>
		///	Converts a raw OSC packet byte array to useable objects.
		///	</summary>
		///	<param name="bytes">Byte array to be unpacked</param>
		public static OSCPacket Unpack(byte[] bytes)
		{
			int start = 0;
			return Unpack(bytes, ref start, bytes.Length);
		}

		/// <summary>
		///	Converts a raw OSC packet byte array to useable objects.
		///	</summary>
		///	<param name="bytes">Byte array to be unpacked</param>
		///	<param name="start">Index of the start of the packet</param>
		///	<param name="end">Index of the end of the packet</param>
		public static OSCPacket Unpack(byte[] bytes, ref int start, int end)
		{
		//	if(bytes[start] == '#') return OSCBundle.Unpack(bytes, ref start, end);
			//else 
            return OSCMessage.Unpack(bytes, ref start);
		}


		protected string address;

		/// <summary>
		/// Address to associate with the packet.
		/// </summary>
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

		/// <summary>
		/// List of objects to include in the message.<\summary>
		public List<OSCData> Values
		{
            get { return (List<OSCData>)values; }
		}
		abstract public void Append(OSCData value);

		abstract public bool IsBundle();
    }
}
