using TsListCreator.Model.Interfaces;
using TsListCreator.Model.Utils;
using TsListCreator.Shared.Enums;

namespace TsListCreator.Model.Services;

public class EditorDataService : IEditorDataService
{
    public Mode Mode { get; set; } = Mode.Move;
    public bool Magnet { get; set; } = false;
    public TsImage? Image
    {
        get => _image;
        set => _image = value;
    }

    private TsImage? _image = null;
}