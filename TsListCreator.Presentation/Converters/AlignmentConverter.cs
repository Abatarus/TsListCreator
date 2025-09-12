using System.ComponentModel;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using TsListCreator.Shared.Enums;

namespace TsListCreator.Presentation.Converters;

public class AlignmentConverter: IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not AlignmentId alignment)
        {
            return BindingNotification.ExtractError(new BindingNotification(new Exception("AlignmentConverter error"), BindingErrorType.Error));
        }


        var enumType = typeof(AlignmentId);

        var memberInfos = enumType
            .GetMember(alignment.ToString());

        var enumValueMemberInfo = memberInfos
            .FirstOrDefault(m => m.DeclaringType == enumType);

        var valueAttributes = enumValueMemberInfo
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        var description = ((DescriptionAttribute)valueAttributes[0])
            .Description;

        return description;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string strValue)
        {
            return BindingNotification.ExtractError(new BindingNotification(new Exception("AlignmentConverter error"), BindingErrorType.Error));
        }
        var type = typeof(AlignmentId);
        foreach (var field in type.GetFields())
        {
            var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault();
            if (attr != null && attr.Description == strValue)
                return (AlignmentId)field.GetValue(null);
        }

        var match = Enum.GetNames(type)
            .FirstOrDefault(n => n.Equals(strValue, StringComparison.OrdinalIgnoreCase));
        if (match != null)
            return Enum.Parse(type, match);

        throw new ArgumentException($"No enum with description '{strValue}' found in {type.Name}");
    }
}