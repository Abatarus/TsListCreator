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
    public class ImageLoadService: IImageLoadService
    {
        private object _view;
        public ImageLoadService(object view)
        {
            _view = view;
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
