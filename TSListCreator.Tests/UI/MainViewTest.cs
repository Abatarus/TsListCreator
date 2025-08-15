using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Headless.XUnit;
using Avalonia.Threading;
using Avalonia.VisualTree;
using TSListCreator.Services;
using TSListCreator.Tests.Mocks;
using TSListCreator.ViewModels;
using TSListCreator.Views;

namespace TSListCreator.Tests.UI
{
    public class MainViewTest
    {
        [AvaloniaFact]
        public void MainWindow_Open()
        {
            var record = Record.Exception(() =>
            {
                var window = new Window();
                var view = new MainView();
                window.Content = view;
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

        [AvaloniaFact]
        public void MainWindow_ImageNotLoaded_ShouldAllButtonsDisabled()
        {
            var window = new Window();
            var view = new MainView();
            window.Content = view;
            ImageDataService imageDataService = new ImageDataService();
            SingletonServiceContainer serviceContainer = new SingletonServiceContainer(imageDataService);
            LoadImageService loadImageService = new LoadImageService(window);
            var viewModel = new MainViewModel(loadImageService, serviceContainer.ImageDataService);
            window.DataContext = viewModel;

            window.Show();
            Dispatcher.UIThread.RunJobs();

            var loadDataButton = view.Get<Button>("LoadDataButton");
            var saveButton = view.Get<Button>("SaveButton");
            var copyClipboard = view.Get<Button>("CopyClipboard");

            Assert.False(loadDataButton.IsEnabled || saveButton.IsEnabled || copyClipboard.IsEnabled);
        }

        [AvaloniaFact]
        public void MainWindow_ImageLoaded_ShouldAllButtonsDisabled()
        {
            var window = new Window();
            var view = new MainView();
            window.Content = view;
            ImageDataService imageDataService = new ImageDataService();
            SingletonServiceContainer serviceContainer = new SingletonServiceContainer(imageDataService);
            LoadImageService loadImageService = new LoadImageServiceMock().FakedObject;

            var viewModel = new MainViewModel(loadImageService, serviceContainer.ImageDataService);
            window.DataContext = viewModel;

            window.Show();
            Dispatcher.UIThread.RunJobs();
            var loadImageButton = view.Get<Button>("LoadImageButton");
            loadImageButton!.Command!.Execute(null);

            var loadDataButton = view.Get<Button>("LoadDataButton");
            var saveButton = view.Get<Button>("SaveButton");
            var copyClipboard = view.Get<Button>("CopyClipboard");

            Assert.True(loadDataButton.IsEnabled && saveButton.IsEnabled && copyClipboard.IsEnabled);
        }
    }
}
