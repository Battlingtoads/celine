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
using gnow.util.behringer;
using gnow.util.behringer.events;

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

        public void RaiseEventFake(OSCPacket packet)
        {
            OSCPacketReceivedEventArgs args = new OSCPacketReceivedEventArgs();
            args.packet = packet;
            OnOSCPacketReceivedEvent(args);
        }
        

		/// <summary>
		/// Propery closes the port and frees resources.
		/// </summary>
		public void Close()
		{
			udpClient.Dispose();
			udpClient = null;
		}
        public void receivedOscMessage(Object sender, OSCPacketReceivedEventArgs e)
        {
            if (e.packet.Address.Substring(0, 7) == "/meters")
            {
                MetersReceivedEventArgs args = new MetersReceivedEventArgs();
                switch (e.packet.Address)
                {
                    case "/meters/0":
                        args.type = Constants.METER_TYPE.HOME;
                        break;
                    case "/meters/1":
                        args.type = Constants.METER_TYPE.CHANNEL_PAGE;
                        break;
                    case "/meters/2":
                        args.type = Constants.METER_TYPE.MIX_BUS;
                        break;
                    case "/meters/3":
                        args.type = Constants.METER_TYPE.AUX_FX;
                        break;
                    case "/meters/4":
                        args.type = Constants.METER_TYPE.IN_OUT;
                        break;
                    case "/meters/5":
                        args.type = Constants.METER_TYPE.SURFACE;
                        break;
                    case "/meters/6":
                        args.type = Constants.METER_TYPE.CHANNEL_DETAIL;
                        break;
                    case "/meters/7":
                        args.type = Constants.METER_TYPE.BUS_SEND;
                        break;
                    case "/meters/8":
                        args.type = Constants.METER_TYPE.MATRIX_SEND;
                        break;
                    case "/meters/9":
                        args.type = Constants.METER_TYPE.EFFECTS;
                        break;
                        
                    default:
                        break;
                }
                oscStream str = (oscStream)e.packet.Values[0].getValue();
                str.value.Read(args.data, 0, (int)str.value.Length);
                OnMetersReceivedEvent(args);

            }
            else if(e.packet.Address.Substring(0, 7) == "/config")
            {
                ConfigReceivedEventArgs args = new ConfigReceivedEventArgs();
                args.subAddress = e.packet.Address.Substring(7);
                args.value = e.packet.Values[0].getValue();
                OnConfigReceivedEvent(args);
            }
            else if(e.packet.Address.Substring(0,3) == "/ch")
            {
                ChannelReceivedEventArgs args = new ChannelReceivedEventArgs();
                string tempChannel = e.packet.Address.Substring(4, 2);
                args.channel = Convert.ToInt32(tempChannel);
                args.value = e.packet.Values[0].getValue();
                args.subAddress = e.packet.Address.Substring(6);
                OnChannelReceivedEvent(args);
            }
            else if(e.packet.Address.Substring(0,6) == "/auxin")
            {
                AuxinReceivedEventArgs args = new AuxinReceivedEventArgs();
                string tempChannel = e.packet.Address.Substring(7, 2);
                args.channel = Convert.ToInt32(tempChannel);
                args.value = e.packet.Values[0].getValue();
                args.subAddress = e.packet.Address.Substring(9);
                OnAuxinReceivedEvent(args);
            }
            else if(e.packet.Address.Substring(0,6) == "/fxrtn")
            {
                FXReturnReceivedEventArgs args = new FXReturnReceivedEventArgs();
                string tempChannel = e.packet.Address.Substring(7, 2);
                args.channel = Convert.ToInt32(tempChannel);
                args.value = e.packet.Values[0].getValue();
                args.subAddress = e.packet.Address.Substring(9);
                OnFXReturnReceivedEvent(args);
            }
            else if(e.packet.Address.Substring(0,4) == "/bus")
            {
                BusReceivedEventArgs args = new BusReceivedEventArgs();
                string tempBus = e.packet.Address.Substring(5, 2);
                args.bus = Convert.ToInt32(tempBus);
                args.value = e.packet.Values[0].getValue();
                args.subAddress = e.packet.Address.Substring(7);
                OnBusReceivedEvent(args);
            }
            else if(e.packet.Address.Substring(0,4) == "/mtx")
            {
                MatrixReceivedEventArgs args = new MatrixReceivedEventArgs();
                string tempMatrix = e.packet.Address.Substring(5, 2);
                args.matrix = Convert.ToInt32(tempMatrix);
                args.value = e.packet.Values[0].getValue();
                args.subAddress = e.packet.Address.Substring(7);
                OnMatrixReceivedEvent(args);
            }
            else if(e.packet.Address.Substring(0,4) == "/main/st")
            {
                MainReceivedEventArgs args = new MainReceivedEventArgs();
                args.value = e.packet.Values[0].getValue();
                args.subAddress = e.packet.Address.Substring(8);
                OnMainReceivedEvent(args);
            }
            else if(e.packet.Address.Substring(0,4) == "/mono/st")
            {
                MonoReceivedEventArgs args = new MonoReceivedEventArgs();
                args.value = e.packet.Values[0].getValue();
                args.subAddress = e.packet.Address.Substring(8);
                OnMonoReceivedEvent(args);
            }
            else if(e.packet.Address.Substring(0,4) == "/dca")
            {
                DCAReceivedEventArgs args = new DCAReceivedEventArgs();
                string tempDCA = e.packet.Address.Substring(5, 1);
                args.dca = Convert.ToInt32(tempDCA);
                args.value = e.packet.Values[0].getValue();
                args.subAddress = e.packet.Address.Substring(6);
                OnDCAReceivedEvent(args);
            }
            else if(e.packet.Address.Substring(0,3) == "/fx")
            {
                EffectReceivedEventArgs args = new EffectReceivedEventArgs();
                string tempEffect = e.packet.Address.Substring(4, 1);
                args.effect = Convert.ToInt32(tempEffect);
                args.value = e.packet.Values[0].getValue();
                args.subAddress = e.packet.Address.Substring(5);
                OnEffectReceivedEvent(args);
            }
            else if(e.packet.Address.Substring(0,8) == "/outputs")
            {
                if(e.packet.Address.Substring(8,5) == "/main")
                {
                    MainOutReceivedEventArgs args = new MainOutReceivedEventArgs();
                    string tempMainOut = e.packet.Address.Substring(13, 2);
                    args.mainout = Convert.ToInt32(tempMainOut);
                    args.value = e.packet.Values[0].getValue();
                    args.subAddress = e.packet.Address.Substring(15);
                    OnMainOutReceivedEvent(args);
                }
                else if(e.packet.Address.Substring(8,4) == "/aux")
                {
                    AuxOutReceivedEventArgs args = new AuxOutReceivedEventArgs();
                    string tempAuxOut = e.packet.Address.Substring(12, 2);
                    args.auxout= Convert.ToInt32(tempAuxOut);
                    args.value = e.packet.Values[0].getValue();
                    args.subAddress = e.packet.Address.Substring(14);
                    OnAuxOutReceivedEvent(args);
                }
                else if(e.packet.Address.Substring(8,4) == "/p16")
                {
                    P16OutReceivedEventArgs args = new P16OutReceivedEventArgs();
                    string tempP16Out = e.packet.Address.Substring(12, 2);
                    args.p16out= Convert.ToInt32(tempP16Out);
                    args.value = e.packet.Values[0].getValue();
                    args.subAddress = e.packet.Address.Substring(14);
                    OnP16OutReceivedEvent(args);
                }
                else if(e.packet.Address.Substring(8,4) == "/aes")
                {
                    AESOutReceivedEventArgs args = new AESOutReceivedEventArgs();
                    string tempAESOut = e.packet.Address.Substring(12, 2);
                    args.aesout = Convert.ToInt32(tempAESOut);
                    args.value = e.packet.Values[0].getValue();
                    args.subAddress = e.packet.Address.Substring(14);
                    OnAESOutReceivedEvent(args);
                }
                else if(e.packet.Address.Substring(8,4) == "/rec")
                {
                    RecordOutReceivedEventArgs args = new RecordOutReceivedEventArgs();
                    string tempRecordOut = e.packet.Address.Substring(12, 2);
                    args.recordout = Convert.ToInt32(tempRecordOut);
                    args.value = e.packet.Values[0].getValue();
                    args.subAddress = e.packet.Address.Substring(14);
                    OnRecordOutReceivedEvent(args);
                }
            }
            else if(e.packet.Address.Substring(0, 8) == "/headamp")
            {
                    HeadampReceivedEventArgs args = new HeadampReceivedEventArgs();
                    string tempHeadamp = e.packet.Address.Substring(9, 3);
                    args.headamp = Convert.ToInt32(tempHeadamp);
                    args.value = e.packet.Values[0].getValue();
                    args.subAddress = e.packet.Address.Substring(12);
                    OnHeadampReceivedEvent(args);
            }


                
        }

        public event MetersReceivedEventHandler  MetersReceivedEvent;

        public virtual void OnMetersReceivedEvent(MetersReceivedEventArgs e)
        {
            MetersReceivedEventHandler handler = MetersReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event ConfigReceivedEventHandler ConfigReceivedEvent;

        public virtual void OnConfigReceivedEvent(ConfigReceivedEventArgs e)
        {
            ConfigReceivedEventHandler handler = ConfigReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event ChannelReceivedEventHandler ChannelReceivedEvent;
        
        public virtual void OnChannelReceivedEvent(ChannelReceivedEventArgs e)
        {
            ChannelReceivedEventHandler handler = ChannelReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event AuxinReceivedEventHandler AuxinReceivedEvent;
        
        public virtual void OnAuxinReceivedEvent(AuxinReceivedEventArgs e)
        {
            AuxinReceivedEventHandler handler = AuxinReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }
        public event FXReturnReceivedEventHandler FXReturnReceivedEvent;
        
        public virtual void OnFXReturnReceivedEvent(FXReturnReceivedEventArgs e)
        {
            FXReturnReceivedEventHandler handler = FXReturnReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event BusReceivedEventHandler BusReceivedEvent;
        
        public virtual void OnBusReceivedEvent(BusReceivedEventArgs e)
        {
            BusReceivedEventHandler handler = BusReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event MatrixReceivedEventHandler MatrixReceivedEvent;
        
        public virtual void OnMatrixReceivedEvent(MatrixReceivedEventArgs e)
        {
            MatrixReceivedEventHandler handler = MatrixReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event MainReceivedEventHandler MainReceivedEvent;
        
        public virtual void OnMainReceivedEvent(MainReceivedEventArgs e)
        {
            MainReceivedEventHandler handler = MainReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event MonoReceivedEventHandler MonoReceivedEvent;
        
        public virtual void OnMonoReceivedEvent(MonoReceivedEventArgs e)
        {
            MonoReceivedEventHandler handler = MonoReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event DCAReceivedEventHandler DCAReceivedEvent;
        
        public virtual void OnDCAReceivedEvent(DCAReceivedEventArgs e)
        {
            DCAReceivedEventHandler handler = DCAReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event EffectReceivedEventHandler EffectReceivedEvent;
        
        public virtual void OnEffectReceivedEvent(EffectReceivedEventArgs e)
        {
            EffectReceivedEventHandler handler = EffectReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event MainOutReceivedEventHandler MainOutReceivedEvent;
        
        public virtual void OnMainOutReceivedEvent(MainOutReceivedEventArgs e)
        {
            MainOutReceivedEventHandler handler = MainOutReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event AuxOutReceivedEventHandler AuxOutReceivedEvent;
        
        public virtual void OnAuxOutReceivedEvent(AuxOutReceivedEventArgs e)
        {
            AuxOutReceivedEventHandler handler = AuxOutReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event P16OutReceivedEventHandler P16OutReceivedEvent;
        
        public virtual void OnP16OutReceivedEvent(P16OutReceivedEventArgs e)
        {
            P16OutReceivedEventHandler handler = P16OutReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event AESOutReceivedEventHandler AESOutReceivedEvent;
        
        public virtual void OnAESOutReceivedEvent(AESOutReceivedEventArgs e)
        {
            AESOutReceivedEventHandler handler = AESOutReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event RecordOutReceivedEventHandler RecordOutReceivedEvent;
        
        public virtual void OnRecordOutReceivedEvent(RecordOutReceivedEventArgs e)
        {
            RecordOutReceivedEventHandler handler = RecordOutReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event HeadampReceivedEventHandler HeadampReceivedEvent;
        
        public virtual void OnHeadampReceivedEvent(HeadampReceivedEventArgs e)
        {
            HeadampReceivedEventHandler handler = HeadampReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
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

