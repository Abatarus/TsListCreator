using TsListCreator.Model.Utils;

namespace TsListCreator.Model.Interfaces;

public interface ISaveLoadService
{
    Task Save(IJsonInput settings, IEnumerable<IJsonInput> textBoxes, IEnumerable<IJsonInput> counters, IEnumerable<IJsonInput> checkBoxes);

    Task<DataHolder> Load(ISettingsService settingsService, IEditorDataService editorDataService);
    Task<TsImage> LoadImage();

    void SaveToClipBoard(ILuaInput settings,
        IEnumerable<ILuaInput> textBoxes,
        IEnumerable<ILuaInput> counters,
        IEnumerable<ILuaInput> checkBoxes);
}