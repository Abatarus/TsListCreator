using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using TSListCreator.Controls;

namespace TSListCreator.Utils
{
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
            return data is TsControl;
        }
    }
}
