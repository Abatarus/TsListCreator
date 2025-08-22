using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Interfaces;
using TSListCreator.Utils;

namespace TSListCreator.Services
{
    public class FilePickerService: IFilePickerService
    {
        private object _view;
        public FilePickerService(object view)
        {
            _view = view;
        }
        public async Task SaveToFile(MemoryStream stream)
        {
            stream.Position = 0;
            try
            {
                var topLevel = TopLevel.GetTopLevel((Visual)_view);
                var value = new FilePickerSaveOptions()
                {
                    Title = "Выберете файл сохранения",
                    FileTypeChoices = new[] { FilePickerFileTypes.All }
                };
                var file = await topLevel!.StorageProvider.SaveFilePickerAsync(value);

                if (file != null)
                {
                    await using Stream fileStream = await file.OpenWriteAsync();
                    await stream.CopyToAsync(fileStream);
                }
            }
            catch (Exception e)
            {
                // ReSharper disable once AsyncVoidThrowException
                throw new Exception("File loading failed");
            }
        }
        public async Task<Bitmap?> GetImage()
        {
            try
            {
                var topLevel = TopLevel.GetTopLevel((Visual)_view);
                var value = new FilePickerOpenOptions
                {
                    Title = "Выберете изображение",
                    AllowMultiple = false,
                    FileTypeFilter = new[] { FilePickerFileTypes.ImagePng, FilePickerFileTypes.ImageAll }
                };
                var files = await topLevel!.StorageProvider.OpenFilePickerAsync(value);

                if (files.Count >= 1)
                {
                    await using Stream stream = await files[0].OpenReadAsync();
                    return new Bitmap(stream);
                }
            }
            catch (Exception e)
            {
                throw new Exception("File loading failed");
            }
            return null;
        }
    }
}
