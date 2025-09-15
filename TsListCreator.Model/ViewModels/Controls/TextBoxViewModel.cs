using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.Json.Nodes;
using TsListCreator.Model.Interfaces;
using TsListCreator.Model.Models;
using TsListCreator.Shared.Enums;
using TsListCreator.Shared.ViewModels.Controls;

namespace TsListCreator.Model.ViewModels.Controls;

public class TextBoxViewModel(TsTextBox control, ISettingsService settingsService, IEditorDataService editorDataService)
    : ControlViewModel(control, settingsService, editorDataService), ITextBoxViewModel
{
    private AlignmentId _alignment = AlignmentId.Left;
    public AlignmentId Alignment
    {
        get => control.Alignment;
        set
        {
            control.Alignment = value;
            OnPropertyChanged();
        }
    }

    public int RowCount
    {
        get => control.RowCount;
        set
        {
            control.RowCount = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Height));
        }
    }

    public override double Height
    {
        get => FontSize * RowCount + 24; // из скрипта
        set
        {
            control.RowCount = (int)(value / FontSize);
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanvasHeight));
        }
    }

    public string? Value
    {
        get => control.Value;
        set
        {
            control.Value = value;
            OnPropertyChanged();
        }
    }

    public string? Label
    {
        get => control.Label;
        set
        {
            control.Label = value;
            OnPropertyChanged();
        }
    }

    public double FontSize
    {
        get => control.FontSize;
        set
        {
            control.FontSize = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanvasWidth));
            OnPropertyChanged(nameof(CanvasHeight));
            OnPropertyChanged(nameof(CanvasMinWidth));
            OnPropertyChanged(nameof(CanvasMinHeight));
        }
    }

    public override double CanvasMinHeight
    {
        get => _sizeConverter.Convert(FontSize, "Height");
        set => FontSize = _sizeConverter.ConvertBack(value, "Height");
    }
    public override double CanvasMinWidth
    {
        get => _sizeConverter.Convert(FontSize,  "Width");
        set => FontSize = _sizeConverter.ConvertBack(value, "Width");
    }
}
