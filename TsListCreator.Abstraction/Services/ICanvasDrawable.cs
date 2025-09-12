using TsListCreator.Shared.Enums;

namespace TsListCreator.Shared.Services;

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