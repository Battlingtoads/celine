using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace gnow.util
{
    public interface IView
    {
        void PresenterUpdate(PresenterUpdateArgs);
        IEnumberable<Part> Parts { get; set; }
    }
}
