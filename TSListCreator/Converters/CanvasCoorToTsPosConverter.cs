using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

            //doubleValue =
            return doubleValue;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
