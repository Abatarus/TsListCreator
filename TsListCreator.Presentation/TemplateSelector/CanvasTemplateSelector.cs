using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using TsListCreator.Shared.Services;

namespace TsListCreator.Presentation.TemplateSelector;

class CanvasTemplateSelector: IDataTemplate
{
    [Content]
    public Dictionary<string, IDataTemplate> Templates { get; } = new Dictionary<string, IDataTemplate>();
    public Control? Build(object? param)
    {
        return Templates[param.ToString().Split(".")[2]].Build(param);
    }

    public bool Match(object? data)
    {
        return data is ICanvasDrawable;
    }
}