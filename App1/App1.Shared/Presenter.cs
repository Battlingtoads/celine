using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using gnow.util.behringer;

namespace gnow.util
{
    public sealed class Presenter
    {
        private static readonly Presenter instance = new Presenter();

        private Presenter() { 
            X32MessageDispatcher.Instance.ChannelReceivedEvent += Dispatch_ChannelReceivedEvent;
            X32MessageDispatcher.Instance.AuxinReceivedEvent += Dispatch_AuxinReceivedEvent;
            X32MessageDispatcher.Instance.FXReturnReceivedEvent += Dispatch_FXReturnReceivedEvent;
            X32MessageDispatcher.Instance.BusReceivedEvent += Dispatch_BusReceivedEvent;
            X32MessageDispatcher.Instance.MatrixReceivedEvent += Dispatch_MatrixReceivedEvent;
            X32MessageDispatcher.Instance.MainReceivedEvent += Dispatch_MainReceivedEvent;
        
        }

        public static Presenter Instance
        {
            get
            {
                return instance;
            }
        }

        public Page CurrentPage;
        public X32ChannelBase selectedChannelType;
        public int selectedChannelIndex;

        private void Dispatch_ChannelReceivedEvent(object sender, ChannelReceivedEventArgs e)
        {
            string[] subs = e.subAddress.Split('/');

            //the first substring will be a null character because the format of the 'subaddress
            //string is '/<parameter>/....'
            switch (subs[1])
            {
                case "config":
                case "mix":
                    X32Console.Instance.Channels[e.channel - 1].SetValuesFromOSC(subs, e.value);
                    break;
                case "delay":
                    break;
                case "preamp":
                    X32Console.Instance.Channels[e.channel - 1].m_PreAmp.SetValuesFromOSC(subs, e.value);
                    break;
                case "gate":
                    X32Console.Instance.Channels[e.channel - 1].m_Gate.SetValuesFromOSC(subs, e.value);
                    break;
                case "dyn":
                    X32Console.Instance.Channels[e.channel - 1].m_Dynamic.SetValuesFromOSC(subs, e.value);
                    break;
                case "eq":
                    X32Console.Instance.Channels[e.channel - 1].m_Eq.SetValuesFromOSC(subs, e.value);
                    break;
                case "grp":
                    break;
            }
            UpdateUI();
        }

        private void Dispatch_AuxinReceivedEvent(object sender, AuxinReceivedEventArgs e)
        {
            //TODO Dispatch channel
            string[] subs = e.subAddress.Split('/');
            switch (subs[1])
            {
                case "config":
                case "mix":
                    X32Console.Instance.AuxInputs[e.channel - 1].SetValuesFromOSC(subs, e.value);
                    break;
                case "preamp":
                    X32Console.Instance.AuxInputs[e.channel - 1].m_PreAmp.SetValuesFromOSC(subs, e.value);
                    break;
                case "eq":
                    X32Console.Instance.AuxInputs[e.channel - 1].m_Eq.SetValuesFromOSC(subs, e.value);
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
                    X32Console.Instance.FXReturns[e.channel - 1].SetValuesFromOSC(subs, e.value);
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
                    X32Console.Instance.MixBusses[e.bus - 1].SetValuesFromOSC(subs, e.value);
                    break;
                case "dyn":
                    X32Console.Instance.MixBusses[e.bus - 1].m_Dynamic.SetValuesFromOSC(subs, e.value);
                    break;
                case "insert":
                    break;
                case "eq":
                    X32Console.Instance.MixBusses[e.bus - 1].m_Eq.SetValuesFromOSC(subs, e.value);
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
                    X32Console.Instance.Matrices[e.matrix - 1].SetValuesFromOSC(subs, e.value);
                    break;
                case "dyn":
                    X32Console.Instance.Matrices[e.matrix - 1].m_Dynamic.SetValuesFromOSC(subs, e.value);
                    break;
                case "insert":
                    break;
                case "eq":
                    X32Console.Instance.Matrices[e.matrix - 1].m_Eq.SetValuesFromOSC(subs, e.value);
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
                    X32Console.Instance.StereoMain.SetValuesFromOSC(subs, e.value);
                    break;
                case "dyn":
                    X32Console.Instance.StereoMain.m_Dynamic.SetValuesFromOSC(subs, e.value);
                    break;
                case "insert":
                    break;
                case "eq":
                    X32Console.Instance.StereoMain.m_Eq.SetValuesFromOSC(subs, e.value);
                    break;
                case "grp":
                    break;
            }
        }

        private void UpdateUI()
        {
            //check if new data is relevant

            //send notification to UI

        }
    }
}
