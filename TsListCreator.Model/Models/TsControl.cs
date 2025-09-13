using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using TsListCreator.Model.Interfaces;

namespace TsListCreator.Model.Models
{
    public abstract class TsControl: IJsonInput, ILuaInput
    {
        public string Name { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public abstract JsonObject GetJsonObject();
        public abstract string GetLuaString();
    }
}
