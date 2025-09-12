using System.Globalization;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TsListCreator.Model.Services;
using TsListCreator.Model.ViewModels;

namespace TSListCreator;

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

            ISettingsService settingsService = new SettingsService();
            IEditorDataService editorDataService = new EditorDataService();
            TopLevelService topLevelService = new TopLevelService(desktop.MainWindow);
            SaveLoadService saveLoadService = new SaveLoadService(topLevelService);
            desktop.MainWindow.DataContext = new MainViewModel(
                editorDataService, 
                saveLoadService,
                settingsService);
        }

        base.OnFrameworkInitializationCompleted();
    }
}