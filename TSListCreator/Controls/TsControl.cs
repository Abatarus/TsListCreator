using TSListCreator.Interfaces;
using TSListCreator.Utils;
namespace TSListCreator.Controls;
public abstract class TsControl : DataModel, IJsonInput
{
    private string _name = "";
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    private double _posX = 0.0;
    public double PosX
    {
        get => _posX;
        set => SetField(ref _posX, value); 
    }

    private double _posY = 0.0;
    public double PosY
    {
        get => _posY;
        set => SetField(ref _posY, value); 
    }

    private bool _isHighlighted = false;
    public bool IsHighlighted
    {
        get => _isHighlighted;
        set => SetField(ref _isHighlighted, value);
    }

    public abstract string GetJsonString();
}
