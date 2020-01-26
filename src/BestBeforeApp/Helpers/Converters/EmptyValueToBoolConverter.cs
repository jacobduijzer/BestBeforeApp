using System;
using Xamarin.Forms;

namespace BestBeforeApp.Helpers.Converters
{
    public class EmptyValueToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => !string.IsNullOrEmpty($"{value}");

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => value;
    }
}

