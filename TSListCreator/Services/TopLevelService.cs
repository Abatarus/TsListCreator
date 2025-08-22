using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TSListCreator.Interfaces;
using TSListCreator.Utils;

namespace TSListCreator.Services
{
    public class TopLevelService : ITopLevelService
    {
        private object _view;
        public TopLevelService(object view)
        {
            _view = view;
        }

        public async Task<string?> LoadJsonFile()
        {
            try
            {
                var topLevel = TopLevel.GetTopLevel((Visual)_view);
                var value = new FilePickerOpenOptions
                {
                    Title = "Выберете файл загрузки",
                    AllowMultiple = false,
                    FileTypeFilter = new[] {
                        new FilePickerFileType("json")
                        {
                            Patterns = new[] { "*.json" },
                            AppleUniformTypeIdentifiers = new[] { "public.json" },
                            MimeTypes = new[] { "application/json" }
                        }
                    }
                };
                var files = await topLevel!.StorageProvider.OpenFilePickerAsync(value);

                if (files.Count >= 1)
                {
                    await using Stream stream = await files[0].OpenReadAsync();
                    using var reader = new StreamReader(stream);
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception("File loading failed");
            }
            return null;
        }

        public async Task SaveJsonToFile(JsonObject json)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = false,
            };
            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json.ToJsonString(options)));
            stream.Position = 0;
            try
            {
                var topLevel = TopLevel.GetTopLevel((Visual)_view);
                var value = new FilePickerSaveOptions()
                {
                    Title = "Выберете файл сохранения",
                    SuggestedFileName = "save.json",
                    FileTypeChoices = new[] { 
                        new FilePickerFileType("json")
                        {
                            Patterns = new[] { "*.json" },
                            AppleUniformTypeIdentifiers = new[] { "public.json" },
                            MimeTypes = new[] { "application/json" }
                        }
                    }
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

        public void SetClipboardText(string text)
        {
            var topLevel = TopLevel.GetTopLevel((Visual)_view);
            topLevel!.Clipboard!.SetTextAsync(text);
        }
    }
}
