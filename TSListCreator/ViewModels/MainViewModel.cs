using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using TSListCreator.Controls;
using TSListCreator.Utils;

namespace TSListCreator.ViewModels;

public class MainViewModel: DataModel
{
    private Bitmap? _image = null;
    public Bitmap? Image
    {
        get => _image;
        set
        {
            SetField(ref _image, value);
            OnPropertyChanged(nameof(CanInteract));
        }
    }

    private ObservableCollection<TsTextBox> _textBoxes = new ObservableCollection<TsTextBox>(new List<TsTextBox>());
    public ObservableCollection<TsTextBox> TextBoxes
    {
        get => _textBoxes;
        set => SetField(ref _textBoxes, value);
    }
    public bool CanInteract => Image != null;

    private object _view;
    public MainViewModel(object view)
    {
        _view = view;
    }

    public async void LoadImage()
    {
        var topLevel = TopLevel.GetTopLevel((Visual)_view);
        var value = new FilePickerOpenOptions {
            Title = "Выберете изображение",
            AllowMultiple = false,
            FileTypeFilter = new[] { FilePickerFileTypes.ImagePng, FilePickerFileTypes.ImageAll }
        };
        var files = await topLevel!.StorageProvider.OpenFilePickerAsync(value);

        if (files.Count >= 1)
        {
            await using Stream stream = await files[0].OpenReadAsync();
            Image = new Bitmap(stream);
        }
    }

    public void AddNewTextBox()
    {
        TextBoxes.Add(new TsTextBox());
    }
}
