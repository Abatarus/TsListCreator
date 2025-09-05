using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.Json.Nodes;
using TSListCreator.Converters;
using TSListCreator.Interfaces;

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
    public TsTextBox(ISettingsService settingsService, IImageDataService imageDataService, IEditorStateService editorStateService)
        : base(settingsService, imageDataService, editorStateService)
    {
        Width = 1000;
        Height = FontSize + 100;
    }
    private AlignmentId _alignment = AlignmentId.Left;
    public AlignmentId Alignment
    {
        get => _alignment;
        set => SetField(ref _alignment, value);
    }
    private int _rowCount = 1;

    public int RowCount
    {
        get => _rowCount;
        set
        { 
            SetField(ref _rowCount, value);
            Height = FontSize * _rowCount + 24;
            OnPropertyChanged(nameof(Height));
        }
    }

    public override double Height
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
            OnPropertyChanged(nameof(CanvasHeight));
        }
    }


    private string _value = "";
    public string? Value
    {
        get => _value;
        set => SetField(ref _value, value);
    }

    private string _label = "";
    public string? Label
    {
        get => _label;
        set => SetField(ref _label, value);
    }

    private double _fontSize = 250;
    public double FontSize
    {
        get => _fontSize;
        set
        {
            SetField(ref _fontSize, value);
        }
    }
    public override JsonObject GetJsonObject()
    {
        var result = new JsonObject
        {
            ["name"] = Name,
            ["font_size"] = FontSize,
            ["rows"] = _rowCount,
            ["pos"] = new JsonArray(PosX, 0.1, PosY),
            ["width"] = (int)Width,
            ["value"] = Value,
            ["label"] = Label,
            ["alignment"] = (int)Alignment
        };
        return result;
    }

    public override string GetLuaString()
    {
        double tsSizeToBound = 3.141 / 15100 / 2;
        StringBuilder builder = new StringBuilder($"{{-- {Name}");
        builder.Append($"\r\n pos = {{{_posX + Width * tsSizeToBound},0.11,{_posY + Height * tsSizeToBound}}},");
        builder.Append($"\r\n rows = {_rowCount},");
        builder.Append($"\r\n width = {Width},");
        builder.Append($"\r\n font_size = {FontSize},");
        builder.Append($"\r\n label = \"{Label}\",");
        builder.Append($"\r\n value = \"{Value}\",");
        builder.Append($"\r\n alignment = {(int)Alignment}");
        builder.Append("\r\n},");
        return builder.ToString();
    }
}
