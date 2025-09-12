using System.Drawing;
using TsListCreator.Model.Interfaces;
using TsListCreator.Model.Utils;

namespace TsListCreator.Model.ViewModels;

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

    public uint Background
    {
        get => _settingsService.Background.Value;
        set
        {
            _settingsService.Background.Value = value;
            OnPropertyChanged();
        }
    }

    public uint FontColor
    {
        get => _settingsService.FontColor.Value;
        set
        {
            _settingsService.FontColor.Value = value;
            OnPropertyChanged();
        }
    }
}