using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml.Data;

namespace App1
{
    class RatioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = (double)value;
            if(v != 0)
            {
                string ratioString;
                switch ((int)v)
                {
                    case 1:
                        ratioString = "1.1:1";
                        break;
                    case 2:
                        ratioString = "1.3:1";
                        break;
                    case 3:
                        ratioString = "1.5:1";
                        break;
                    case 4:
                        ratioString = "2:1";
                        break;
                    case 5:
                        ratioString = "2.5:1";
                        break;
                    case 6:
                        ratioString = "3:1";
                        break;
                    case 7:
                        ratioString = "4:1";
                        break;
                    case 8:
                        ratioString = "5:1";
                        break;
                    case 9:
                        ratioString = "7:1";
                        break;
                    case 10:
                        ratioString = "10:1";
                        break;
                    case 11:
                        ratioString = "20:1";
                        break;
                    case 12:
                        ratioString = "100:1";
                        break;
                    default:
                        ratioString = "";
                        break;
                }
                return ratioString;
            }
            else
                return "1:1";
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
