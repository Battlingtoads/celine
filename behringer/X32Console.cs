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
    public sealed  class X32Console
    {
        private static readonly X32Console instance = new X32Console();

        public static X32Console Instance
        {
            get
            {
                return instance;
            }
        }

        public List<X32Channel> Channels;
        public List<X32AuxIn> AuxInputs;
        public List<X32FXReturn> FXReturns;
        public List<X32MixBus> MixBusses;
        public List<X32Matrix> Matrices;
        public X32Main StereoMain;
        public X32Main MonoMain;

        private X32Console()
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

            //the first substring will be a null character because the format of the 'subaddress
            //string is '/<parameter>/....'
            switch (subs[1])
            {
                case "config":
                case "mix":
                    Channels[e.channel - 1].SetValuesFromOSC(subs, e.value);
                    break;
                case "delay":
                    break;
                case "preamp":
                    Channels[e.channel - 1].m_PreAmp.SetValuesFromOSC(subs, e.value);
                    break;
                case "gate":
                    Channels[e.channel - 1].m_Gate.SetValuesFromOSC(subs, e.value);
                    break;
                case "dyn":
                    Channels[e.channel - 1].m_Dynamic.SetValuesFromOSC(subs, e.value);
                    break;
                case "eq":
                    Channels[e.channel - 1].m_Eq.SetValuesFromOSC(subs, e.value);
                    break;
                case "grp":
                    break;
            }
        }

        private void Dispatch_AuxinReceivedEvent(object sender, AuxinReceivedEventArgs e)
        {
            //TODO Dispatch channel
            string[] subs = e.subAddress.Split('/');
            switch (subs[1])
            {
                case "config":
                case "mix":
                    AuxInputs[e.channel - 1].SetValuesFromOSC(subs, e.value);
                    break;
                case "preamp":
                    AuxInputs[e.channel - 1].m_PreAmp.SetValuesFromOSC(subs, e.value);
                    break;
                case "eq":
                    AuxInputs[e.channel - 1].m_Eq.SetValuesFromOSC(subs, e.value);
                    break;
                case "grp":
                    break;
            }
        }

        private void Dispatch_FXReturnReceivedEvent(object sender, FXReturnReceivedEventArgs e)
        {
            //TODO Dispatch channel
            Debug.WriteLine("received FXReturn...");
            string[] subs = e.subAddress.Split('/');
            switch (subs[1])
            {
                case "config":
                case "mix":
                    FXReturns[e.channel - 1].SetValuesFromOSC(subs, e.value);
                    break;
                case "grp":
                    break;
            }
        }

        private void Dispatch_BusReceivedEvent(object sender, BusReceivedEventArgs e)
        {
            //TODO Dispatch channel
            Debug.WriteLine("received bus...");
            string[] subs = e.subAddress.Split('/');
            switch (subs[1])
            {
                case "config":
                case "mix":
                    MixBusses[e.bus - 1].SetValuesFromOSC(subs, e.value);
                    break;
                case "dyn":
                    MixBusses[e.bus - 1].m_Dynamic.SetValuesFromOSC(subs, e.value);
                    break;
                case "insert":
                    break;
                case "eq":
                    MixBusses[e.bus - 1].m_Eq.SetValuesFromOSC(subs, e.value);
                    break;
                case "grp":
                    break;
            }
        }

        private void Dispatch_MatrixReceivedEvent(object sender, MatrixReceivedEventArgs e)
        {
            //TODO Dispatch channel
            Debug.WriteLine("received matrix...");
            string[] subs = e.subAddress.Split('/');
            switch (subs[1])
            {
                case "config":
                case "mix":
                    Matrices[e.matrix - 1].SetValuesFromOSC(subs, e.value);
                    break;
                case "dyn":
                    Matrices[e.matrix - 1].m_Dynamic.SetValuesFromOSC(subs, e.value);
                    break;
                case "insert":
                    break;
                case "eq":
                    Matrices[e.matrix - 1].m_Eq.SetValuesFromOSC(subs, e.value);
                    break;
                case "grp":
                    break;
            }
        }
        
        private void Dispatch_MainReceivedEvent(object sender, MainReceivedEventArgs e)
        {
            //TODO Dispatch channel
            Debug.WriteLine("received main...");
            string[] subs = e.subAddress.Split('/');
            switch (subs[1])
            {
                case "config":
                case "mix":
                    StereoMain.SetValuesFromOSC(subs, e.value);
                    break;
                case "dyn":
                    StereoMain.m_Dynamic.SetValuesFromOSC(subs, e.value);
                    break;
                case "insert":
                    break;
                case "eq":
                    StereoMain.m_Eq.SetValuesFromOSC(subs, e.value);
                    break;
                case "grp":
                    break;
            }
        }
    }
}
