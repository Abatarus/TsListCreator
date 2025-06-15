using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace TSListCreator.Utils
{
    public class TsImage: DataModel
    {
        public TsImage(Bitmap source)
        {
            Source = source;
            Width = Source.Size.Width;
        }
        private Bitmap _source;

        public Bitmap Source
        {
            get => _source;
            set => SetField(ref _source, value);
        }

        //private double _height;

        //public double Height
        //{
        //    get => _height;
        //    set => SetField(ref _height, value);
        //}
        private double _width;

        public double Width
        {
            get => _width;
            set
            {
                SetField(ref _width, value);
                OnPropertyChanged(nameof(EmToPix));
            } 
        }

        public double EmToPix => Width / Source.PixelSize.Width;
    }
}
