using System;
using System.Globalization;
using System.Windows.Data;

namespace Labb3.Converters
{
    internal class RadioButtonCheckedConverter : IValueConverter
    {
        // Klass för att konvertera radioknapparna till integers.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integer = (int) value;
            if (integer == int.Parse(parameter.ToString()))
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
