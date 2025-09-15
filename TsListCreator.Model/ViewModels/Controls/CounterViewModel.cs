using System.Text;
using System.Text.Json.Nodes;
using TsListCreator.Model.Interfaces;
using TsListCreator.Model.Models;

namespace TsListCreator.Model.ViewModels.Controls;
public class CounterViewModel(TsCounter control, ISettingsService settingsService, IEditorDataService editorDataService)
    : ControlViewModel(control, settingsService, editorDataService)
{
    public double Size
    {
        get => control.Width;
        set
        {
            control.Height = value;
            control.Width = value;
        }
    }

    public int Value
    {
        get => control.Value;
        set
        {
            control.Value = value;
            OnPropertyChanged();
        }
    }
    public bool HideBg
    {
        get => control.HideBg;
        set
        {
            control.HideBg = value;
            OnPropertyChanged();
        }
    }
}
