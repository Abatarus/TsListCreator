using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSListCreator.Services
{
    public class SingletonServiceContainer
    {
        private ImageDataService _imageDataService;

        public SingletonServiceContainer(ImageDataService imageDataService)
        {
            _imageDataService = imageDataService;
            _instance = this;
        }

        public ImageDataService ImageDataService => _imageDataService;

        private static SingletonServiceContainer? _instance;
        public static SingletonServiceContainer Instance
        {
            get => _instance;
        }
    }
}
