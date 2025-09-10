using System;
using System.Globalization;
using Avalonia.Data.Converters;
using TSListCreator.Interfaces;
using TSListCreator.Services;

namespace TSListCreator.Converters
{
    public class CanvasCoorToTsPosConverter(ISettingsService settingsService, IEditorDataService imageDataService) : IValueConverter
    {
        private readonly IEditorDataService _editorDataService = imageDataService;
        private readonly ISettingsService _settingsService = settingsService;
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
                    sizeEm = _editorDataService.Image.Height;
                }
                else if (strParameter == "Width")
                {
                    sizeBound = _settingsService.BoundWidth;
                    sizeEm = _editorDataService.Image.Width;
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
                    sizeEm = _editorDataService.Image.Height;
                }
                else if (strParameter == "Width")
                {
                    sizeBound = _settingsService.BoundWidth;
                    sizeEm = _editorDataService.Image.Width;
                }
            }

            double EmToTsPos = sizeBound / sizeEm;
            double halfSizeEm = sizeBound / 2;

            double result = doubleValue * EmToTsPos - halfSizeEm;
            return result;
        }
    }
}
