using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Utils;

namespace TSListCreator.Interfaces
{
    public interface IImageDataService
    {
        void LoadImage(TsImage image);
        public double GetImageHeight();
        public double GetImageWidth();

        public int GetImagePixHeight();
        public int GetImagePixWidth();
    }
}
