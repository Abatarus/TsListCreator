using System.Text;
using System.Text.Json.Nodes;
using TsListCreator.Model.Interfaces;
using TsListCreator.Model.Models;

namespace TsListCreator.Model.ViewModels.Controls;
public class CounterViewModel(TsCounter control, ISettingsService settingsService, IEditorDataService editorDataService)
    : ControlViewModel(control, settingsService, editorDataService)
{
    public double Size
    {
        get => control.Width;
        set
        {
            control.Height = value;
            control.Width = value;
        }
    }

    public int Value
    {
        get => control.Value;
        set
        {
            control.Value = value;
            OnPropertyChanged();
        }
    }
    public bool HideBg
    {
        get => control.HideBg;
        set
        {
            control.HideBg = value;
            OnPropertyChanged();
        }
    }
    public override JsonObject GetJsonObject()
    {
        var result = new JsonObject
        {
            ["name"] = Name,
            ["pos"] = new JsonArray(PosX, 0.1, PosY),
            ["size"] = (int)Size,
            ["value"] = Value,
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
