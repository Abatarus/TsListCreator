using System.Globalization;
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
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();

                IImageDataService imageDataService = new ImageDataService();
                ISettingsService settingsService = new SettingsService();
                IEditorStateService editorStateService = new EditorStateServiceService();
                TopLevelService topLevelService = new TopLevelService(desktop.MainWindow);
                SaveLoadService saveLoadService = new SaveLoadService(topLevelService);
                desktop.MainWindow.DataContext = new MainViewModel(
                    editorStateService, 
                    saveLoadService,
                    imageDataService,
                    settingsService);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
