using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

public class MainViewModel(IImageLoadService imageLoadService, 
        IImageDataService imageDataService, 
        ISettingsService settingsService)
    : DataModel
{

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

    private ObservableCollection<TsTextBox> _textBoxes = new ObservableCollection<TsTextBox>(new List<TsTextBox>());
    public ObservableCollection<TsTextBox> TextBoxes
    {
        get => _textBoxes;
        set => SetField(ref _textBoxes, value);
    }

    private SettingsViewModel _settings;
    public SettingsViewModel Settings
    {
        get => _settings;
        set => SetField(ref _settings, value);
    }

    public async Task LoadImage()
    {
        try
        {
            Bitmap? bitmap = await imageLoadService.GetImage();
            if (bitmap != null)
            {
                TsImage = new TsImage(bitmap);
                imageDataService.LoadImage(TsImage);
            }
        }
        catch (Exception e)
        {
            //TODO Log
            throw new Exception(e.Message);
        }
    }

    public void AddNewTextBox()
    {
        TextBoxes.Add(new TsTextBox());
    }
}
