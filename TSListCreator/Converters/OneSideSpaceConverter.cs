﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;

namespace TSListCreator.Converters
{
    class OneSideSpaceConverter: IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not double doubleValue)
            {
                return 0;
            }
            if (parameter is string strParameter)
            {
                if(double.TryParse(strParameter, NumberStyles.AllowDecimalPoint, culture, out double doubleParameter))
                {
                    return doubleValue - doubleParameter / 2;
                }
            }

            return 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
