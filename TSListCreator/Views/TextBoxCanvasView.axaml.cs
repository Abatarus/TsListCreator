using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using System;
using System.ComponentModel.Design;
using TSListCreator.Thumbs;

namespace TSListCreator.Views;

public partial class TextBoxCanvasView: UserControl
{
    private bool _isPointerPressed = false;
    private bool _isRightLeftStretching = false;
    public TextBoxCanvasView()
    {
        InitializeComponent();
    }
    private const double BORDER = 5;
    private static readonly Cursor _bottonCursor = new Cursor(StandardCursorType.SizeNorthSouth);
    private static readonly Cursor _leftRightCursor = new Cursor(StandardCursorType.SizeWestEast);

    private bool IsRightLeftStretching(Point point)
    {
        if (Height - point.Y < 0)
        {
            return false;
        }
        double heightDelta = Math.Abs(Height - point.Y);
        return heightDelta > BORDER;
    }
    private void SetCorrectCursor()
    {
        if (_isRightLeftStretching)
        {
            if (Cursor != _leftRightCursor)
            {
                Cursor = _leftRightCursor;
            }
        }
        else
        {
            if (Cursor != _bottonCursor)
            {
                Cursor = _bottonCursor;
            }
        }
    }
    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        _isRightLeftStretching = IsRightLeftStretching(e.GetCurrentPoint(this).Position);
        SetCorrectCursor();
        if (_isPointerPressed)
        {
            if (_isRightLeftStretching)
            {
                double newWidth = Math.Abs(Canvas.GetLeft(this) - e.GetPosition((Visual)Parent!).X);
                if (MinWidth < newWidth)
                {
                    Width = newWidth;
                }
            }
            else
            {
                double newHeight = Math.Abs(Canvas.GetTop(this) - e.GetPosition((Visual)Parent!).Y);
                int newIntHeight = (int)(newHeight / MinHeight);
                if (newIntHeight >= 1)
                {
                    Height = newIntHeight * MinHeight;
                }
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
