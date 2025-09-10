using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Enums;

namespace TSListCreator.Interfaces
{
    public interface ICanvasDrawable
    {
        Mode Mode { get; set; }
        bool Magnet { get; set; }
        double CanvasPosX { get; set; }
        double CanvasPosY { get; set; }
        double CanvasHeight { get; set; }
        double CanvasWidth { get; set; }
        double CanvasMinHeight { get; set; }
        double CanvasMinWidth { get; set; }
        bool IsHighlighted { get; set; }
    }
}
