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
            X32MessageDispatcher.Instance.ChannelReceivedEvent += OSCReceived;
        }
        private void FaderChangedUI(object sender, FaderValueChangedArgs e)
        {
            view.Bank.setLevel(e.offset, e.value);
        }

        private void MuteChangedUI(object sender, MuteValueChangedArgs e)
        {
            view.Bank.setMute(e.offset, e.value);
        }

        private void OSCReceived(object sender, ChannelReceivedEventArgs e)
        {
        }
    }
    public delegate void FaderValueChangedEventHandler(object sender, FaderValueChangedArgs e);
    public delegate void MuteValueChangedEventHandler(object sender, MuteValueChangedArgs e);

}
