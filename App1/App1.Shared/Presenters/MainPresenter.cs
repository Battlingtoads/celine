using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls.Primitives;
using gnow.util.behringer;

namespace gnow.util
{
    class MainPresenter : Presenter
    {
        private new IMainView view;
        public MainPresenter(IMainView view)
        {
            this.view = view;
            this.view.FaderValueChanged += FaderChangedUI;
        }
        private void FaderChangedUI(object sender, FaderValueChangedArgs e)
        {
            string address;
            string subAddress;
            Constants.COMPONENT_TYPE type;
            int channel;
            List<float> faders = view.FaderValues as List<float>;
            switch (view.Bank)
            {
                case Constants.FADER_GROUP.CHANNEL_1_8:
                    subAddress = "/mix/fader";
                    channel = (1 + e.offset);
                    address = "/ch/" + channel.ToString().PadLeft(2, '0') + subAddress;
                    X32Console.Instance.Channels[channel].Level.DbFSLevel = e.value;
                    type = Constants.COMPONENT_TYPE.CHANNEL;
                    break;
                case Constants.FADER_GROUP.CHANNEL_9_16:
                    subAddress = "/mix/fader";
                    channel = (9 + e.offset);
                    address = "/ch/" + channel.ToString().PadLeft(2, '0') + subAddress;
                    X32Console.Instance.Channels[channel].Level.DbFSLevel = e.value;
                    type = Constants.COMPONENT_TYPE.CHANNEL;
                    break;
                case Constants.FADER_GROUP.CHANNEL_17_24:
                    subAddress = "/mix/fader";
                    channel = (17 + e.offset);
                    address = "/ch/" + channel.ToString().PadLeft(2, '0') + subAddress;
                    X32Console.Instance.Channels[channel].Level.DbFSLevel = e.value;
                    type = Constants.COMPONENT_TYPE.CHANNEL;
                    break;
                case Constants.FADER_GROUP.CHANNEL_25_32:
                    subAddress = "/mix/fader";
                    channel = (25 + e.offset);
                    address = "/ch/" + channel.ToString().PadLeft(2, '0') + subAddress;
                    X32Console.Instance.Channels[channel].Level.DbFSLevel = e.value;
                    type = Constants.COMPONENT_TYPE.CHANNEL;
                    break;
                case Constants.FADER_GROUP.AUX_1_8:
                    subAddress = "/mix/fader";
                    channel = 1 + e.offset;
                    address = "/auxin/" + channel.ToString().PadLeft(2, '0') + subAddress;
                    X32Console.Instance.AuxInputs[channel].Level.DbFSLevel = e.value;
                    type = Constants.COMPONENT_TYPE.AUX_INPUT;
                    break;
                case Constants.FADER_GROUP.BUS_1_8:
                    subAddress = "/mix/fader";
                    channel = 1 + e.offset;
                    address = "/bus/" + channel.ToString().PadLeft(2, '0') + subAddress;
                    X32Console.Instance.MixBusses[channel].Level.DbFSLevel = e.value;
                    type = Constants.COMPONENT_TYPE.MIX_BUS;
                    break;
                case Constants.FADER_GROUP.BUS_9_16:
                    subAddress = "/mix/fader";
                    channel = 9 + e.offset;
                    address = "/bus/" + channel.ToString().PadLeft(2, '0') + subAddress;
                    X32Console.Instance.MixBusses[channel].Level.DbFSLevel = e.value;
                    type = Constants.COMPONENT_TYPE.MIX_BUS;
                    break;
                case Constants.FADER_GROUP.MATRIX_MAIN:
                    subAddress = "/mix/fader";
                    if (e.offset < 6)
                    {
                        channel = 1 + e.offset;
                        type = Constants.COMPONENT_TYPE.MATRIX;
                        address = "/mtx/" + channel.ToString().PadLeft(2, '0') + subAddress;
                        X32Console.Instance.Matrices[channel].Level.DbFSLevel = e.value;
                    }
                    else if (e.offset == 6)
                    {
                        channel = 0;
                        type = Constants.COMPONENT_TYPE.MAIN;
                        address = "/main/st/mix/fader";
                        X32Console.Instance.StereoMain.Level.DbFSLevel = e.value;
                    }
                    else
                    {
                        channel = 1;
                        type = Constants.COMPONENT_TYPE.MAIN;
                        address = "/main/m/mix/fader";
                    }
                    break;
                default:
                    address = "/failure";
                    type = Constants.COMPONENT_TYPE.CHANNEL;
                    subAddress = "/failure";
                    channel = 0;
                    break;
            }
        }
    }
    public delegate void FaderValueChangedEventHandler(object sender, FaderValueChangedArgs e);

}
