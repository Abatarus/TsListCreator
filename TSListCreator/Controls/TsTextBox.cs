using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.Json.Nodes;
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

    public override JsonObject GetJsonObject()
    {
        var result = new JsonObject
        {
            ["name"] = Name, // строка сама обернётся в кавычки
            ["pos"] = new JsonArray(PosX, 0.1, PosY),
            ["font_size"] = FontSize,
            ["width"] = (int)Width,
            ["value"] = Value,
            ["label"] = Label,
            ["alignment"] = (int)Alignment
        };
        return result;
    }
}
