using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSListCreator.Interfaces
{
    public interface ICanvasDrawable
    {
        double CanvasPosX { get; set; }
        double CanvasPosY { get; set; }
        double CanvasHeight { get; set; }
        double CanvasWidth { get; set; }
        bool IsHighlighted { get; set; }
    }
}
