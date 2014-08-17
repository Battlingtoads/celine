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
using System.IO;
using System.Text;
namespace gnow.util.osc
{
	/// <summary>
	/// OSCMessage
	/// 
	/// Contains an address, a comma followed by one or more type identifiers. then the data itself follows in binary encoding.
	/// </summary>
	public class OSCMessage : OSCPacket
	{
//      These Attributes adhere to the OSC Specs 1.0
        protected const char INTEGER = 'i'; // int32 8byte
		protected const char FLOAT	  = 'f'; //float32 8byte
		protected const char LONG	  = 'h';  //int64 16byte
		protected const char DOUBLE  = 'd'; // float64 16byte
		protected const char STRING  = 's'; // padded by zeros
		protected const char SYMBOL  = 'S'; // same as STRING really
        protected const char BLOB	  = 'b'; // bytestream, starts with an int that tells the total length of th stream
        protected const char TIMETAG = 't'; // fixed point floating number with 32bytes (16bytes for totaldays after 1.1.1900 and 16bytes for fractionOfDay)
        protected const char CHAR	  = 'c'; // bit

        //protected const char TRUE	  = 'T';
        //protected const char FALSE = 'F';
        protected const char NIL = 'N';
        //protected const char INFINITUM = 'I';

        //protected const char ALL     = '*';

		public OSCMessage(string address)
		{
            this.typeTag = ",";
			this.Address = address;
		}
		public OSCMessage(string address, OSCData value)
		{
            this.typeTag = ",";
			this.Address = address;
			Append(value);
		}

		override protected void pack()
		{
			List<byte> data = new List<byte>();
            oscString oAddress = this.address;
            oscString oTypeTag = this.typeTag;

			addBytes(data, oAddress.pack());
			padNull(data);
			addBytes(data, oTypeTag.pack());
			padNull(data);

            foreach(OSCData item in values)
            {
                addBytes(data, item.pack());
                padNull(data);
            }
            this.binaryData = (byte[])data.ToArray();
        }

		public static OSCMessage Unpack(byte[] bytes, ref int start)
		{
			string address = oscString.unpack(bytes, ref start);
			//Console.WriteLine("address: " + address);
			OSCMessage msg = new OSCMessage(address);
            oscString oTags;
            oTags = oscString.unpack(bytes, ref start);
            char[] tags = ((string)oTags).ToCharArray();
			//Console.WriteLine("tags: " + new string(tags));
			foreach(char tag in tags)
			{
				//Console.WriteLine("tag: " + tag + " @ "+start);
				if(tag == ',') continue;
				else if(tag == INTEGER) msg.Append(oscInt.unpack(bytes, ref start));
				else if(tag == LONG) msg.Append(oscLong.unpack(bytes, ref start));
				else if(tag == DOUBLE) msg.Append(oscDouble.unpack(bytes, ref start));
				else if(tag == FLOAT) msg.Append(oscFloat.unpack(bytes, ref start));
                else if (tag == STRING || tag == SYMBOL) msg.Append(oscString.unpack(bytes, ref start));
                else if (tag == CHAR) msg.Append(oscChar.unpack(bytes, ref start));
                else if (tag == BLOB) msg.Append(oscStream.unpack(bytes, ref start));
                else if (tag == TIMETAG) msg.Append(oscDateTime.unpack(bytes, ref start));

                //TODO: Change unpacks here ^^^^ to use oscdata interface
		        
            }
            return msg;
        }

		override public void Append(OSCData value)
		{
            values.Add(value);
            AppendTag(value.GetTag());
            
		}

	    private void Fallback()
	    {
	        AppendTag(NIL);
//	        values.Add("undefined");
	    }

	    protected string typeTag;
		protected void AppendTag(char type)
		{
			typeTag += type;
		}

		override public bool IsBundle() { return false; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Address + " ");
            for(int i = 0; i < values.Count; i++)
                sb.Append(values[i].ToString() + " ");
            return sb.ToString();
        }
	}
}
