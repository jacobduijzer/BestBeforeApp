using System;
using System.Globalization;
using Xamarin.Forms;

namespace BestBeforeApp.Helpers.Converters
{
    public class InvertedBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            !(bool)value;


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            (bool)value;
    }
}
