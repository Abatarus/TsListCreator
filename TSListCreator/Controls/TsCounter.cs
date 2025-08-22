using System;
using System.Text;
using System.Text.Json.Nodes;
using TSListCreator.Controls;
using TSListCreator.Interfaces;
using TSListCreator.Utils;

namespace TSListCreator.Controls;
public class TsCounter : TsControl
{
    private double _size = 350;
    public double Size
    {
        get => _size;
        set => SetField(ref _size, value);
    }

    private int _value = 0;
    public int Value
    {
        get => _value;
        set => SetField(ref _value, value);
    }

    private bool _hideBg = false;
    public bool HideBg
    {
        get => _hideBg;
        set => SetField(ref _hideBg, value);
    }
    public override JsonObject GetJsonObject()
    {
        var result = new JsonObject
        {
            ["name"] = Name,
            ["pos"] = new JsonArray(PosX, 0.1, PosY),
            ["size"] = (int)Size,
            ["value"] = (int)Value,
            ["hideBG"] = HideBg,
        };
        return result;
    }

    public override string GetLuaString()
    {
        double bias = Size * 3.141 / 15100 / 2; // перевод из size в bound пополам 
        StringBuilder builder = new StringBuilder($"{{-- {Name}");
        builder.Append($"\r\n pos = {{{_posX + bias},0.11,{_posY + bias}}},");
        builder.Append($"\r\n size = {Size},");
        builder.Append($"\r\n value = {Value},");
        builder.Append($"\r\n hideBG = {Convert.ToInt32(HideBg)},");
        builder.Append("\r\n},");
        return builder.ToString();
    }
}
