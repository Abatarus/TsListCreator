using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSListCreator.Interfaces
{
    public interface IImageLoadService
    {
        Task<Bitmap?> GetImage();
    }
}
