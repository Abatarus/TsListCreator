using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Interfaces;

namespace TSListCreator.Services
{
    public class EditorStateServiceService: IEditorStateService
    {
        public Mode Mode { get; set; }
        public bool Magnet { get; set; }
    }
}
