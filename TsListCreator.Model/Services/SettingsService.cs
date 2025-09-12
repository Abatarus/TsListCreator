using System.Drawing;
using System.Text;
using System.Text.Json.Nodes;
using TsListCreator.Model.Interfaces;
using TsListCreator.Shared.Utils;

namespace TsListCreator.Model.Services;

public class SettingsService: ISettingsService
{
    public JsonObject GetJsonObject()
    {
        var result = new JsonObject
        {
            ["bound_width"] = BoundWidth,
            ["bound_height"] = BoundHeight,
            ["background"] = Background.Value,
            ["font_color"] = FontColor.Value
        };
        return result;
    }
    public double BoundWidth { get; set; }
    public double BoundHeight { get; set; }
    public ITsColor Background { get; set; }
    public ITsColor FontColor { get; set; }
    public string GetLuaString()
    {
        double tsSizeToBound = 3.141 / 15100 / 2;
        StringBuilder builder = 
            new StringBuilder($"\r\n buttonColor = {{{Background.R},{Background.G},{Background.B}}}");
        builder.Append($"\r\n buttonFontColor = {{{FontColor.R},{FontColor.G},{FontColor.B}}}");
        return builder.ToString();
    }
}