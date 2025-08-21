using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using TSListCreator.Interfaces;
using TSListCreator.Utils;

namespace TSListCreator.ViewModels
{
    public class SettingsViewModel(ISettingsService _settingsService) : DataModel
    {

        public double BoundWidth
        {
            get => _settingsService.BoundWidth;
            set
            {
                _settingsService.BoundWidth = value;
                OnPropertyChanged();
            }
        }

        public double BoundHeight
        {
            get => _settingsService.BoundHeight;
            set
            {
                _settingsService.BoundHeight = value;
                OnPropertyChanged();
            }
        }

        public Color Background
        {
            get => _settingsService.Background;
            set
            {
                _settingsService.Background = value;
                OnPropertyChanged();
            }
        }

        public Color FontColor
        {
            get => _settingsService.Background;
            set
            {
                _settingsService.FontColor = value;
                OnPropertyChanged();
            }
        }
    }
}
