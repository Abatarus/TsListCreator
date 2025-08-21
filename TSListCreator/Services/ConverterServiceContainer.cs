using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Interfaces;

namespace TSListCreator.Services
{
    public class ConverterServiceContainer
    {
        private IImageDataService _imageDataService;
        public IImageDataService ImageDataService => _imageDataService;

        private ISettingsService _settingsService;

        public ISettingsService SettingsService => _settingsService;

        public ConverterServiceContainer(ISettingsService settingsService, IImageDataService imageDataService)
        {
            _settingsService = settingsService;
            _imageDataService = imageDataService;
            _instance = this;
        }


        private static ConverterServiceContainer? _instance;
        public static ConverterServiceContainer Instance
        {
            get => _instance;
        }
    }
}
