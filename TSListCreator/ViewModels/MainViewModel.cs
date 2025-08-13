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
using TSListCreator.Services;
using TSListCreator.Utils;

namespace TSListCreator.ViewModels;

public class MainViewModel : DataModel
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

    private LoadImageService _loadImageService;
    public MainViewModel(LoadImageService loadImageService)
    {
        _loadImageService = loadImageService;
    }

    public async Task LoadImage()
    {
        try
        {
            Bitmap? bitmap = await _loadImageService.GetImage();
            if (bitmap != null)
            {
                TsImage = new TsImage(bitmap);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public void AddNewTextBox()
    {
        TextBoxes.Add(new TsTextBox());
    }
}
