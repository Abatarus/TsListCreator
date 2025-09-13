using System.ComponentModel.Design;
using System.Globalization;
using System.Text.Json.Nodes;
using System.Windows.Input;
using TsListCreator.Model.Converters;
using TsListCreator.Model.Interfaces;
using TsListCreator.Model.Models;
using TsListCreator.Model.Utils;
using TsListCreator.Shared.Enums;
using TsListCreator.Shared.Services;
using TsListCreator.Shared.ViewModels.Controls;

namespace TsListCreator.Model.ViewModels.Controls;
public abstract class ControlViewModel(TsControl control, ISettingsService settingsService, IEditorDataService editorDataService)
   : DataModel, IControlViewModel, ICanvasDrawable, IRedraw
{
    protected readonly IEditorDataService _editorDataService = editorDataService;
    protected readonly CanvasCoorToTsPosConverter _posConverter = new (settingsService, editorDataService);
    protected readonly CanvasCoorToTsSizeConverter _sizeConverter = new (settingsService, editorDataService);
    private string _name = "";
    public string Name
    {
        get => control.Name;
        set
        {
            control.Name = value;
            OnPropertyChanged();
        }
    }

    protected double _posX = 0.0;
    public double PosX
    {
        get => _posX;
        set => SetField(ref _posX, value);
    }
    public double CanvasPosX
    {
        get => _posConverter.Convert(control.PosX, "Width");
        set
        {
            control.PosX = _posConverter.ConvertBack(value, "Width");
            OnPropertyChanged();
        }
    }
    public double CanvasPosY
    {
        get => _posConverter.Convert(control.PosY, "Height");
        set
        {
            control.PosY = _posConverter.ConvertBack(value, "Height");
            OnPropertyChanged();
        }
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

    public ICommand Delete
    {
        get => _delete;
        set => _delete = value;
    }

    public abstract JsonObject GetJsonObject();
    public abstract string GetLuaString();

    private Action<ControlViewModel> _removeMe;
    private ICommand _delete;

    public void SetRemove(Action<ControlViewModel> removeMe)
    {
        Delete = new Command<ControlViewModel>(removeMe);
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
