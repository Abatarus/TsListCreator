using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
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
                LoadImageService loadImageService = new LoadImageService(desktop.MainWindow);
                desktop.MainWindow.DataContext = new MainViewModel(loadImageService);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
