using System.Drawing;
using System.Text;
using System.Text.Json.Nodes;
using TsListCreator.Model.Interfaces;
using TsListCreator.Model.Models;

namespace TsListCreator.Model.ViewModels.Controls;

public class CheckboxViewModel(TsCheckBox control, ISettingsService settingsService, IEditorDataService editorDataService) 
    : ControlViewModel(control, settingsService, editorDataService)
{
    public double Size
    {
        get => Width;
        set
        {
            Height = value;
            Width = value;
        }
    }

    private bool _state = false;
    public bool State
    {
        get => _state;
        set => SetField(ref _state, value);
    }
}
