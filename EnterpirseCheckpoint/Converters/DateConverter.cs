using Avalonia.Data.Converters;
using System;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EnterpirseCheckpoint.Converters
{
    public class DateConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var date = value as DateTime?;
            if (date is null) return null;

            return $"{(date.Value.Hour > 10 ? date.Value.Hour : "0" + date.Value.Hour)}:{(date.Value.Minute > 10 ? date.Value.Minute : "0" + date.Value.Minute)}";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var str = value as string;
            if (str is null) return null;

            var splittedStr = str.Split(':');
            return new DateTime(1, 1, 1, int.Parse(splittedStr[0]), int.Parse(splittedStr[1]), 0);
        }
    }
}
