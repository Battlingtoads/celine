using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using gnow.util.behringer.events;
using gnow.util.osc;


namespace gnow.util.behringer
{
    public sealed class X32MessageDispatcher
    {
        private static readonly X32MessageDispatcher instance = new X32MessageDispatcher();

        private X32MessageDispatcher() 
        {
        }

        public static X32MessageDispatcher Instance
        {
            get
            {
                return instance;
            }
        }
        
        public void Initialize()
        {
            OSCInPort.Instance.OSCPacketReceivedEvent += receivedOscMessage;
        }

        public void receivedOscMessage(Object sender, OSCPacketReceivedEventArgs e)
        {
            if(!goodPacket(e.packet))
            {
                return;
            }
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
                args.data = new byte[str.value.Length];
                try
                {
                    int i = 0;
                    str.value.Position = 0;
                    while (str.value.Position < str.value.Length)
                    {
                        args.data[i] = (byte)str.value.ReadByte();
                        i++;
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                } OnMetersReceivedEvent(args);

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

        private bool goodPacket(OSCPacket oSCPacket)
        {
            //TODO: check incoming packet for proper osc data
            return true;
        }

        public event MetersReceivedEventHandler  MetersReceivedEvent;

        private void OnMetersReceivedEvent(MetersReceivedEventArgs e)
        {
            MetersReceivedEventHandler handler = MetersReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event ConfigReceivedEventHandler ConfigReceivedEvent;

        private void OnConfigReceivedEvent(ConfigReceivedEventArgs e)
        {
            ConfigReceivedEventHandler handler = ConfigReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event ChannelReceivedEventHandler ChannelReceivedEvent;
        
        private void OnChannelReceivedEvent(ChannelReceivedEventArgs e)
        {
            ChannelReceivedEventHandler handler = ChannelReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event AuxinReceivedEventHandler AuxinReceivedEvent;
        
        private void OnAuxinReceivedEvent(AuxinReceivedEventArgs e)
        {
            AuxinReceivedEventHandler handler = AuxinReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }
        public event FXReturnReceivedEventHandler FXReturnReceivedEvent;
        
        private void OnFXReturnReceivedEvent(FXReturnReceivedEventArgs e)
        {
            FXReturnReceivedEventHandler handler = FXReturnReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event BusReceivedEventHandler BusReceivedEvent;
        
        private void OnBusReceivedEvent(BusReceivedEventArgs e)
        {
            BusReceivedEventHandler handler = BusReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event MatrixReceivedEventHandler MatrixReceivedEvent;
        
        private void OnMatrixReceivedEvent(MatrixReceivedEventArgs e)
        {
            MatrixReceivedEventHandler handler = MatrixReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event MainReceivedEventHandler MainReceivedEvent;
        
        private void OnMainReceivedEvent(MainReceivedEventArgs e)
        {
            MainReceivedEventHandler handler = MainReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event MonoReceivedEventHandler MonoReceivedEvent;
        
        private void OnMonoReceivedEvent(MonoReceivedEventArgs e)
        {
            MonoReceivedEventHandler handler = MonoReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event DCAReceivedEventHandler DCAReceivedEvent;
        
        private void OnDCAReceivedEvent(DCAReceivedEventArgs e)
        {
            DCAReceivedEventHandler handler = DCAReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event EffectReceivedEventHandler EffectReceivedEvent;
        
        private void OnEffectReceivedEvent(EffectReceivedEventArgs e)
        {
            EffectReceivedEventHandler handler = EffectReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event MainOutReceivedEventHandler MainOutReceivedEvent;
        
        private void OnMainOutReceivedEvent(MainOutReceivedEventArgs e)
        {
            MainOutReceivedEventHandler handler = MainOutReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event AuxOutReceivedEventHandler AuxOutReceivedEvent;
        
        private void OnAuxOutReceivedEvent(AuxOutReceivedEventArgs e)
        {
            AuxOutReceivedEventHandler handler = AuxOutReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event P16OutReceivedEventHandler P16OutReceivedEvent;
        
        private void OnP16OutReceivedEvent(P16OutReceivedEventArgs e)
        {
            P16OutReceivedEventHandler handler = P16OutReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event AESOutReceivedEventHandler AESOutReceivedEvent;
        
        private void OnAESOutReceivedEvent(AESOutReceivedEventArgs e)
        {
            AESOutReceivedEventHandler handler = AESOutReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event RecordOutReceivedEventHandler RecordOutReceivedEvent;
        
        private void OnRecordOutReceivedEvent(RecordOutReceivedEventArgs e)
        {
            RecordOutReceivedEventHandler handler = RecordOutReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public event HeadampReceivedEventHandler HeadampReceivedEvent;
        
        private void OnHeadampReceivedEvent(HeadampReceivedEventArgs e)
        {
            HeadampReceivedEventHandler handler = HeadampReceivedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }
    }
}
