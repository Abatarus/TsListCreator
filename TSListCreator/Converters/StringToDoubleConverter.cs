using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Utilities;

namespace TSListCreator.Converters
{
    public class StringToDoubleConverter: IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            
            if (value is not double doubleValue)
            { 
                return "0";
            }
            return doubleValue.ToString();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not string strValue)
            {
                return 0;
            }

            if (double.TryParse(strValue, out var doubleValue))
            {
                return doubleValue;
            }
            return 0;
        }
    }
}
