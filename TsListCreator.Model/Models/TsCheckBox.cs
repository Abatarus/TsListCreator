using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace TsListCreator.Model.Models
{
    public class TsCheckBox: TsControl
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
        public bool State { get; set; }
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
            builder.Append($"\r\n pos = {{{PosX + bias},0.11,{PosY + bias}}},");
            builder.Append($"\r\n size = {(int)Size},");
            builder.Append($"\r\n state = {Convert.ToInt32(State)},");
            builder.Append("\r\n},");
            return builder.ToString();
        }
    }
}
