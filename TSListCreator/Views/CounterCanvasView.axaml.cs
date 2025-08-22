using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System;
using TSListCreator.Controls;

namespace TSListCreator.Views;

public partial class CounterCanvasView : UserControl
{
    public CounterCanvasView()
    {
        InitializeComponent();
        _border = this.Get<Border>("ResizeBorder");
        _border.Height = _border.Width;
    }
    private bool _isPointerPressed = false;
    private Border _border;
    private double PosX => ((TsControl)(DataContext)).PosX;
    private double PosY => ((TsControl)(DataContext)).PosY;
    private const double BORDER = 5;

    private bool IsRightLeftStretching(Point point)
    {
        if (_border.Height - point.Y < 0)
        {
            return false;
        }
        double heightDelta = Math.Abs(_border.Height - point.Y);
        return heightDelta > BORDER;
    }
    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (_isPointerPressed)
        {
            double newWidth = Math.Abs(PosX - e.GetPosition((Visual)Parent.Parent!).X);
            double newHeight = Math.Abs(PosY - e.GetPosition((Visual)Parent.Parent!).Y);
            double max = Math.Max(newWidth, newHeight);
            if (_border.MinWidth < max)
            {
                _border.Width = max;
                _border.Height = max;
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