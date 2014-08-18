using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gnow.util.behringer.events;
using System.Diagnostics;
using gnow.util.osc;

namespace gnow.util.behringer
{
    public class X32Console
    {
        public List<X32Channel> Channels;
        public List<X32AuxIn> AuxInputs;
        public List<X32FXReturn> FXReturns;
        public List<X32MixBus> MixBusses;
        public List<X32Matrix> Matrices;
        public X32Main StereoMain;
        public X32Main MonoMain;

        public X32Console()
        {
            Channels = new List<X32Channel>(32);
            for(int i = 0; i < 32; i++)
            {
                Channels.Add(new X32Channel());
            }
            MixBusses = new List<X32MixBus>(16);
            for(int i = 0; i < 16; i++)
            {
                MixBusses.Add(new X32MixBus());
            }
            AuxInputs = new List<X32AuxIn>(8);
            FXReturns = new List<X32FXReturn>(8);
            for(int i = 0; i < 8; i++)
            {
                AuxInputs.Add(new X32AuxIn());
                FXReturns.Add(new X32FXReturn());
            }
            Matrices = new List<X32Matrix>(6);
            for(int i = 0; i < 6; i++)
            {
                Matrices.Add(new X32Matrix());
            }
            StereoMain = new X32Main();
            MonoMain = new X32Main();


            //Setup Event listeners
            X32MessageDispatcher.Instance.ChannelReceivedEvent += Dispatch_ChannelReceivedEvent;
            X32MessageDispatcher.Instance.AuxinReceivedEvent += Dispatch_AuxinReceivedEvent;
            X32MessageDispatcher.Instance.FXReturnReceivedEvent += Dispatch_FXReturnReceivedEvent;
            X32MessageDispatcher.Instance.BusReceivedEvent += Dispatch_BusReceivedEvent;
            X32MessageDispatcher.Instance.MatrixReceivedEvent += Dispatch_MatrixReceivedEvent;
            X32MessageDispatcher.Instance.MainReceivedEvent += Dispatch_MainReceivedEvent;
        }

        private void Dispatch_ChannelReceivedEvent(object sender, ChannelReceivedEventArgs e)
        {
            string[] subs = e.subAddress.Split('/');
            switch (subs[1])
            {
                case "config":
                    break;
                case "delay":
                    break;
                case "preamp":
                    break;
                case "gate":
                    break;
                case "dyn":
                    break;
                case "eq":
                    break;
                case "mix":
                    switch (subs[2])
                    {
                        case "on":
                            Channels[e.channel-1].Mute = (Constants.ON_OFF)(int)(oscInt)e.value;
                            break;
                        case "fader":
                            try
                            {
                                Channels[e.channel-1].Level.RawLevel = (float)(oscFloat)e.value;
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine(err.Message);
                            }
                            break;
                        default:
                            break;
                    }

                    break;
                case "grp":
                    break;
            }
        }

        private void Dispatch_AuxinReceivedEvent(object sender, AuxinReceivedEventArgs e)
        {
            //TODO Dispatch channel
            Debug.WriteLine("received auxin...");
        }

        private void Dispatch_FXReturnReceivedEvent(object sender, FXReturnReceivedEventArgs e)
        {
            //TODO Dispatch channel
            Debug.WriteLine("received FXReturn...");
        }

        private void Dispatch_BusReceivedEvent(object sender, BusReceivedEventArgs e)
        {
            //TODO Dispatch channel
            Debug.WriteLine("received bus...");
        }

        private void Dispatch_MatrixReceivedEvent(object sender, MatrixReceivedEventArgs e)
        {
            //TODO Dispatch channel
            Debug.WriteLine("received matrix...");
        }
        
        private void Dispatch_MainReceivedEvent(object sender, MainReceivedEventArgs e)
        {
            //TODO Dispatch channel
            Debug.WriteLine("received main...");
        }
    }
}
