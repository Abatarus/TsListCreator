using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using SkiaSharp;
using TSListCreator.Controls;
using TSListCreator.Interfaces;
using TSListCreator.Services;
using TSListCreator.Utils;

namespace TSListCreator.ViewModels;

public class MainViewModel
    : DataModel
{
    private ITopLevelService _topLevelService;
    private IImageDataService _imageDataService;
    private ISettingsService _settingsService;
    private ISaveLoadService _saveLoadService;

    public MainViewModel(ITopLevelService topLevelService,
        ISaveLoadService saveLoadService,
        IImageDataService imageDataService,
        ISettingsService settingsService)
    {
        _saveLoadService = saveLoadService;
        _topLevelService = topLevelService;
        _imageDataService = imageDataService;
        _settingsService = settingsService;
        Settings = new SettingsViewModel(_settingsService);

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


    private ObservableCollection<TsControl> _sharedCollection = new ObservableCollection<TsControl>(new List<TsControl>());
    public ObservableCollection<TsControl> SharedCollection
    {
        get => _sharedCollection;
        set => SetField(ref _sharedCollection, value);
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

    private SettingsViewModel? _settings;
    public SettingsViewModel Settings
    {
        get => _settings;
        set => SetField(ref _settings, value);
    }

    public async Task LoadImage()
    {
        try
        {
            Bitmap? bitmap = await _topLevelService.GetImage();
            if (bitmap != null)
            {
                TsImage = new TsImage(bitmap);
                _imageDataService.LoadImage(TsImage);
            }
        }
        catch (Exception e)
        {
            //TODO Log
            throw new Exception(e.Message);
        }
    }

    public void UpdateControls()
    {
        //TODO костыль
        Dispatcher.UIThread.Invoke(() =>
        {
            var shared = SharedCollection;
            SharedCollection = null;
            SharedCollection = shared;
        });

    }
    public void AddNewTextBox()
    {
        TextBoxes.Add(new TsTextBox() { Name = $"TextBox{TextBoxes.Count}" });
        SharedCollection.Add(TextBoxes.Last());
        TextBoxes.Last().SetRemove(RemoveMe);
    }
    public void AddNewCheckBox()
    {
        CheckBoxes.Add(new TsCheckBox() { Name = $"TsCheckbox{CheckBoxes.Count}" });
        SharedCollection.Add(CheckBoxes.Last());
        CheckBoxes.Last().SetRemove(RemoveMe);
    }
    public void AddNewCounter()
    {
        Counters.Add(new TsCounter() { Name = $"Counter{Counters.Count}" });
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
            holder = await _saveLoadService.Load(_settingsService);
        }
        catch (Exception ex)
        {
            //TODO log
            return;
        }
        Settings.BoundHeight = holder.Settings.BoundHeight;
        Settings.BoundWidth = holder.Settings.BoundWidth;
        Settings.Background = holder.Settings.Background;
        Settings.FontColor = holder.Settings.FontColor;
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
        //TODO
        if (child is TsTextBox tb) TextBoxes.Remove(tb);
        else if (child is TsCounter c) Counters.Remove(c);
        else if (child is TsCheckBox cb) CheckBoxes.Remove(cb);
    }
}
