using System;
using System.Globalization;
using System.Text.Json.Nodes;
using System.Windows.Input;
using TSListCreator.Converters;
using TSListCreator.Interfaces;
using TSListCreator.Utils;
namespace TSListCreator.Controls;
public abstract class TsControl : DataModel, IJsonInput
{
    private CanvasCoorToTsPosConverter posConverter = new CanvasCoorToTsPosConverter();
    private string _name = "";
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    private double _posX = 0.0;
    public double PosX
    {
        get => (double)posConverter.Convert(_posX, null, "Width", CultureInfo.CurrentCulture);
        set => SetField(ref _posX, (double)posConverter.ConvertBack(value, null, "Width", CultureInfo.CurrentCulture)); 
    }

    private double _posY = 0.0;
    public double PosY
    {
        get => (double)posConverter.Convert(_posY, null, "Height", CultureInfo.CurrentCulture);
        set => SetField(ref _posY, (double)posConverter.ConvertBack(value, null, "Height", CultureInfo.CurrentCulture));
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
    private Action<TsControl> _removeMe;
    public void SetRemove(Action<TsControl> removeMe)
    {
        _removeMe = removeMe;;
    }
}
