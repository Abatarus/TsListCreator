using Avalonia.Controls;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using System;
using Avalonia.Interactivity;
using TSListCreator.Controls;

namespace TSListCreator.Thumbs
{
    public class MovedThumb : Thumb
    {
        protected override void OnDragDelta(VectorEventArgs e)
        {
            if (DataContext is not StyledElement designerItem)
            {
                throw new Exception("DataContext must be control");
            }
            if (designerItem.DataContext is not TsControl tsControl)
            {
                throw new Exception("DataContext.DataContext must be TsControl");
            }

            double left = tsControl.CanvasPosX;
            double top = tsControl.CanvasPosY;

            double newPosX = left + e.Vector.X;
            double newPosY = top + e.Vector.Y;
            tsControl.CanvasPosX = newPosX;
            tsControl.CanvasPosY = newPosY;
        }
    }
}
