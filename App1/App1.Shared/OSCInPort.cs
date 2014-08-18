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

namespace gnow.util.osc
{
	/// <summary>
	/// Singleton wrapper for a udp port which received OSC packets
	/// </summary>
	public sealed class OSCInPort
	{
        private static readonly OSCInPort instance = new OSCInPort();

        private OSCInPort() { }

		/// <summary>
		/// Gets the instnace of the class
		/// </summary>
        public static OSCInPort Instance
        {
            get
            {
                return instance;
            }
        }

		/// <summary>
		/// Socket which receives OSC packets.
		/// </summary>
		private static DatagramSocket udpClient;

		/// <summary>
		/// The port to listen to for OSC packets
		/// </summary>
        public static string localPort;

		/// <summary>
		/// Connects to the local port and sets up receiving events.
		/// </summary>
		async public Task Connect()
		{
			if(udpClient != null) Close();
            if (localPort == null) { return; }
            udpClient = new DatagramSocket();
            udpClient.MessageReceived += udpClient_MessageReceived;

            try
            {
                await udpClient.BindServiceNameAsync(localPort);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return;
            }
		}

		/// <summary>
		/// Connects to the local port and sets up receiving events.
		/// </summary>
		/// <param name="port">The local port to connect to.</param>
		async public Task Connect(string port)
		{
			if(udpClient != null) Close();
			localPort = port;
            udpClient = new DatagramSocket();
            udpClient.MessageReceived += udpClient_MessageReceived;

            try
            {
                await udpClient.BindServiceNameAsync(localPort);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return;
            }
		}

		/// <summary>
		/// Event consumer for udp message received. Raises OSCPacketReceivedEvent.
		/// </summary>
        public void udpClient_MessageReceived(object sender, DatagramSocketMessageReceivedEventArgs e)
        {
            DataReader reader = e.GetDataReader();
            if (reader.UnconsumedBufferLength > 0)
            {
                byte[] bytes = new byte[reader.UnconsumedBufferLength];
                reader.ReadBytes(bytes);
                OSCPacketReceivedEventArgs args = new OSCPacketReceivedEventArgs();
                args.packet = OSCPacket.Unpack(bytes);
                OnOSCPacketReceivedEvent(args);
                Debug.WriteLine(args.packet.ToString());
            }
        }

        public delegate void OSCPacketReceivedEventHandler(object sender, OSCPacketReceivedEventArgs e);

        public event OSCPacketReceivedEventHandler OSCPacketReceivedEvent;

		/// <summary>
		/// Event raised when a packet is received
		/// </summary>
        private void OnOSCPacketReceivedEvent(OSCPacketReceivedEventArgs e)
        {
            OSCPacketReceivedEventHandler handler = OSCPacketReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        

		/// <summary>
		/// Propery closes the port and frees resources.
		/// </summary>
		public void Close()
		{
			udpClient.Dispose();
			udpClient = null;
		}


	}

	/// <summary>
	/// Arguments sent with <see cref="OSCPacketRecievedEventHandler"/>.
	/// </summary>
    public class OSCPacketReceivedEventArgs : EventArgs
    {
		/// <summary>
		/// Received packet
		/// </summary>
        public OSCPacket packet;
    }
}
