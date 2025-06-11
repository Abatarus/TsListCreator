namespace TSListCreator.Controls;

public enum AlignmentId
{
    Automatic,
    Left,
    Center,
    Right,
    Justified
}

public class TsTextBox : Control
{
    private AlignmentId _alignment = AlignmentId.Left;
    public AlignmentId Alignment
    {
        get => _alignment;
        set => SetField(ref _alignment, value);
    }

    private int _width = 1000;
    public int Width
    {
        get => _width;
        set => SetField(ref _width, value);
    }

    private string _value = "";
    public string Value
    {
        get => _value;
        set => SetField(ref _value, value);
    }

    private string _label = "";
    public string Label
    {
        get => _label;
        set => SetField(ref _label, value);
    }

    public override string GetJsonString()
    {
        string result = "{";
        result += $"\"pos\":[{PosX},0.1,{PosY}],";
        result += $"\"size\":{Width},";
        result += "\"font_size\":250,";
        result += $"\"width\":{Width},";
        result += $"\"value\":{Value},";
        result += $"\"label\":{Value},";
        result += $"\"alignment\":{(int)Alignment},";
        result += "},";
        return result;
    }
}
