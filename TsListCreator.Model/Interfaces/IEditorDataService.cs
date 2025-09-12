using TsListCreator.Model.Utils;
using TsListCreator.Shared.Enums;

namespace TsListCreator.Model.Interfaces;

public interface IEditorDataService
{
    Mode Mode { get; set; }
    bool Magnet { get; set; }
    TsImage Image { get; set; }
}