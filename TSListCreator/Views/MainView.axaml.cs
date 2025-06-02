using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Metadata;

namespace TSListCreator.Views;

public partial class MainView : UserControl
{
    private bool _isMoved = false;
    public MainView()
    {
        InitializeComponent();
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        e.Handled = true;
        if (!_isMoved) return;
        
    }
    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        _isMoved = true;
    }
    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _isMoved = false;
    }
}
