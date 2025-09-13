using TsListCreator.Model.Interfaces;

namespace TsListCreator.Model.Converters;

public class CanvasCoorToTsSizeConverter(ISettingsService settingsService, IEditorDataService imageDataService)
{
    private readonly IEditorDataService _editorDataService = imageDataService;
    private readonly ISettingsService _settingsService = settingsService;
    public double Convert(double value,  string parameter)
    {
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
        double portionOfValue = (value * tsSizeToBound) / sizeBound;
        double result = portionOfValue * sizeEm;
        return result;
    }

    public double ConvertBack(double value, string parameter)
    {
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

        double portionOfValue = value / sizeEm;
        double boundToTsSize = 15100 / 3.141;
        double result = portionOfValue * sizeBound * boundToTsSize;
        return result;
    }
}