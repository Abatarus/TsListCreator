using TSListCreator.Enums;
using TSListCreator.Utils;

namespace TSListCreator.Interfaces
{
    public interface IEditorDataService
    {
        Mode Mode { get; set; }
        bool Magnet { get; set; }
        TsImage Image { get; set; }
    }
}
