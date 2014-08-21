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

namespace App1
{
    public sealed partial class DynamicOverview : UserControl
    {
        public DynamicOverview()
        {
            this.InitializeComponent();
            gainReduction.SetBackgroundFill(new SolidColorBrush(Colors.Red));

        }


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value);
            titleBlock.Text = value;
            }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DynamicOverview), new PropertyMetadata("text", onValueChanged));


        private static void onValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {

        }
        
        
    }
}
