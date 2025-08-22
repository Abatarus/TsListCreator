using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using TSListCreator.Interfaces;
using TSListCreator.Services;

namespace TSListCreator.Converters
{
    public class CanvasCoorToTsSizeConverter: IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not double doubleValue)
            {
                return 0;
            }
            double sizeBound = 0;
            double sizeEm = 0;
            if (parameter is string strParameter)
            {
                ISettingsService settingsService = ConverterServiceContainer.Instance.SettingsService;
                IImageDataService imageDataService = ConverterServiceContainer.Instance.ImageDataService;
                if (strParameter == "Height")
                {
                    sizeBound = settingsService.BoundHeight;
                    sizeEm = imageDataService.GetImageHeight();
                }
                else if (strParameter == "Width")
                {
                    sizeBound = settingsService.BoundWidth;
                    sizeEm = imageDataService.GetImageWidth();
                }
            }
            double tsSizeToBound = 3.141 / 15100;
            double portionOfValue = (doubleValue * tsSizeToBound) / sizeBound;
            double result = portionOfValue * sizeEm;
            return result;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not double doubleValue)
            {
                return 0;
            }
            double sizeBound = 0;
            double sizeEm = 0;
            if (parameter is string strParameter)
            {
                ISettingsService settingsService = ConverterServiceContainer.Instance.SettingsService;
                IImageDataService imageDataService = ConverterServiceContainer.Instance.ImageDataService;
                if (strParameter == "Height")
                {
                    sizeBound = settingsService.BoundHeight;
                    sizeEm = imageDataService.GetImageHeight();
                }
                else if (strParameter == "Width")
                {
                    sizeBound = settingsService.BoundWidth;
                    sizeEm = imageDataService.GetImageWidth();
                }
            }

            double portionOfValue = doubleValue / sizeEm;
            double boundToTsSize = 15100 / 3.141;
            double result = portionOfValue * sizeBound * boundToTsSize;
            return result;
        }
    }
}
