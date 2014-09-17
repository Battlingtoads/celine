using gnow.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailView : Page, IDetailView
    {
        DetailPresenter presenter;
        public DetailView()
        {
            this.InitializeComponent();
            presenter = new DetailPresenter(this);
        }

        private void RefreshView_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 1; i < 17; i++)
            {
                Sends.SetLevel(i, .5f);
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        public float Gain
        {
            get
            {
                return preamp.GetGain();
            }
            set
            {
                preamp.SetGain(value);
            }
        }

        public float GateThreshold
        {
            get
            {
                return Gate.GetThreshold();
            }
            set
            {
                Gate.SetThreshold(value);
            }
        }

        public float DynamicThreshold
        {
            get
            {
                return Dynamic.GetThreshold();
            }
            set
            {
                Dynamic.SetThreshold(value);
            }
        }

        public bool PhantomPower
        {
            get
            {
                return (bool)preamp.GetPhantomPower();
            }
            set
            {
                preamp.SetPhantomPower(value);
            }
        }

        public bool Phase
        {
            get
            {
                return (bool)preamp.GetPhase();
            }
            set
            {
                preamp.SetPhase(value);
            }
        }

        public bool Link
        {
            get
            {
                return (bool)preamp.GetLink();
            }
            set
            {
                preamp.SetLink(value);
            }
        }

        public int SelectedChannelIndex
        {
            get;
            set;
        }

        public gnow.util.behringer.Constants.COMPONENT_TYPE SelectedChannelType
        {
            get;
            set;
        }
    }
}
