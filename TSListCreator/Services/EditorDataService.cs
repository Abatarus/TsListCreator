using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Enums;
using TSListCreator.Interfaces;
using TSListCreator.Utils;

namespace TSListCreator.Services
{
    public class EditorDataService : IEditorDataService
    {
        public Mode Mode { get; set; } = Mode.Move;
        public bool Magnet { get; set; } = false;
        public TsImage? Image
        {
            get => _image;
            set => _image = value;
        }

        private TsImage? _image = null;
    }
}
