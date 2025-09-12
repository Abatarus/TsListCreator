using System.Text.Json.Nodes;
using TsListCreator.Shared.Utils;

namespace TsListCreator.Shared.Services;

public interface ITopLevelService
{
    Task<string> LoadJsonFile();
    Task SaveJsonToFile(JsonObject json);
    Task<IImageSource> GetImage();
    void SetClipboardText(string text);
}