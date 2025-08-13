using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using TSListCreator.Services;

namespace TSListCreator.Converters
{
    public class CanvasCoorToTsPosConverter: IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not double doubleValue)
            {
                return 0;
            }
            double sizePix = 0;
            double sizeEm = 0;
            if (parameter is string strParameter)
            {
                ImageDataService imageDataService = SingletonServiceContainer.Instance.ImageDataService;
                if (strParameter == "Height")
                {
                    sizePix = imageDataService.GetImagePixHeight();
                    sizeEm = imageDataService.GetImageHeight();
                }
                else if(strParameter == "Width")
                {
                    sizePix = imageDataService.GetImagePixWidth();
                    sizeEm = imageDataService.GetImageWidth();
                }
            }

            const double pixSize = 0.001;
            int sign = Math.Sign(doubleValue);
            double halfSizePix = sizePix / 2;
            double imageSizedValue = doubleValue / pixSize;
            if (sign < 0)
            {
                imageSizedValue += halfSizePix;
            }
            return imageSizedValue;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
