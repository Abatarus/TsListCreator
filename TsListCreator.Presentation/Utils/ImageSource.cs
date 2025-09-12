using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using TsListCreator.Shared.Utils;

namespace TsListCreator.Presentation.Utils;

public class ImageSource: IImageSource
{
    private Bitmap _bitmap; 
    public ImageSource(Bitmap bitmap)
    {
        _bitmap = bitmap;
    }

    public object Source => _bitmap;
}