using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Xaml.Interactivity;
using System;
using Avalonia.Data;
using TSListCreator.Controls;
using TSListCreator.Enums;
using TSListCreator.Interfaces;

namespace TSListCreator.Behaviors
{
    public class MoveCanvasItemBehavior: Behavior<Thumb>
    {
        public static readonly StyledProperty<Mode> ModeProperty =
            AvaloniaProperty.Register<MoveCanvasItemBehavior, Mode>(
                nameof(Mode), defaultValue: Mode.Move, defaultBindingMode: BindingMode.OneWay);
        public Mode Mode
        {
            get => GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject is not Thumb)
            {
                throw new Exception("DataContext must be control");
            }
            AssociatedObject.DragDelta += OnDragDelta!;
        }
        protected override void OnDetaching()
        {
            AssociatedObject!.DragDelta -= OnDragDelta!;
            base.OnDetaching();
        }

        protected void OnDragDelta(object s, VectorEventArgs e)
        {
            if (Mode != Mode.Move)
            {
                return;
            }
            if (AssociatedObject.DataContext is not ICanvasDrawable item)
            {
                throw new Exception("DataContext.DataContext must be TsControl");
            }
            double left = item.CanvasPosX;
            double top = item.CanvasPosY;

            double newPosX = left + e.Vector.X;
            double newPosY = top + e.Vector.Y;
            item.CanvasPosX = newPosX;
            item.CanvasPosY = newPosY;
        }
    }
}
