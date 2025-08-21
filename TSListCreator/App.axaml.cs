using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TSListCreator.Interfaces;
using TSListCreator.Services;
using TSListCreator.ViewModels;

namespace TSListCreator
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();

                IImageDataService imageDataService = new ImageDataService();
                ISettingsService settingsService = new SettingsService();
                ConverterServiceContainer serviceContainer = new ConverterServiceContainer(settingsService, imageDataService);
                ImageLoadService imageLoadService = new ImageLoadService(desktop.MainWindow);
                desktop.MainWindow.DataContext = new MainViewModel(imageLoadService, imageDataService, settingsService);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
