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
using gnow.util.behringer;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DynamicView : Page
    {
        public DynamicView()
        {
            this.InitializeComponent();
        }

        private void ThresholdKnob_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if(Graph != null)
                Graph.threshHoldValue = e.NewValue;
        }

        private void RatioKnob_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (Graph != null)
                Graph.Ratio = Constants.GetRatioAsFloat((Constants.DYN_RATIO)(int)(e.NewValue - 1));
        }
    }
}
