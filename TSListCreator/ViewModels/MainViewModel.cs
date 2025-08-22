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

    private ObservableCollection<TsTextBox> _textBoxes = new ObservableCollection<TsTextBox>(new List<TsTextBox>());
    public ObservableCollection<TsTextBox> TextBoxes
    {
        get => _textBoxes;
        set => SetField(ref _textBoxes, value);
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
        var textBoxes = TextBoxes;
        TextBoxes = null;
        TextBoxes = textBoxes;
    }
    public void AddNewTextBox()
    {
        TextBoxes.Add(new TsTextBox(){Name = $"TextBox{TextBoxes.Count}"});
        TextBoxes.Last().SetRemove(RemoveMe);
    }

    public async void Save()
    {
        await _saveLoadService.Save(_settingsService, _textBoxes, new List<IJsonInput>(), new List<IJsonInput>());
    }
    public async void Load()
    {
        DataHolder holder = await _saveLoadService.Load();
        Settings.BoundHeight = holder.Settings.BoundHeight;
        Settings.BoundWidth = holder.Settings.BoundWidth;
        Settings.Background = holder.Settings.Background;
        Settings.FontColor = holder.Settings.FontColor;
        TextBoxes.Clear();
        TextBoxes = holder.TextBoxes;
        foreach (var textBox in TextBoxes)
        {
            textBox.SetRemove(RemoveMe);
        }
    }
    public void CopyToClipBoard()
    {
        _saveLoadService.SaveToClipBoard(_settingsService, _textBoxes, new List<ILuaInput>(), new List<ILuaInput>());
    }
    private void RemoveMe(object child)
    {
        //TODO
        if (child is TsTextBox tb) TextBoxes.Remove(tb);
        //else if (child is TsCounter c) Counters.Remove(c);
        //else if (child is TsCounter cb) CheckBoxes.Remove(cb);
    }
}
