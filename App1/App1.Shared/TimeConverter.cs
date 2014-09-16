using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml.Data;

namespace App1
{
    class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = (double)value;
            string format = v.ToString("##0");
            if (!string.IsNullOrEmpty(format))
            {
                return format + "ms";
            }
            else
                return format;
        }
        public object Convert(object value, Type targetType, object parameter, string s)
        {
            return Convert(value, targetType, parameter, CultureInfo.CurrentCulture);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string s)
        {
            return parameter;
        }
    }
}
