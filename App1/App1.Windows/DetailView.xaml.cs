﻿using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailView : Page
    {
        public DetailView()
        {
            this.InitializeComponent();
        }

        private void RefreshView_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 1; i < 17; i++)
            {
                Sends.SetLevel(i, .5f);
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Presenter.Instance.CurrentPage = this;
            NavigationArgs args = (NavigationArgs)e.Parameter;
            Presenter.Instance.selectedChannelType = args.type;
            Presenter.Instance.selectedChannelIndex = args.index;

        }
    }
}
