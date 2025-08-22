using System;
using System.Text;
using System.Text.Json.Nodes;

namespace TSListCreator.Controls;

public class TsCheckBox : TsControl
{
    private double _size = 150;
    public double Size
    {
        get => _size;
        set => SetField(ref _size, value);
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
        builder.Append($"\r\n size = {Size},");
        builder.Append($"\r\n state = {Convert.ToInt32(State)},");
        return builder.ToString();
    }
}
