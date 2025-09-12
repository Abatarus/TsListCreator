using System.Globalization;
using Avalonia.Data.Converters;

namespace TsListCreator.Presentation.Converters;

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
            return 0.0;
        }

        if (double.TryParse(strValue, out var doubleValue))
        {
            return doubleValue;
        }
        return 0.0;
    }
}