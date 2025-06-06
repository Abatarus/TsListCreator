﻿using Avalonia.Controls;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using System;
using Avalonia.Interactivity;

namespace TSListCreator.Thumbs
{
    public class MovedThumb: Thumb
    {
        protected override void OnDragDelta(VectorEventArgs e)
        {
            if (DataContext is StyledElement designerItem)
            {
                double left = Canvas.GetLeft(designerItem);
                double top = Canvas.GetTop(designerItem);
                StyledElement parentCanvas = designerItem.Parent;
                while (designerItem.Parent is not Canvas)
                {
                    parentCanvas = parentCanvas!.Parent!;
                    if (parentCanvas is Window)
                    {
                        throw new Exception("MoveThumb must be inside Canvas");
                    }
                }

                double right = parentCanvas.GetValue(WidthProperty);
                double bottom = parentCanvas.GetValue(HeightProperty);

                double newPosX = left + e.Vector.X;
                double newPosY = top + e.Vector.Y;
                if (newPosX < 0)
                {
                    newPosX = 0;
                }
                else if (newPosX + designerItem.GetValue(WidthProperty) > right)
                {
                    newPosX = right - designerItem.GetValue(WidthProperty);
                }
                if (newPosY < 0)
                {
                    newPosY = 0;
                }
                else if (newPosY + designerItem.GetValue(HeightProperty) > bottom)
                {
                    newPosY = bottom - designerItem.GetValue(HeightProperty);
                }
                Canvas.SetLeft(designerItem, newPosX);
                Canvas.SetTop(designerItem, newPosY);
            }
        }
    }
}
