using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace TSListCreator.Interfaces
{
    public interface IFilePickerService
    {
        Task<string> LoadJsonFile();
        Task SaveJsonToFile(JsonObject json);
        Task<Bitmap?> GetImage();
    }
}
