using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using TSListCreator.Controls;
using TSListCreator.Interfaces;

namespace TSListCreator.Views;

public partial class TextBoxCanvasView : UserControl
{
    private bool _isPointerPressed = false;
    private bool _isRightLeftStretching = false;
    private Border border;
    private double PosX => ((ICanvasDrawable)(DataContext)).CanvasPosX;
    private double PosY => ((ICanvasDrawable)(DataContext)).CanvasPosY;
    private double CanvasHeight
    {
        get => ((ICanvasDrawable)(DataContext)).CanvasHeight;
        set => ((ICanvasDrawable)(DataContext)).CanvasHeight = value;
    }

    public TextBoxCanvasView()
    {
        InitializeComponent();
        border = this.Get<Border>("ResizeBorder");
    }
    private static readonly Cursor _bottonCursor = new Cursor(StandardCursorType.SizeNorthSouth);
    private static readonly Cursor _leftRightCursor = new Cursor(StandardCursorType.SizeWestEast);


    double sign(double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y)
    {
        return (p1X - p3X) * (p2Y - p3Y) - (p2X - p3X) * (p1Y - p3Y);
    }
    bool PointInTriangle(double pX, double pY, double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y)
    {
        var d1 = sign(pX, pY, p1X, p1Y, p2X, p2Y);
        var d2 = sign(pX, pY, p2X, p2Y, p3X, p3Y);
        var d3 = sign(pX, pY, p3X, p3Y, p1X, p1Y);
        var hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        var hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);
        return !(hasNeg && hasPos);
    }
    private bool IsRightLeftStretching(Point point)
    {
        return !PointInTriangle(point.X, point.Y,
            0, border.Height,
            border.Width / 2, border.Height / 2,
            border.Width, border.Height) && point.Y < border.Height;
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
        _isRightLeftStretching = IsRightLeftStretching(e.GetCurrentPoint(border).Position);
        SetCorrectCursor();
        if (_isPointerPressed)
        {
            if (_isRightLeftStretching)
            {
                double newWidth = Math.Abs(PosX - e.GetPosition((Visual)Parent!.Parent!).X);
                if (border.MinWidth < newWidth)
                {
                    border.Width = newWidth;
                }
            }
            else
            {
                double newHeight = Math.Abs(PosY - e.GetPosition((Visual)Parent!.Parent!).Y);
                if (newHeight >= border.MinHeight)
                {
                    CanvasHeight = newHeight;
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
