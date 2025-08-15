using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using TSListCreator.Controls;

namespace TSListCreator.Converters
{
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
            throw new NotImplementedException();
        }
    }
}
