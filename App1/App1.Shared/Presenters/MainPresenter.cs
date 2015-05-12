using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls.Primitives;
using gnow.util.behringer;
using gnow.util.behringer.events;
using gnow.UI;

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
            if((e.channel < 9 && view.Bank.getEnum() != Constants.FADER_GROUP.CHANNEL_1_8) ||
               (e.channel > 8 && e.channel < 17 && view.Bank.getEnum() != Constants.FADER_GROUP.CHANNEL_9_16) ||
               (e.channel > 16 && e.channel < 25 && view.Bank.getEnum() != Constants.FADER_GROUP.CHANNEL_17_24) ||
               (e.channel > 24 && e.channel < 33 && view.Bank.getEnum() != Constants.FADER_GROUP.CHANNEL_25_32))
            {
                //don't need to update
                return;
            }
            string[] subs = e.subAddress.Split('/');
            switch(subs[1])
            {
                case "mix":
                    switch (subs[2])
                    {
                        case "fader":
                            List<float> faderLevels = view.FaderValues as List<float>;
                            X32Level level = new X32Level(0, 1024);
                            level.RawLevel = (float)e.value;
                            faderLevels[(e.channel - 1) % 8] = level.RawLevel;
                            view.FaderValues = faderLevels;
                            break;
                        case "on":
                            List<bool> mutes = view.Mutes as List<bool>;
                            bool mVal = Convert.ToBoolean((int)e.value);
                            mutes[(e.channel - 1) % 8] = mVal;
                            view.Mutes = mutes;
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
