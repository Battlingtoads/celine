using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
            LinearGradientBrush brsh = new LinearGradientBrush();
            brsh.StartPoint = new Point(.5, 1);
            brsh.EndPoint = new Point(.5, 0);
            brsh.GradientStops.Add(new GradientStop { Color = new Color() { A=255, G = 255 }, Offset = 0.0 });
            brsh.GradientStops.Add(new GradientStop { Color = new Color() { A=255, G = 255, R=255}, Offset = 0.70 });
            brsh.GradientStops.Add(new GradientStop { Color = new Color() { A=255, R = 255 }, Offset = 1.0 });
            meter.SetBackgroundFill(brsh);
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            meter.SetLevel((float)e.NewValue/100);
            SetTextValue(mapLogarithmic((float)e.NewValue));
            OnFaderValueChangedEvent(e);
        }

        public void SetTextValue(float val)
        {
            if (val < -100)
                valueBox.Text = "-" + '\u221E'.ToString();
            else
                valueBox.Text = val.ToString("######.00");
        }

        public void SetFaderValue(float val)
        {
            slider0.Value = val*100;           
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

        public float mapLogarithmic(float value)
        {
            float retVal;
            if(value == 0)
            {
                return -1000;
            }
            else if(value == 1)
            {
                return -82;
            }
            else if (value <= 70)
            {
                retVal = (float)Math.Log(value);
                return retVal * 21.183f - 90;
            }
            else
            {
                return value * 10 / 30 - 23.3333f;
            }
        }
    }
}
