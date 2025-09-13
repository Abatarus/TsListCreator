using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TsListCreator.Shared.Enums;

namespace TsListCreator.Model.Models
{
    public class TsTextBox: TsControl
    {
        public AlignmentId Alignment { get; set; }
        private int _rowCount;

        public int RowCount
        {
            get => _rowCount;
            set
            {
                _rowCount = value;
                if (_rowCount < 1)
                {
                    _rowCount = 1;
                }
            }
        }

        public string? Value { get; set; }
        public string? Label { get; set; }
        public double FontSize { get; set; }
        public override JsonObject GetJsonObject()
        {
            var result = new JsonObject
            {
                ["name"] = Name,
                ["font_size"] = FontSize,
                ["rows"] = RowCount,
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
            builder.Append($"\r\n pos = {{{PosX + Width * tsSizeToBound},0.11,{PosY + Height * tsSizeToBound}}},");
            builder.Append($"\r\n rows = {RowCount},");
            builder.Append($"\r\n width = {Width},");
            builder.Append($"\r\n font_size = {FontSize},");
            builder.Append($"\r\n label = \"{Label}\",");
            builder.Append($"\r\n value = \"{Value}\",");
            builder.Append($"\r\n alignment = {(int)Alignment}");
            builder.Append("\r\n},");
            return builder.ToString();
        }
    }
}
