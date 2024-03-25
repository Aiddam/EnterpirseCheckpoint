using Avalonia.Controls;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace EnterpirseCheckpoint.Converters
{
    public class ComboBoxWeekDayConverter : ComboBoxConverter
    {
        public ComboBoxWeekDayConverter() : base(new WeekDayConverter())
        {
        }
    }
}
