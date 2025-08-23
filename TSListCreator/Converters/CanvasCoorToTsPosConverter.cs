using System;
using System.Globalization;
using Avalonia.Data.Converters;
using TSListCreator.Interfaces;
using TSListCreator.Services;

namespace TSListCreator.Converters
{
    public class CanvasCoorToTsPosConverter(ISettingsService settingsService, IImageDataService imageDataService) : IValueConverter
    {
        IImageDataService _imageDataService = imageDataService;
        ISettingsService _settingsService = settingsService;
        public CanvasCoorToTsPosConverter(): this(ConverterServiceContainer.Instance.SettingsService, ConverterServiceContainer.Instance.ImageDataService)
        {}
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
                if (strParameter == "Height")
                {
                    sizeBound = _settingsService.BoundHeight;
                    sizeEm = _imageDataService.GetImageHeight();
                }
                else if (strParameter == "Width")
                {
                    sizeBound = _settingsService.BoundWidth;
                    sizeEm = _imageDataService.GetImageWidth();
                }
            }

            double TsPosToEm = sizeEm / sizeBound;
            double halfSizeEm = sizeEm / 2;

            double result = doubleValue * TsPosToEm + halfSizeEm;
            return result;
        }
        //to TsCoor
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
                if (strParameter == "Height")
                {
                    sizeBound = _settingsService.BoundHeight;
                    sizeEm = _imageDataService.GetImageHeight();
                }
                else if (strParameter == "Width")
                {
                    sizeBound = _settingsService.BoundWidth;
                    sizeEm = _imageDataService.GetImageWidth();
                }
            }

            double EmToTsPos = sizeBound / sizeEm;
            double halfSizeEm = sizeBound / 2;

            double result = doubleValue * EmToTsPos - halfSizeEm;
            return result;
        }
    }
}
