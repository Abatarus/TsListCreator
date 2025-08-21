using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using TSListCreator.Converters;

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
        var converter = new CanvasCoorToTsPosConverter();
        double posX = (double)converter.Convert(PosX, null, "Width", CultureInfo.CurrentCulture);
        double posY = (double)converter.Convert(PosY, null, "Height", CultureInfo.CurrentCulture);
        StringBuilder result = new StringBuilder("{");
        result.Append($"\"pos\":[{posX},0.1,{posY}],");
        result.Append($"\"size\":{Width},");
        result.Append("\"font_size\":250,");
        result.Append($"\"width\":{Width},");
        result.Append($"\"value\":{Value},");
        result.Append($"\"label\":{Label},");
        result.Append($"\"alignment\":{(int)Alignment},");
        result.Append("},");
        return result.ToString();
    }
}
