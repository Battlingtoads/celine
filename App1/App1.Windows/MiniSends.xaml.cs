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
using gnow.UI;
using Windows.UI;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App1
{
    public sealed partial class MiniSends : UserControl
    {
        List<SendLevel> sends;
        public MiniSends()
        {
            this.InitializeComponent();
            InitializeView();
        }

        private void InitializeView()
        {
            sends = new List<SendLevel>();
            
            sendsGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            TextBlock title = new TextBlock();
            title.Text = "Sends";
            title.SetValue(VerticalAlignmentProperty, VerticalAlignment.Center);
            title.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
            title.FontSize = 22;
            sendsGrid.Children.Add(title);

            for(int i = 1; i < 17; i++)
            {
                sendsGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(5, GridUnitType.Star) });
                SendLevel temp = new SendLevel();
                temp.SetBackgroundFill(new SolidColorBrush(Colors.Yellow));
                sendsGrid.Children.Add(temp);
                Grid.SetRow(temp, i);
                sends.Add(temp);
                
            }
        }

        public void SetLevel(int send, float level)
        {
            sends[send - 1].SetLevel(level);
        }
    }
}
