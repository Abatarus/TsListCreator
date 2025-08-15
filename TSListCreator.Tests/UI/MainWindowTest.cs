using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Headless.XUnit;
using Avalonia.Threading;
using TSListCreator.Services;
using TSListCreator.ViewModels;
using TSListCreator.Views;

namespace TSListCreator.Tests.UI
{
    public class MainWindowTest
    {
        [AvaloniaFact]
        public void MainWindow_Open()
        {
            var record = Record.Exception(() =>
            {
                var window = new MainWindow();
                var view = new MainView();
                ImageDataService imageDataService = new ImageDataService();
                SingletonServiceContainer serviceContainer = new SingletonServiceContainer(imageDataService);
                LoadImageService loadImageService = new LoadImageService(window);
                var viewModel = new MainViewModel(loadImageService, serviceContainer.ImageDataService);
                view.DataContext = viewModel;

                window.Show();
                Dispatcher.UIThread.RunJobs();
            });
            Assert.Null(record);
        }
    }
}
