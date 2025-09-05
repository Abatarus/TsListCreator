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
    public class ModeChoiceViewModel(IEditorStateService editorStateService): DataModel, IEditorStateService
    {
        public Mode Mode
        {
            get => editorStateService.Mode;
            set
            {
                editorStateService.Mode = value;
                OnPropertyChanged();
            }
        }

        public bool Magnet
        {
            get => editorStateService.Magnet;
            set
            {
                editorStateService.Magnet = value;
                OnPropertyChanged();
            }
        }
    }
}
