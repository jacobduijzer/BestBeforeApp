using System;
using System.Globalization;
using Xamarin.Forms;

namespace BestBeforeApp.Helpers.Converters
{
    public class DateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse(value.ToString(), out DateTime date))
            {
                if (date.Date <= DateTime.Now.Date)
                    return Color.Red;
                else if (date.Date >= DateTime.Now && date.Date <= DateTime.Now.AddDays(14))
                    return Color.Orange;
            }

            return Color.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
