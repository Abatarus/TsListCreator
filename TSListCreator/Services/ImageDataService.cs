using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Interfaces;
using TSListCreator.Utils;

namespace TSListCreator.Services
{
    public class ImageDataService: IImageDataService
    {
        private TsImage? _image = null;

        public void LoadImage(TsImage image)
        {
            _image = image;
        }

        public double GetImageHeight()
        {
            if (_image == null)
            {
                throw new Exception("Image not loaded");
            }
            return _image.Source.Size.Height;
        }

        public double GetImageWidth()
        {
            if (_image == null)
            {
                throw new Exception("Image not loaded");
            }
            return _image.Source.Size.Width;
        }

        public int GetImagePixHeight()
        {
            if (_image == null)
            {
                throw new Exception("Image not loaded");
            }
            return _image.Source.PixelSize.Height;
        }

        public int GetImagePixWidth()
        {
            if (_image == null)
            {
                throw new Exception("Image not loaded");
            }
            return _image.Source.PixelSize.Height;
        }
    }
}
