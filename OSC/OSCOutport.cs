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
// * Redistributions of source code must retain the above copyright notice, list of conditions and the following disclaimer.
// * Redistributions in binary form must reproduce the above copyright notice, list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
// * Neither the name of "luvtechno.net" nor the names of its contributors may be used to endorse or promote products derived from software without specific prior written permission.
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
using System.Net;
using System.Diagnostics;
using Windows.Networking.Sockets;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Storage.Streams;
using Windows.Foundation;
using Windows.System.Threading;
using System.Threading;

namespace gnow.util.osc
{
	/// <summary>
	/// Singleton wrapper for udp port which sends OSC packets.
	/// </summary>
	public sealed class OSCOutPort
	{
        private static readonly OSCOutPort instance = new OSCOutPort();

        private OSCOutPort() { }

		/// <summary>
		/// Gets the instance of the singleton OSCOutport
		/// </summary>
        public static OSCOutPort Instance
        {
            get
            {
                return instance;
            }
        }

		/// <summary>
		/// Socket where messages are sent from.
		/// </summary>
		private DatagramSocket udpServer;

        private DataWriter oscWriter;

		/// <summary>
		/// Port to send to.
		/// </summary>
        public static string remotePort;

		/// <summary>
		/// IP address to send to.
		/// </summary>
        public static HostName remoteHost;

		/// <summary>
		/// Initializes the socket to the <see cref="remoteHost"/> and <see cref="remotePort"/>.
		/// </summary>
		async public Task Connect()
		{
			if(udpServer != null) Close();
            udpServer = new DatagramSocket();
            udpServer.MessageReceived += udpServer_MessageReceived;
            try
            {
                await udpServer.ConnectAsync(remoteHost, remotePort);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return;
            }
            oscWriter = new DataWriter(udpServer.OutputStream);
		}

        static void udpServer_MessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            //we whouldn't get any messages here
        }

		/// <summary>
		/// Closes the socket and frees resources.
		/// </summary>
		public static void Close()
		{
			Instance.udpServer.Dispose();
			Instance.udpServer = null;
		}

		/// <summary>
		/// Writes a packet to the socket.
		/// </summary>
		/// <param name="packet">The packet to be sent</param>
		public async Task SendAsync(OSCPacket packet)
		{
			byte[] data = packet.BinaryData;

			try 
			{

                for (int i = 0; i < 1; i++)
                {
                    oscWriter.WriteBytes(data);
                    await oscWriter.StoreAsync();
                    Debug.WriteLine(packet.ToString() + DateTime.UtcNow.ToString() + DateTime.UtcNow.Millisecond.ToString());
                }
			}
			catch (Exception e)
			{
			    Debug.WriteLine(e.Message);
				Debug.WriteLine(e.StackTrace);
			}

		}

	}
}
