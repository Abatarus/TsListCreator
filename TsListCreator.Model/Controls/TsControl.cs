using System.Globalization;
using System.Text.Json.Nodes;
using TsListCreator.Model.Converters;
using TsListCreator.Model.Interfaces;
using TsListCreator.Model.Utils;
using TsListCreator.Shared.Enums;
using TsListCreator.Shared.Services;

namespace TsListCreator.Model.Controls;
public abstract class TsControl(ISettingsService settingsService, IEditorDataService editorDataService)
   : DataModel, ICanvasDrawable, IJsonInput, ILuaInput, IRedraw
{
    protected readonly IEditorDataService _editorDataService = editorDataService;
    protected readonly CanvasCoorToTsPosConverter _posConverter = new (settingsService, editorDataService);
    protected readonly CanvasCoorToTsSizeConverter _sizeConverter = new (settingsService, editorDataService);
    private string _name = "";
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    protected double _posX = 0.0;
    public double PosX
    {
        get => _posX;
        set => SetField(ref _posX, value);
    }
    public double CanvasPosX
    {
        get => (double)_posConverter.Convert(_posX, "Width");
        set => SetField(ref _posX, (double)_posConverter.ConvertBack(value, "Width"));
    }

    protected double _posY = 0.0;
    public double PosY
    {
        get => _posY;
        set => SetField(ref _posY, value);
    }
    public double CanvasPosY
    {
        get => (double)_posConverter.Convert(_posY, "Height");
        set => SetField(ref _posY, (double)_posConverter.ConvertBack(value, "Height"));
    }

    private double _width = 300;

    public virtual double Width
    {
        get => _width;
        set
        {
            SetField(ref _width, value);
            OnPropertyChanged(nameof(CanvasWidth));
        }
    }
    public double CanvasWidth
    {
        get => (double)_sizeConverter.Convert(Width, "Width");
        set => Width = (double)_sizeConverter.ConvertBack(value, "Width");
    }
    private double _canvasMinHeight = 1;
    public virtual double CanvasMinHeight
    {
        get => _canvasMinHeight;
        set => SetField(ref _canvasMinHeight, value);
    }
    private double _canvasMinWidth = 1;
    public virtual double CanvasMinWidth
    {
        get => _canvasMinWidth;
        set => SetField(ref _canvasMinWidth, value);
    }

    private double _height = 300;
    public virtual double Height
    {
        get => _height;
        set
        {
            SetField(ref _height, value);
            OnPropertyChanged(nameof(CanvasHeight));
        }
    }
    public double CanvasHeight
    {
        get => (double)_sizeConverter.Convert(Height, "Height");
        set => Height = (double)_sizeConverter.ConvertBack(value, "Height");
    }


    private bool _isHighlighted = false;
    public bool IsHighlighted
    {
        get => _isHighlighted;
        set => SetField(ref _isHighlighted, value);
    }

    public void Delete()
    {
        _removeMe(this);
    }
    public abstract JsonObject GetJsonObject();
    public abstract string GetLuaString();

    private Action<TsControl> _removeMe;

    public void SetRemove(Action<TsControl> removeMe)
    {
        _removeMe = removeMe; ;
    }

    public void Redraw()
    {
        foreach (var property in GetType().GetProperties())
        {
            OnPropertyChanged(property.Name);
        }
    }

    public Mode Mode
    {
        get => _editorDataService.Mode;
        set
        {
            _editorDataService.Mode = value;
            OnPropertyChanged();
        }
    }
    public bool Magnet
    {
        get => _editorDataService.Magnet;
        set
        {
            _editorDataService.Magnet = value;
            OnPropertyChanged();
        }
    }
}
