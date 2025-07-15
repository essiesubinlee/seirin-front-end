// Controls/MovableWindow.cs
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;

namespace seirin1.Controls
{
    public class MovableWindow : ContentView
    {
        private const double ResizeHandleSize = 20;
        private Point _lastPanPosition;
        private bool _isResizing;
        private Rect _resizeHandleRect;

        public MovableWindow()
        {
            // Basic styling
            BackgroundColor = Colors.White;
            Padding = new Thickness(10);
            Shadow = new Shadow { Brush = Brush.Black, Opacity = 0.2f, Radius = 10, Offset = new Point(4, 4) };

            // Add gestures
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => BringToFront();
            GestureRecognizers.Add(tapGesture);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            _resizeHandleRect = new Rect(width - ResizeHandleSize, height - ResizeHandleSize, ResizeHandleSize, ResizeHandleSize);
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    if (_resizeHandleRect.Contains(new Point(e.TotalX, e.TotalY)))
                        _isResizing = true;
                    else
                        _lastPanPosition = new Point(TranslationX, TranslationY);
                    break;

                case GestureStatus.Running:
                    if (_isResizing)
                    {
                        WidthRequest = Math.Max(200, Width + e.TotalX);
                        HeightRequest = Math.Max(150, Height + e.TotalY);
                    }
                    else
                    {
                        TranslationX = _lastPanPosition.X + e.TotalX;
                        TranslationY = _lastPanPosition.Y + e.TotalY;
                    }
                    break;

                case GestureStatus.Completed:
                    _isResizing = false;
                    break;
            }
        }

        public void BringToFront()
        {
            if (Parent is Layout parent)
            {
                // This is the reliable way to bring to front
                parent.Children.Remove(this);
                parent.Children.Add(this);

                //// Update ZIndex for visual consistency
                //foreach (var child in parent.Children)
                //{
                //    child.ZIndex = child == this ? 1 : 0;
                //}
            }
        }
    }
}