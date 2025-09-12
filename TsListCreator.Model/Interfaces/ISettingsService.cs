using TsListCreator.Shared.Utils;

namespace TsListCreator.Model.Interfaces;

public interface ISettingsService: IJsonInput, ILuaInput
{
    double BoundWidth { get; set; }
    double BoundHeight { get; set; }
    ITsColor Background { get; set; }
    ITsColor FontColor { get; set; }
}