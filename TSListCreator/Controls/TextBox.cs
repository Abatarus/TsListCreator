using System.ComponentModel;
using TSListCreator.Interfaces;

namespace TSListCreator.Controls;
enum AlignmentId
{
    Automatic,
    Left,
    Center,
    Right,
    Justified
}
class TextBox : INotifyPropertyChanged, ILuaInput
{
    private int _width = 1000;
    public int Width
    {
        get => _width;
        set { _width = value; OnPropertyChanged(nameof(Width)); }
    }

    private string _value = "";
    public string Value
    {
        get => _value;
        set { _value = value; OnPropertyChanged(nameof(Value)); }
    }

    private string _label = "";
    public string Label
    {
        get => _label;
        set { _label = value; OnPropertyChanged(nameof(Label)); }
    }

    private double _posX = 0.0;
    public double PosX
    {
        get => _posX;
        set { _posX = value; OnPropertyChanged(nameof(PosX)); }
    }

    private double _posY = 0.0;
    public double PosY
    {
        get => _posY;
        set { _posY = value; OnPropertyChanged(nameof(PosY)); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public string GetLuaString()
    {
        return "";
    }
}
