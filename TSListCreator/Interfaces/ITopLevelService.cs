using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Avalonia.Xaml.Interactions.Core;

namespace TSListCreator.Interfaces
{
    public interface ITopLevelService
    {
        Task<string> LoadJsonFile();
        Task SaveJsonToFile(JsonObject json);
        Task<Bitmap?> GetImage();

        void SetClipboardText(string text);
    }
}
