using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls.Primitives;
using gnow.util.behringer;
using gnow.util.behringer.events;

namespace gnow.util
{
    class MainPresenter : Presenter
    {
        private new IMainView view;
        public MainPresenter(IMainView view)
        {
            this.view = view;
            this.view.FaderValueChanged += FaderChangedUI;
            this.view.MuteValueChanged += MuteChangedUI;
            X32MessageDispatcher.Instance.ChannelReceivedEvent += ChannelOSCReceived;
        }
        private void FaderChangedUI(object sender, FaderValueChangedArgs e)
        {
            view.Bank.setLevel(e.offset, e.value);
        }

        private void MuteChangedUI(object sender, MuteValueChangedArgs e)
        {
            view.Bank.setMute(e.offset, e.value);
        }

        private void ChannelOSCReceived(object sender, ChannelReceivedEventArgs e)
        {
            string[] subs = e.subAddress.Split('/');
            switch(subs[1])
            {
                case "mix":
                    switch (subs[2])
                    {
                        case "fader":
                            X32Level level = new X32Level(0, 1024);
                            level.RawLevel = (float)e.value;
                            List<float> faderLevels = view.FaderValues as List<float>;
                            switch(view.Bank.getEnum())
                            {
                                case Constants.FADER_GROUP.CHANNEL_1_8:
                                    faderLevels[e.channel - 1] = level.DbFSLevel;
                                    break;
                                case Constants.FADER_GROUP.CHANNEL_9_16:
                                    faderLevels[e.channel - 9] = level.DbFSLevel;
                                    break;
                                case Constants.FADER_GROUP.CHANNEL_17_24:
                                    faderLevels[e.channel - 17] = level.DbFSLevel;
                                    break;
                                case Constants.FADER_GROUP.CHANNEL_25_32:
                                    faderLevels[e.channel - 25] = level.DbFSLevel;
                                    break;
                            }
                            break;
                    }

                    break;
                default:
                    //do nothing
                    break;
            }
        }
    }
    public delegate void FaderValueChangedEventHandler(object sender, FaderValueChangedArgs e);
    public delegate void MuteValueChangedEventHandler(object sender, MuteValueChangedArgs e);

}
