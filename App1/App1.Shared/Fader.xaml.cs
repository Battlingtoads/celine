using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace gnow.UI
{
    public sealed partial class Fader : UserControl
    {
        public Fader()
        {
            this.InitializeComponent();
            meter.SetBackgroundFill(new SolidColorBrush(new Color(){A=255, G=255}));
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            meter.SetLevel((float)e.NewValue / 100);
            valueBox.Text = (e.NewValue.ToString());
            OnFaderValueChangedEvent(e);
        }

         
        public delegate void FaderValueChangedEventHandler(object sender, RangeBaseValueChangedEventArgs e);

        public event FaderValueChangedEventHandler FaderValueChangedEvent;

        private void OnFaderValueChangedEvent(RangeBaseValueChangedEventArgs e)
        {
            FaderValueChangedEventHandler handler = FaderValueChangedEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }
    }
}
