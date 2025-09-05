using Avalonia.Controls;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.LogicalTree;
using TSListCreator.Enums;
using TSListCreator.Interfaces;
using System.Threading;

namespace TSListCreator.Behaviors
{
    public class StretchTextBoxBehavior : Behavior<Thumb>
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
        private bool _isRightLeftStretching = false;
        private Visual _ancestor;
       
        private void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (!_isPointerPressed || Mode != Mode.Stretch) return;
            if (AssociatedObject.DataContext is not ICanvasDrawable item)
            {
                throw new Exception("DataContext.DataContext must be TsControl");
            }

            _ancestor = ((Visual)AssociatedObject!).FindLogicalAncestorOfType<ItemsControl>();
            _isRightLeftStretching = IsRightLeftStretching(item, e.GetPosition(AssociatedObject));
            if (_isRightLeftStretching)
            {
                double newWidth = Math.Abs(item.CanvasPosX - e.GetPosition(_ancestor).X);
                if (item.CanvasMinWidth < newWidth)
                {
                    item.CanvasWidth = newWidth;
                }
            }
            else
            {
                double newHeight = Math.Abs(item.CanvasPosY - e.GetPosition(_ancestor).Y);
                if (newHeight >= item.CanvasMinHeight)
                {
                    item.CanvasHeight = newHeight;
                }
            }
        }
        private bool IsRightLeftStretching(ICanvasDrawable item, Point point)
        {
            var width = item.CanvasWidth;
            var height = item.CanvasHeight;
            return !PointInTriangle(point.X, point.Y,
                0, height,
                width / 2, height / 2,
                width, height) && point.Y < height;
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
        double sign(double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y)
        {
            return (p1X - p3X) * (p2Y - p3Y) - (p2X - p3X) * (p1Y - p3Y);
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
