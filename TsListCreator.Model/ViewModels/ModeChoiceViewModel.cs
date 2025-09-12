using TsListCreator.Model.Interfaces;
using TsListCreator.Model.Utils;
using TsListCreator.Shared.Enums;

namespace TsListCreator.Model.ViewModels;

public class ModeChoiceViewModel(IEditorDataService editorDataService): DataModel
{
    public Mode Mode
    {
        get => editorDataService.Mode;
        set
        {
            editorDataService.Mode = value;
            OnPropertyChanged();
        }
    }

    public bool Magnet
    {
        get => editorDataService.Magnet;
        set
        {
            editorDataService.Magnet = value;
            OnPropertyChanged();
        }
    }
}