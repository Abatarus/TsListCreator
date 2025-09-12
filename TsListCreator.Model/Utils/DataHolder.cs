using System.Collections.ObjectModel;
using TsListCreator.Model.Controls;
using TsListCreator.Model.Services;

namespace TsListCreator.Model.Utils;

public class DataHolder: DataModel
{
    private SettingsService _settings = new SettingsService();

    public SettingsService Settings
    {
        get => _settings;
        set => SetField(ref _settings, value);
    }
    private ObservableCollection<TsTextBox> _textBoxes = new ObservableCollection<TsTextBox>(new List<TsTextBox>());
    public ObservableCollection<TsTextBox> TextBoxes
    {
        get => _textBoxes;
        set => SetField(ref _textBoxes, value);
    }
    private ObservableCollection<TsCounter> _counters = new ObservableCollection<TsCounter>(new List<TsCounter>());
    public ObservableCollection<TsCounter> Counters
    {
        get => _counters;
        set => SetField(ref _counters, value);
    }
    private ObservableCollection<TsCheckBox> _checkBoxes = new ObservableCollection<TsCheckBox>(new List<TsCheckBox>());
    public ObservableCollection<TsCheckBox> CheckBoxes
    {
        get => _checkBoxes;
        set => SetField(ref _checkBoxes, value);
    }

}