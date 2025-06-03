using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using System;
using TSListCreator.Thumbs;

namespace TSListCreator.Views;

public partial class TextBoxCanvasView: UserControl
{
    private bool _isPointerPressed = false;
    public TextBoxCanvasView()
    {
        InitializeComponent();
    }
    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (_isPointerPressed)
        {
            double newWidth = Math.Abs(Canvas.GetLeft(this) - e.GetPosition((Visual)Parent!).X);
            if (MinWidth < newWidth)
            {
                Width = newWidth;
            }
        }
    }
    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        _isPointerPressed = true;
    }

    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _isPointerPressed = false;
    }
}
