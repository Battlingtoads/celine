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
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App1
{
    public sealed partial class Eq : UserControl
    {
        List<int> frequencyStops;
        public Eq()
        {
            this.InitializeComponent();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            frequencyStops = new List<int>() {20, 30, 40, 60, 80, 100, 200, 300, 400, 500, 600, 800, 1000, 2000, 3000, 4000,
                                                    5000, 6000, 8000, 10000, 20000};
            int column = 0;
            foreach(int i in frequencyStops)
            {
                int weight = i;
                if (i > 10000)
                    weight /= 10000;
                else if (i > 1000)
                    weight /= 100;
                else if (i > 100)
                    weight /= 10;
                EqGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength((double)(100 - weight), GridUnitType.Star) });
                Rectangle rect = new Rectangle()
                {
                    Fill = new SolidColorBrush(Colors.Gray),
                    Width = 1,
                    HorizontalAlignment = HorizontalAlignment.Right
                };
                EqGrid.Children.Add(rect);
                Grid.SetColumn(rect, column);
                Grid.SetRowSpan(rect, 6);
                column++;
            }
        }
    }
}
