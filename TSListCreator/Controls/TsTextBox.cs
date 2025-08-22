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

    private double _width = 1000;
    public double Width
    {
        get => _width;
        set => SetField(ref _width, value);
    }

    private int _rowCount = 1;
    public double Height
    {
        get => FontSize * _rowCount + 24; // из скрипта
        set
        {
            _rowCount = (int)(value / FontSize);
            if (_rowCount < 1)
            {
                _rowCount = 1;
            }
            OnPropertyChanged();
        }
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

    private double _fontSize = 250;
    public double FontSize
    {
        get => _fontSize;
        set => SetField(ref _fontSize, value);
    }

    public override string GetJsonString()
    {
        var converter = new CanvasCoorToTsPosConverter();
        double posX = (double)converter.ConvertBack(PosX, null, "Width", CultureInfo.CurrentCulture);
        double posY = (double)converter.ConvertBack(PosY, null, "Height", CultureInfo.CurrentCulture);
        StringBuilder result = new StringBuilder("{");
        result.Append($"\"name\":{Name},");
        result.Append($"\"pos\":[{posX},0.1,{posY}],");
        result.Append($"\"font_size\":{FontSize},");
        result.Append($"\"width\":{(int)Width},");
        result.Append($"\"value\":{Value},");
        result.Append($"\"label\":{Label},");
        result.Append($"\"alignment\":{(int)Alignment},");
        result.Append("},");
        return result.ToString();
    }
}
