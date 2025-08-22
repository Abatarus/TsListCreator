using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    private IImageLoadService _imageLoadService;
    private IImageDataService _imageDataService;
    private ISettingsService _settingsService;

    public MainViewModel(IImageLoadService imageLoadService,
        IImageDataService imageDataService,
        ISettingsService settingsService)
    {
        _imageLoadService = imageLoadService;
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
            Bitmap? bitmap = await _imageLoadService.GetImage();
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
    }
}
