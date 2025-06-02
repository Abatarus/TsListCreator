using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using System;
using TSListCreator.Thumbs;

namespace TSListCreator.Views;

public partial class TextBoxCanvasView: UserControl
{
    private Point _position = new(0,0);
    private bool _isDragged = false;
    public TextBoxCanvasView()
    {
        InitializeComponent();
        //AddHandler(DragDrop.DragOverEvent, DragOver);
        AddHandler(DragDrop.DragEnterEvent, DragOver);
    }

    /*private async void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        Console.WriteLine("DoDrag start");

        var dragData = new DataObject();
        
        var result = await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Move);
        Console.WriteLine($"DragAndDrop result: {result}");
    }*/
    private void OnPointerExited(object? sender, PointerEventArgs e)
    {

        _isDragged = false;
    }
    private void OnPointerEntered(object? sender, PointerEventArgs e)
    {

        _isDragged = true;
    }


    private void DragOver(object? sender, DragEventArgs e) {
        if (!_isDragged) return;
        var currentPosition = e.GetPosition((Visual)Parent!);
        var offsetX = currentPosition.X - Width/2 - _position.X;
        var offsetY = currentPosition.Y - Height/2 - _position.Y;
        Canvas.SetLeft(this, offsetX);
        Canvas.SetTop(this, offsetY);
    }
}
