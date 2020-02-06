using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace BestBeforeApp.Helpers.Converters
{
    public class StreamToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            ImageSource.FromStream(() => new MemoryStream((byte[])value));


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
