using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using gnow.util.behringer;

namespace App1
{
    public sealed class Presenter
    {
        private static readonly Presenter instance = new Presenter();

        private Presenter() { }

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
    }
}
