using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSListCreator.Interfaces
{
    public interface IFilePickerService
    {
        Task SaveToFile(MemoryStream stream);
        Task<Bitmap?> GetImage();
    }
}
