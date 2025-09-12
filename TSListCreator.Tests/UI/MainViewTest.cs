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
using FakeItEasy;
using TsListCreator.Model.Interfaces;
using TsListCreator.Model.ViewModels;
using TsListCreator.Presentation.Views;
using TsListCreator.Shared.Services;
using TSListCreator.Tests.Mocks;
using TSListCreator.Views;

namespace TSListCreator.Tests.UI;

public class MainViewTest
{
    /*[AvaloniaFact]
    public void MainWindow_Open()
    {
        var record = Record.Exception(() =>
        {
            var window = new Window();
            var view = new MainView();
            window.Content = view;
            var viewModel = new MainViewModel(A.Fake<IEditorDataService>(),
                new SaveLoadServiceMock().FakedObject,
                new ImageDataServiceMock().FakedObject,
                new SettingsServiceMock().FakedObject);
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
        var viewModel = new MainViewModel(A.Fake<IEditorDataService>(),
            new SaveLoadServiceMock().FakedObject,
            new ImageDataServiceMock().FakedObject,
            new SettingsServiceMock().FakedObject);
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
        ITopLevelService topLevelService = new FilePickerServiceMock().FakedObject;

        var viewModel = new MainViewModel(A.Fake<IEditorDataService>(),
            new SaveLoadServiceMock().FakedObject,
            new ImageDataServiceMock().FakedObject,
            new SettingsServiceMock().FakedObject);
        window.DataContext = viewModel;

        window.Show();
        Dispatcher.UIThread.RunJobs();
        var loadImageButton = view.Get<Button>("LoadImageButton");
        loadImageButton!.Command!.Execute(null);

        var loadDataButton = view.Get<Button>("LoadDataButton");
        var saveButton = view.Get<Button>("SaveButton");
        var copyClipboard = view.Get<Button>("CopyClipboard");

        Assert.True(loadDataButton.IsEnabled && saveButton.IsEnabled && copyClipboard.IsEnabled);
    }*/
}