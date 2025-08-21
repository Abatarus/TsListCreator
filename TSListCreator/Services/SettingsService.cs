using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using TSListCreator.Interfaces;

namespace TSListCreator.Services
{
    public class SettingsService: ISettingsService
    {
        public string GetJsonString()
        {
            throw new NotImplementedException();
        }
        public double BoundWidth { get; set; }
        public double BoundHeight { get; set; }
        public Color Background { get; set; }
        public Color FontColor { get; set; }
    }
}
