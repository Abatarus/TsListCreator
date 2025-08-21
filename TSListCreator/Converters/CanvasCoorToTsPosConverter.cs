using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data;
using Avalonia.Data.Converters;
using TSListCreator.Interfaces;
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
            double sizeBound = 0;
            double sizeEm = 0;
            if (parameter is string strParameter)
            {
                IImageDataService imageDataService = ConverterServiceContainer.Instance.ImageDataService;
                ISettingsService settingsService = ConverterServiceContainer.Instance.SettingsService;
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

            double TsPosToEm = sizeEm / sizeBound;
            double halfSizeEm = sizeEm / 2;

            double result = doubleValue * TsPosToEm + halfSizeEm;
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
                IImageDataService imageDataService = ConverterServiceContainer.Instance.ImageDataService;
                ISettingsService settingsService = ConverterServiceContainer.Instance.SettingsService;
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

            double EmToTsPos = sizeBound / sizeEm;
            double halfSizeEm = sizeBound / 2;

            double result = doubleValue * EmToTsPos - halfSizeEm;
            return result;
        }
    }
}
