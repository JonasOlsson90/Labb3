using System;
using System.Globalization;
using System.Windows.Data;

namespace Labb3.Converters
{
    internal class RadioButtonCheckedConverter : IValueConverter
    {
        // Klass för att konvertera vald radioknapps värde till en integer. Hämtat från stackoverflow och codeproject,
        // dubbelkollat med dokumentationen tills jag kände mig säker nog för att använda det. 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integer = (int) value;
            return integer == int.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
