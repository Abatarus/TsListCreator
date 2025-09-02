using System;
using System.Globalization;
using System.Text;
using System.Text.Json.Nodes;

namespace TSListCreator.Controls;

public class TsCheckBox : TsControl
{
    public double Size
    {
        get => Width;
        set
        {
            Height = value;
            Width = value;
        }
    }

    private bool _state = false;
    public bool State
    {
        get => _state;
        set => SetField(ref _state, value);
    }
    public override JsonObject GetJsonObject()
    {
        var result = new JsonObject
        {
            ["name"] = Name,
            ["pos"] = new JsonArray(PosX, 0.1, PosY),
            ["size"] = (int)Size,
            ["state"] = State,
        };
        return result;
    }

    public override string GetLuaString()
    {
        double bias = Size * 3.141 / 15100 / 2; // перевод из size в bound пополам 
        StringBuilder builder = new StringBuilder($"{{-- {Name}");
        builder.Append($"\r\n pos = {{{_posX + bias},0.11,{_posY + bias}}},");
        builder.Append($"\r\n size = {(int)Size},");
        builder.Append($"\r\n state = {Convert.ToInt32(State)},");
        builder.Append("\r\n},");
        return builder.ToString();
    }
}
