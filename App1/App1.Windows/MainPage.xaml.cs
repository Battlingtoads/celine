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
using gnow.util.osc;
using System.Diagnostics;
using Windows.UI.Xaml.Shapes;
using gnow.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<Fader> faders;
        List<Meter> meters;
        public MainPage()
        {
            this.InitializeComponent();
            Page_Load();
        }
        

        private void Fader_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            var s = (Fader)sender;
            for (int i = 0; i < 8; i++ )
            {
                meters[faders.IndexOf(s) + (i * 8)].SetLevel((float)e.NewValue / 100);
            }
            Debug.WriteLine(s.Name);


        }
        void Page_Load()
        {
            meters = new List<Meter>();

            //setup meters
            for(int i = 0; i < 64; i++)
            {
                Meter meterBase = new Meter();
                meterBase.SetBackgroundFill( new SolidColorBrush(new Color() { A = 255, B = 00, G = 255, R = 00 }));
                meterBase.SetValue(MarginProperty, new Thickness() { Left = 2, Right = 2 });
                meters.Add(meterBase);
                meterGrid.Children.Add(meters[i]);
                Grid.SetRow(meters[i], 0);
                Grid.SetColumn(meters[i], i);

                
            }

            faders = new List<Fader>();
            //faders
            for(int i = 0; i < 8; i++)
            {
                Fader faderBase = new Fader();
                faderBase.FaderValueChangedEvent += Fader_ValueChanged;
                faders.Add(faderBase);
                MainGrid.Children.Add(faders[i]);
                Grid.SetRow(faders[i], 2);
                Grid.SetColumn(faders[i], i+1);
            }

        }

    }
}
