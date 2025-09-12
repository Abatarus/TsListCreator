using System.Globalization;
using Avalonia.Data.Converters;

namespace TsListCreator.Presentation.Converters;

class SideSpaceConverter: IValueConverter
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
                return doubleValue - doubleParameter;
            }
        }

        return 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}