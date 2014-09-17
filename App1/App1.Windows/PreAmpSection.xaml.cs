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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App1
{
    public sealed partial class PreAmpSection : UserControl
    {
        public PreAmpSection()
        {
            this.InitializeComponent();
            gain.ValueChanged += gain_ValueChanged;
        }

        void gain_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            gainValue.Text = e.NewValue.ToString("##.00") + "dB";
        }

        public void SetGain(double v)
        {
            gain.SetValue(v);
        }

        public float GetGain()
        {
            return (float)gain.Value;
        }

        public void SetPhantomPower(bool b)
        {
            phantomPower.IsChecked = b;
        }

        public bool? GetPhantomPower()
        {
            return phantomPower.IsChecked;
        }

        public void SetPhase(bool b)
        {
            phase.IsChecked = b;
        }

        public bool? GetPhase()
        {
            return phase.IsChecked;
        }

        public void SetLink(bool b)
        {
            link.IsChecked = b;
        }

        public bool? GetLink()
        {
            return link.IsChecked;
        }
    }
}
