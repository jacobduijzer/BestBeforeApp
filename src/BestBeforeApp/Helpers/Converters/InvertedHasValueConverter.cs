using System;
using System.Globalization;
using Xamarin.Forms;

namespace BestBeforeApp.Helpers.Converters
{
    public class InvertedHasValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value == null;


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            value != null;
    }
}
