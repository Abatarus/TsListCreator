using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;

namespace TSListCreator.Interfaces
{
    public interface ISettingsService: IJsonInput
    {
        double BoundWidth { get; set; }
        double BoundHeight { get; set; }
        Color Background { get; set; }
        Color FontColor { get; set; }
    }
}
