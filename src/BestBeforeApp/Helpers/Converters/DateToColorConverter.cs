using System;
using System.Globalization;
using Xamarin.Forms;

namespace BestBeforeApp.Helpers.Converters
{
    public class DateToColorConverter : IValueConverter
    {
        private static Color Green = Color.FromHex("#01B367");
        private static Color Orange = Color.FromHex("#EF8626");
        private static Color Red = Color.FromHex("#ED3F62");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse(value.ToString(), out DateTime date))
            {
                if (date.Date <= DateTime.Now.Date)
                    return Red;
                else if (date.Date >= DateTime.Now && date.Date <= DateTime.Now.AddDays(14))
                    return Orange;
            }

            return Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
