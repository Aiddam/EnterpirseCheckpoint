using Avalonia.Data.Converters;
using EnterpriseCheckpoint.Models.Models;
using System;
using System.Globalization;

namespace EnterpirseCheckpoint.Converters
{
    public class LoginConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var user = value as User;
            if (user is null) return "Log in";
            return "Log out";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
