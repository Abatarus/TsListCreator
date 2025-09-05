using TSListCreator.Enums;

namespace TSListCreator.Interfaces
{
    public interface IEditorStateService
    {
        Mode Mode { get; set; }
        bool Magnet { get; set; }
    }
}
