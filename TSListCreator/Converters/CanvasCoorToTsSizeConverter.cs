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
    public class CanvasCoorToTsSizeConverter(ISettingsService settingsService, IEditorDataService imageDataService) : IValueConverter
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

            double portionOfValue = doubleValue / sizeEm;
            double boundToTsSize = 15100 / 3.141;
            double result = portionOfValue * sizeBound * boundToTsSize;
            return result;
        }
    }
}
