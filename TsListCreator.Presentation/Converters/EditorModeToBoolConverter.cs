using System.Globalization;
using Avalonia.Data.Converters;
using TsListCreator.Shared.Enums;

namespace TsListCreator.Presentation.Converters;

public class EditorModeToBoolConverter: IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Mode mode && parameter is string paramStr)
        {
            if (Enum.TryParse<Mode>(paramStr, out var paramMode))
                return mode == paramMode;
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b && b && parameter is string paramStr)
        {
            if (Enum.TryParse<Mode>(paramStr, out var paramMode))
                return paramMode;
        }
        return Avalonia.Data.BindingOperations.DoNothing;
    }
}