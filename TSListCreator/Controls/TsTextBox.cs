using System.ComponentModel;

namespace TSListCreator.Controls;

public enum AlignmentId
{
    [Description("Автоматически")] Automatic,
    [Description("Лево")] Left,
    [Description("Центру")] Center,
    [Description("Право")] Right,
    [Description("Ширине")] Justified
}

public class TsTextBox : TsControl
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
