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
using Windows.System.Threading;
using System.Diagnostics;
using Windows.UI.Xaml.Shapes;
using gnow.UI;
using gnow.util;
using gnow.util.osc;
using gnow.util.behringer;
using gnow.util.behringer.events;
using System.Threading.Tasks;
using Windows.Networking;

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
        List<Button> navButtons;
        Constants.FADER_GROUP currentPage;
        long meterUpdateRate = 1000000;
        public MainPage()
        {
            this.InitializeComponent();
            Page_Load();
            currentPage = 0;

            //Set default fader set to Channel 1-8
            navButtons[0].BorderBrush = new SolidColorBrush(new Color { A = 255, B = 200 });
            X32MessageDispatcher.Instance.ChannelReceivedEvent += Console_FaderChanged;
            X32MessageDispatcher.Instance.MetersReceivedEvent += MeterUpdateReceived;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(meterUpdateRate);
            timer.Tick += timer_Tick;
            timer.Start();

        }

        void timer_Tick(object sender, object e)
        {
#if(DEVEL)
            RequestValues.FromLocal(Constants.METER_TYPE.HOME);
#else
            RequestValues.FromOSC(Constants.METER_TYPE.HOME);
#endif
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void Console_FaderChanged(object sender, ChannelReceivedEventArgs e)
        {
            if (e.subAddress == "/mix/fader")
            {
                faders[e.channel % 8].SetFaderValue((oscFloat)e.value);
            }
            else
            {
                faders[e.channel % 8].setMute((gnow.util.behringer.Constants.ON_OFF)(int)(oscInt)e.value);
            }
        }
        

        private void Fader_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            var s = (Fader)sender;
            string address;
            switch (currentPage)
            {
                case Constants.FADER_GROUP.CHANNEL_1_8:
                    address = "/ch/" + (1 + faders.IndexOf(s)).ToString().PadLeft(2, '0') + "/mix/fader";
                    break;
                case Constants.FADER_GROUP.CHANNEL_9_16:
                    address = "/ch/" + (9 + faders.IndexOf(s)).ToString().PadLeft(2, '0') + "/mix/fader";
                    break;
                case Constants.FADER_GROUP.CHANNEL_17_24:
                    address = "/ch/" + (17 + faders.IndexOf(s)).ToString().PadLeft(2, '0') + "/mix/fader";
                    break;
                case Constants.FADER_GROUP.CHANNEL_25_32:
                    address = "/ch/" + (25 + faders.IndexOf(s)).ToString().PadLeft(2, '0') + "/mix/fader";
                    break;
                case Constants.FADER_GROUP.AUX_1_8:
                    address = "/auxin/" + (1 + faders.IndexOf(s)).ToString().PadLeft(2, '0') + "/mix/fader";
                    break;
                case Constants.FADER_GROUP.BUS_1_8:
                    address = "/bus/" + (1 + faders.IndexOf(s)).ToString().PadLeft(2, '0') + "/mix/fader";
                    break;
                case Constants.FADER_GROUP.BUS_9_16:
                    address = "/bus/" + (9 + faders.IndexOf(s)).ToString().PadLeft(2, '0') + "/mix/fader";
                    break;
                case Constants.FADER_GROUP.MATRIX_MAIN:
                    if (faders.IndexOf(s) < 6)
                    {
                        address = "/mtx/" + (1 + faders.IndexOf(s)).ToString().PadLeft(2, '0') + "/mix/fader";
                    }
                    else if(faders.IndexOf(s) == 6)
                    {
                        address = "/main/st/mix/fader";
                    }
                    else
                    {
                        address = "/main/m/mix/fader";
                    }
                    break;
                default:
                    address = "/failure";
                    break;
            }
            X32Level level = new X32Level(Constants.NO_LEVEL, 1024);
            level.DbFSLevel = Fader.mapLogarithmic((float)e.NewValue);
            OSCMessage msg = new OSCMessage(address, (oscFloat)(level.RawLevel));
            OSCOutPort.Instance.SendAsync(msg);
        }

        private void NavButtonClick(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            if(s == navButtons[(int)currentPage])
            {
                //do nothing
                return;
            }
            s.BorderBrush = new SolidColorBrush(new Color { A = 255, B = 200 });
            navButtons[(int)currentPage].BorderBrush = new SolidColorBrush(Windows.UI.Colors.White);
            currentPage = (Constants.FADER_GROUP)navButtons.IndexOf(s);
            GetChannelValues(currentPage);

        }

        private void MeterUpdateReceived(object sender, MetersReceivedEventArgs e)
        {
            X32MeterGroup meterGroup = new X32MeterGroup(e.data, e.type);
            switch (e.type)
            {
                case gnow.util.behringer.Constants.METER_TYPE.HOME:
                    for (int i = 0; i < meterGroup.Values.Count; i++ )
                    {
                        meters[i].SetLevel(meterGroup.Values[i]);
                    }
                    break;

            }

        }

        private void GetChannelValues(Constants.FADER_GROUP group)
        {
#if DEVEL
            RequestValues.FromLocal(group);
#else
            //ignore warning, it doesn't matter how long this takes
            RequestValues.FromOSC(group);
#endif
        }

        private void Page_Load()
        {
            meters = new List<Meter>();

            //setup meters
            for(int i = 0; i < meterGrid.ColumnDefinitions.Count; i++)
            {
                Meter meterBase = new Meter();
                LinearGradientBrush brsh = new LinearGradientBrush();
                brsh.StartPoint = new Point(.5, 1);
                brsh.EndPoint = new Point(.5, 0);
                brsh.GradientStops.Add(new GradientStop { Color = new Color() { A=255, G = 255 }, Offset = 0.0 });
                brsh.GradientStops.Add(new GradientStop { Color = new Color() { A=255, G = 255, R=255}, Offset = 0.70 });
                brsh.GradientStops.Add(new GradientStop { Color = new Color() { A=255, R = 255 }, Offset = 1.0 });
                meterBase.SetBackgroundFill(brsh);
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

            //Navigation
            navButtons = new List<Button>();
            for(int i = 0; i < 10; i++)
            {
                Button buttonBase = new Button();
                switch (i)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        buttonBase.Content = "Channel " + (i * 8 + 1).ToString() + "-" + (i * 8 + 8).ToString();
                        break;
                    case 4:
                        buttonBase.Content = "Aux 1-8";
                        break;
                    case 5:
                        buttonBase.Content = "FX Returns";
                        break;
                    case 6:
                        buttonBase.Content = "Bus 1-8";
                        break;
                    case 7:
                        buttonBase.Content = "Bus 9-16";
                        break;
                    case 8:
                        buttonBase.Content = "Matrix/Main";
                        break;
                    case 9:
                        buttonBase.Content = "DCA 1-8";
                        break;
                }
                buttonBase.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                buttonBase.SetValue(VerticalAlignmentProperty, VerticalAlignment.Stretch);
                buttonBase.Click += NavButtonClick;
                navButtons.Add(buttonBase);
                navGrid.Children.Add(buttonBase);
                Grid.SetColumn(buttonBase, i);
                Grid.SetRow(buttonBase, 0);

            }
            
        }
    }
}
