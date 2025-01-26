using System.ComponentModel; using TSListCreator.Interfaces;

namespace TSListCreator.Controls;
enum AlignmentId
{
    Automatic,
    Left,
    Center,
    Right,
    Justified
}
class TextBox : INotifyPropertyChanged, IJsonInput
{
    private AlignmentId _alignment = AlignmentId.Left;
    public AlignmentId Alignment
    {
        get => _alignment;
        set { _alignment = value; OnPropertyChanged(nameof(Alignment)); }
    }

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

    public string GetJsonString()
    {
        return "";
    }
}
