using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace TsListCreator.Model.Models
{
    public class TsCounter: TsControl
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

        public int Value { get; set; }
        public bool HideBg { get; set; }
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
            builder.Append($"\r\n pos = {{{PosX + bias},0.11,{PosY + bias}}},");
            builder.Append($"\r\n size = {Size},");
            builder.Append($"\r\n value = {Value},");
            builder.Append($"\r\n hideBG = {Convert.ToInt32(HideBg)},");
            builder.Append("\r\n},");
            return builder.ToString();
        }
    }
}
