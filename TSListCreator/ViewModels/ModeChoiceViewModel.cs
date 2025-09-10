using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Enums;
using TSListCreator.Interfaces;
using TSListCreator.Utils;

namespace TSListCreator.ViewModels
{
    public class ModeChoiceViewModel(IEditorDataService editorDataService): DataModel
    {
        public Mode Mode
        {
            get => editorDataService.Mode;
            set
            {
                editorDataService.Mode = value;
                OnPropertyChanged();
            }
        }

        public bool Magnet
        {
            get => editorDataService.Magnet;
            set
            {
                editorDataService.Magnet = value;
                OnPropertyChanged();
            }
        }
    }
}
