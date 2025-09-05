using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia;
using Avalonia.LogicalTree;
using TSListCreator.Enums;
using TSListCreator.Interfaces;
using Avalonia.Data;

namespace TSListCreator.Behaviors
{
    public class StretchSizeCanvasItemBehavior: Behavior<Thumb>
    {
        public static readonly StyledProperty<Mode> ModeProperty =
            AvaloniaProperty.Register<StretchSizeCanvasItemBehavior, Mode>(
                nameof(Mode), defaultValue: Mode.Move, defaultBindingMode: BindingMode.OneWay);
        public Mode Mode
        {
            get => GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject!.PointerMoved += OnPointerMoved!;
            AssociatedObject!.DragStarted += OnDragStarted!;
            AssociatedObject!.DragCompleted += OnDragCompleted!;
        }

        protected override void OnDetaching()
        {
            AssociatedObject!.PointerMoved -= OnPointerMoved!;
            AssociatedObject!.DragStarted -= OnDragStarted!;
            AssociatedObject!.DragCompleted -= OnDragCompleted!;
            base.OnDetaching();
        }

        private bool _isPointerPressed = false;
        private Visual _ancestor;
        private void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (!_isPointerPressed || Mode != Mode.Stretch) return;
            if (AssociatedObject.DataContext is not ICanvasDrawable item)
            {
                throw new Exception("DataContext.DataContext must be TsControl");
            } 
            _ancestor = ((Visual)AssociatedObject!).FindLogicalAncestorOfType<ItemsControl>();
            double newWidth = Math.Abs(item.CanvasPosX - e.GetPosition(_ancestor).X);
            double newHeight = Math.Abs(item.CanvasPosY - e.GetPosition(_ancestor).Y);
            double max = Math.Max(newWidth, newHeight);
            if (item.CanvasMinWidth < max)
            {
                item.CanvasWidth = max;
                item.CanvasHeight = max;
            }
        }
        private void OnDragStarted(object? sender, VectorEventArgs e)
        {
            _isPointerPressed = true;
        }

        private void OnDragCompleted(object? sender, VectorEventArgs e)
        {
            _isPointerPressed = false;
        }
    }
}
