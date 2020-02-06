using System;
using System.Globalization;
using Xamarin.Forms;

namespace BestBeforeApp.Helpers.Converters
{
    public class DateToTextDecorationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse(value.ToString(), out DateTime date) && date.Date <= DateTime.Now.Date)
                return TextDecorations.Strikethrough;                

            return TextDecorations.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
