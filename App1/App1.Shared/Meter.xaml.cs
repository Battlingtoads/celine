using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace gnow.UI 
{
    public sealed partial class Meter : UserControl
    {
        public Meter()
        {
            this.InitializeComponent();
        }
		///<summary>
		///Sets the fill of the meter
        ///</summary>
        public void SetBackgroundFill(Brush color)
        {
            Background.Fill = color;
        }
        //sets the level of the meter. input should be between 0 and 1
        public void SetLevel(float Value)
        {
            Mask.Height = Background.ActualHeight - Background.ActualHeight * Value;
        }
    }
}
