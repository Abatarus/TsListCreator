using System.Collections.ObjectModel;
using TsListCreator.Model.Models;
using TsListCreator.Model.Services;
using TsListCreator.Model.ViewModels.Controls;

namespace TsListCreator.Model.Utils;

public class DataHolder: DataModel
{
    private SettingsService _settings = new SettingsService();

    public SettingsService Settings
    {
        get => _settings;
        set => SetField(ref _settings, value);
    }
    private List<TextBoxViewModel> _textBoxes = new List<TextBoxViewModel>();
    public List<TextBoxViewModel> TextBoxes
    {
        get => _textBoxes;
        set => SetField(ref _textBoxes, value);
    }
    private List<CounterViewModel> _counters = new List<CounterViewModel>();
    public List<CounterViewModel> Counters
    {
        get => _counters;
        set => SetField(ref _counters, value);
    }
    private List<CheckboxViewModel> _checkBoxes = new List<CheckboxViewModel>();
    public List<CheckboxViewModel> CheckBoxes
    {
        get => _checkBoxes;
        set => SetField(ref _checkBoxes, value);
    }

}