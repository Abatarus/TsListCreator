using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;
using Avalonia.Media;
using TSListCreator.Interfaces;

namespace TSListCreator.Services
{
    public class SettingsService: ISettingsService
    {
        public JsonObject GetJsonObject()
        {
            var result = new JsonObject
            {
                ["bound_width"] = BoundWidth,
                ["bound_height"] = BoundHeight,
                ["background"] = Background.ToUInt32(),
                ["font_color"] = FontColor.ToUInt32()
            };
            return result;
        }
        public double BoundWidth { get; set; }
        public double BoundHeight { get; set; }
        public Color Background { get; set; } = Colors.Black;
        public Color FontColor { get; set; } = Colors.WhiteSmoke;
        public string GetLuaString()
        {
            double tsSizeToBound = 3.141 / 15100 / 2;
            StringBuilder builder = 
                new StringBuilder($"\r\n buttonColor = {{{Background.R / 255.0},{Background.G / 255.0},{Background.B / 255.0}}}");
            builder.Append($"\r\n buttonFontColor = {{{FontColor.R / 255.0},{FontColor.G / 255.0},{FontColor.B / 255.0}}}");
            return builder.ToString();
        }
    }
}
