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
using gnow.util;
using gnow.util.behringer;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GateView : Page, IGateView
    {
        GatePresenter presenter;
        public GateView()
        {
            this.InitializeComponent();
            presenter = new GatePresenter(this);
        }

        private void ThresholdKnob_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if(Graph != null)
                Graph.threshHoldValue = e.NewValue;
        }

        public float Threshold
        {
            get
            {
                return (float)ThresholdKnob.Value;
            }
            set
            {
                ThresholdKnob.SetValue(value);
                if (Graph != null)
                    Graph.threshHoldValue = value;
            }
        }

        public float Range
        {
            get
            {
                return (float)RangeKnob.Value;
            }
            set
            {
                RangeKnob.SetValue(value);
            }
        }


        public float Attack
        {
            get
            {
                return (float)AttackKnob.Value;
            }
            set
            {
                AttackKnob.SetValue(value);
            }
        }

        public float Hold
        {
            get
            {
                return (float)HoldKnob.Value;
            }
            set
            {
                HoldKnob.SetValue(value);
            }
        }

        public float Release
        {
            get
            {
                return (float)ReleaseKnob.Value;
            }
            set
            {
                ReleaseKnob.SetValue(value);
            }
        }

        public int SelectedChannelIndex
        {
            get;
            set;
        }

        public Constants.COMPONENT_TYPE SelectedChannelType
        {
            get;
            set;
        }
    }
}
