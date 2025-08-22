using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSListCreator.Converters
{
    class StringToIntConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {

            if (value is not int intValue)
            {
                return "0";
            }
            return intValue.ToString();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not string strValue)
            {
                return 0;
            }

            if (int.TryParse(strValue, out var intValue))
            {
                return intValue;
            }
            return 0;
        }
    }
}
