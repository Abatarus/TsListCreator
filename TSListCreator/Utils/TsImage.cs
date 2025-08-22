using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;

namespace TSListCreator.Utils
{
    public class TsImage: DataModel
    {
        public TsImage(Bitmap source)
        {
            Source = source;
        }
        private Bitmap _source;

        public Bitmap Source
        {
            get => _source;
            set => SetField(ref _source, value);
        }

        private Rect _bounds;

        public Rect Bounds
        {
            get => _bounds;
            set
            {
                SetField(ref _bounds, value);
            }
        }

        public double Width => _bounds.Width;

        public double Height => _bounds.Height;
    }
}
