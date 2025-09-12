using System.Collections.ObjectModel;
using TsListCreator.Model.Controls;
using TsListCreator.Model.Interfaces;
using TsListCreator.Model.Utils;
using TsListCreator.Shared.Services;

namespace TsListCreator.Model.ViewModels;

public class MainViewModel
    : DataModel
{
    private readonly IEditorDataService _editorDataService;
    private readonly ISettingsService _settingsService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IUIDispatcher _dispatcher;

    public MainViewModel(
        IUIDispatcher dispatcher,
        IEditorDataService editorDataService,
        ISaveLoadService saveLoadService,
        ISettingsService settingsService)
    {
        _saveLoadService = saveLoadService;
        _settingsService = settingsService;
        _editorDataService = editorDataService;

        Settings = new SettingsViewModel(_settingsService);
        ModeChoice = new ModeChoiceViewModel(_editorDataService);

        ModeChoice.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(ModeChoice.Mode) ||
                e.PropertyName == nameof(ModeChoice.Magnet))
            {
                UpdateControls();
            }
        };

        Settings.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(Settings.BoundHeight) ||
                e.PropertyName == nameof(Settings.BoundWidth))
            {
                OnPropertyChanged(nameof(CanAdd));
                UpdateControls();
            }
        };
    }

    private TsImage? _image = null;
    public TsImage? TsImage
    {
        get => _image;
        set
        {
            SetField(ref _image, value);
            OnPropertyChanged(nameof(CanInteract));
            OnPropertyChanged(nameof(CanAdd));
        }
    }
    public bool CanInteract => TsImage != null;
    public bool CanAdd => CanInteract && Settings.BoundHeight > 0 && Settings.BoundWidth > 0;


    private ObservableCollection<TsControl> _sharedCollection = new(new List<TsControl>());
    public ObservableCollection<TsControl> SharedCollection
    {
        get => _sharedCollection;
        set => SetField(ref _sharedCollection, value);
    }

    private ObservableCollection<TsTextBox> _textBoxes = new(new List<TsTextBox>());
    public ObservableCollection<TsTextBox> TextBoxes
    {
        get => _textBoxes;
        set => SetField(ref _textBoxes, value);
    }
    private ObservableCollection<TsCounter> _counters = new(new List<TsCounter>());
    public ObservableCollection<TsCounter> Counters
    {
        get => _counters;
        set => SetField(ref _counters, value);
    }

    private ObservableCollection<TsCheckBox> _checkBoxes = new(new List<TsCheckBox>());
    public ObservableCollection<TsCheckBox> CheckBoxes
    {
        get => _checkBoxes;
        set => SetField(ref _checkBoxes, value);
    }

    private SettingsViewModel? _settings;
    public SettingsViewModel Settings
    {
        get => _settings;
        set => SetField(ref _settings, value);
    }

    private ModeChoiceViewModel? _modeChoice;
    public ModeChoiceViewModel ModeChoice
    {
        get => _modeChoice;
        set => SetField(ref _modeChoice, value);
    }

    public async void LoadImage()
    {
        try
        {
            TsImage = await _saveLoadService.LoadImage();
            _editorDataService.Image = TsImage;
        }
        catch (Exception e)
        {
            throw; // TODO handle exception
        }
    }

    public void UpdateControls()
    {
        _dispatcher.Post(() =>
        {
            foreach (var control in SharedCollection)
            {
                control.Redraw();
            }
        });
    }
    public void AddNewTextBox()
    {
        TextBoxes.Add(new TsTextBox(_settingsService, _editorDataService) { Name = $"TextBox{TextBoxes.Count}" });
        SharedCollection.Add(TextBoxes.Last());
        TextBoxes.Last().SetRemove(RemoveMe);
    }
    public void AddNewCheckBox()
    {
        CheckBoxes.Add(new TsCheckBox(_settingsService, _editorDataService) { Name = $"TsCheckbox{CheckBoxes.Count}" });
        SharedCollection.Add(CheckBoxes.Last());
        CheckBoxes.Last().SetRemove(RemoveMe);
    }
    public void AddNewCounter()
    {
        Counters.Add(new TsCounter(_settingsService, _editorDataService) { Name = $"Counter{Counters.Count}" });
        SharedCollection.Add(Counters.Last());
        Counters.Last().SetRemove(RemoveMe);
    }

    public async void Save()
    {
        await _saveLoadService.Save(_settingsService, TextBoxes, Counters, CheckBoxes);
    }
    public async void Load()
    {
        DataHolder holder;
        try
        {
            holder = await _saveLoadService.Load(_settingsService, _editorDataService);
        }
        catch (Exception ex)
        {
            //TODO log
            return;
        }
        Settings.BoundHeight = holder.Settings.BoundHeight;
        Settings.BoundWidth = holder.Settings.BoundWidth;
        Settings.Background = holder.Settings.Background.Value;
        Settings.FontColor = holder.Settings.FontColor.Value;
        SharedCollection.Clear();
        TextBoxes.Clear();
        TextBoxes = holder.TextBoxes;
        foreach (var textBox in TextBoxes)
        {
            SharedCollection.Add(textBox);
            textBox.SetRemove(RemoveMe);
        }
        CheckBoxes.Clear();
        CheckBoxes = holder.CheckBoxes;
        foreach (var checkBox in CheckBoxes)
        {
            SharedCollection.Add(checkBox);
            checkBox.SetRemove(RemoveMe);
        }
        Counters.Clear();
        Counters = holder.Counters;
        foreach (var counter in Counters)
        {
            SharedCollection.Add(counter);
            counter.SetRemove(RemoveMe);
        }
    }
    public void CopyToClipBoard()
    {
        _saveLoadService.SaveToClipBoard(_settingsService, TextBoxes, Counters, CheckBoxes);
    }
    private void RemoveMe(object child)
    {
        SharedCollection.Remove((TsControl)child);
        if (child is TsTextBox tb) TextBoxes.Remove(tb);
        else if (child is TsCounter c) Counters.Remove(c);
        else if (child is TsCheckBox cb) CheckBoxes.Remove(cb);
    }
}