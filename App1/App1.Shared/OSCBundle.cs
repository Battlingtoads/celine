#region licence/info
// OSC.NET - Open Sound Control for .NET
// http://luvtechno.net/
//
// Copyright (c) 2006, Yoshinori Kawasaki 
// All rights reserved.
//
// Changes and improvements:
// Copyright (c) 2005-2008 Martin Kaltenbrunner <mkalten@iua.upf.edu>
// As included with    
// http://reactivision.sourceforge.net/
//
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
// * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
// * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
// * Neither the name of "luvtechno.net" nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS 
// OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY 
// AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL 
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, 
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY 
// WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion licence/info
	
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// VVVV OSC Utilities 
/// </summary>
namespace gnow.util.osc 
{
	/// <summary>
	/// OSCBundle
	/// </summary>
	public class OSCBundle : OSCPacket
	{
        new private List<OSCPacket> values;
        new public List<OSCPacket> Values
        {
            get { return (List<OSCPacket>)values; }

        }

		protected const string BUNDLE = "#bundle";
		private DateTime timestamp = new DateTime();

        public OSCBundle(DateTime ts)
		{
            this.values = new List<OSCPacket>();
			this.address = BUNDLE;
			this.timestamp = ts;
		}

		public OSCBundle(long ts)
		{
            this.values = new List<OSCPacket>();
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            timestamp = start.AddMilliseconds(ts).ToLocalTime();		    
		}


		public OSCBundle()
		{
            this.values = new List<OSCPacket>();
			this.address = BUNDLE;
			this.timestamp = DateTime.Now;
		}

		override protected void pack()
		{
			List<byte> data = new List<byte>();
            oscString oAddress = this.address;
            oscDateTime oTimestamp = this.timestamp;

            addBytes(data, oAddress.pack());
            padNull(data);
			addBytes(data, oTimestamp.pack());  // fixed point, 8 bytes
            padNull(data);
			
			foreach(OSCPacket oscPacket in this.Values)
			{
				if (oscPacket != null)
				{
					byte[] bs = oscPacket.BinaryData;
                    oscInt len = bs.Length;
					addBytes(data, len.pack());
					addBytes(data, bs);
				}
				else 
				{
					// TODO
				}
			}
			
			this.binaryData = (byte[])data.ToArray();
		}

		public static new OSCBundle Unpack(byte[] bytes, ref int start, int end)
		{

			string address = oscString.unpack(bytes, ref start);
			//Console.WriteLine("bundle: " + address);
			if(!address.Equals(BUNDLE)) return null; // TODO

			DateTime timestamp = oscDateTime.unpack(bytes, ref start);
            OSCBundle bundle = new OSCBundle(timestamp);
			
			while(start < end)
			{
				int length = oscInt.unpack(bytes, ref start);
				int sub_end = start + length;
				bundle.Append(OSCPacket.Unpack(bytes, ref start, sub_end));
			}

			return bundle;
		}

		public DateTime getTimeStamp() {
			return timestamp;
		}

		override public void Append(OSCData value)
		{
			if( value is OSCPacket) 
			{
				values.Add((OSCPacket)value);
			}
			else 
			{
				// TODO: exception
			}
		}

		override public bool IsBundle() { return true; }
	}
}

