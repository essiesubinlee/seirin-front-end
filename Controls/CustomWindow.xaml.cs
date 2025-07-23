using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using seirin1.Services;
using seirin1.ViewModels;
using System.ComponentModel;
using System.Diagnostics;

namespace seirin1.Controls
{
    public partial class CustomWindow : Border
    {
        //private Point _startDragPosition;
        private Rect _startDragBounds; // Stores X, Y, Width, Height at the START of the gesture
        private bool _isDragging;
        private bool _isResizing;
        private ResizeDirection _resizeDirection;
        private readonly ILogService _logger;
        public event Action<string> OnRefreshRequested;



        // Public property to expose the content host
        public ContentView ContentHost => WindowContentHost;

        private Point _currentInitialTouchPosition;

        private const int ResizeBorderSize = 20;

        public static readonly BindableProperty WindowContentProperty =
            BindableProperty.Create(nameof(WindowContent), typeof(View), typeof(CustomWindow), propertyChanged: OnWindowContentChanged);
        public static readonly BindableProperty WindowTypeProperty =
       BindableProperty.Create(nameof(WindowType), typeof(string), typeof(CustomWindow));


        public View WindowContent
        {
            get => (View)GetValue(WindowContentProperty);
            set => SetValue(WindowContentProperty, value);
        }
        public string WindowType
        {
            get => (string)GetValue(WindowTypeProperty);
            set => SetValue(WindowTypeProperty, value);
        }

        public static readonly BindableProperty WindowTitleProperty =
            BindableProperty.Create(nameof(WindowTitle), typeof(string), typeof(CustomWindow), "Resizable Window", propertyChanged: OnWindowTitleChanged);

        public string WindowTitle
        {
            get => (string)GetValue(WindowTitleProperty);
            set
            {
                Debug.WriteLine($"CUSTOM WINDOW WindowTitle = {WindowTitle}");
                SetValue(WindowTitleProperty, value);
            }
        }
        public CustomWindow()
        {
            _logger = DependencyService.Get<ILogService>();
            InitializeComponent();
            SetupGestures();
            BindingContext = this; // Crucial for WindowTitle to bind correctly
                                   // But ensure the content maintains its own context

            // Forward gestures to content

            if (WindowContentHost?.Content != null)
            {
                WindowContentHost.Content.BindingContext = null;
            }



        }

 
        private static void OnWindowTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var window = (CustomWindow)bindable;
            Debug.WriteLine($"WindowTitle changed from '{oldValue}' to '{newValue}'");
            // Force update the title label if needed
            if (window.TitleBarHost?.Children.FirstOrDefault(x => x is Label) is Label titleLabel)
            {
                titleLabel.Text = newValue?.ToString();
            }
        }
        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            if (child is View view)
            {
                view.InputTransparent = false;
            }
        }

        private bool _isMinimized = false;
        private double _originalHeight;

        private void OnMinimizeClicked(object sender, EventArgs e)
        {
            if (_isMinimized) return;

            _originalHeight = HeightRequest;
             HeightRequest = 100; // Set minimized height
            _isMinimized = true;
        }

        private void OnMaximizeClicked(object sender, EventArgs e)
        {
            if (!_isMinimized) return;

            HeightRequest = _originalHeight;
            _isMinimized = false;
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {


            if (WindowContentHost?.Content?.BindingContext is DashboardViewModel vm)
            {
                Debug.WriteLine($"Refresh button clicked for WindowType: {this.WindowType}");
                // Call the RefreshChart method on the DashboardViewModel,
                // passing the WindowType of this CustomWindow
                await vm.RefreshChart(this.WindowType);
            }
            else
            {
                Debug.WriteLine($"CustomWindow: Could not find DashboardViewModel in WindowContentHost.Content.BindingContext for WindowType: {this.WindowType}. Refresh failed.");
            }
        }






        public void SetContentBindingContext(object? bindingContext)
        {
            if (WindowContentHost?.Content != null)
            {
                WindowContentHost.Content.BindingContext = bindingContext;
            }
        }
        private static void OnWindowContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Debug.WriteLine($"OnWindowContentChanged() called");
            var control = (CustomWindow)bindable;
            if (control.WindowContentHost != null)
            {
                control.WindowContentHost.Content = (View)newValue;
            }
        }

        public enum ResizeDirection
        {
            None,
            Top,
            Bottom,
            Left,
            Right,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        private void SetupGestures()
        {
            // Attach the move gesture to TitleBarHost (the Grid that contains the title and close button)
            var titleBarPan = new PanGestureRecognizer();
            titleBarPan.PanUpdated += OnTitleBarPanUpdated;
            TitleBarHost.GestureRecognizers.Add(titleBarPan); // Attached to TitleBarHost (the Grid)

            // All edges and corners for resizing will use the same PanGestureRecognizer
            var resizePan = new PanGestureRecognizer();
            resizePan.PanUpdated += OnResizeHandlePanUpdated;

            // Add resize gesture recognizer to all transparent BoxViews
            BottomEdge.GestureRecognizers.Add(resizePan);
            LeftEdge.GestureRecognizers.Add(resizePan);
            RightEdge.GestureRecognizers.Add(resizePan);
            TopLeftCorner.GestureRecognizers.Add(resizePan);
            TopRightCorner.GestureRecognizers.Add(resizePan);
            BottomLeftCorner.GestureRecognizers.Add(resizePan);
            BottomRightCorner.GestureRecognizers.Add(resizePan);
            TopEdge.GestureRecognizers.Add(resizePan);
        }

        public void StartDrag(Point currentTouchPoint)
        {
            _isDragging = true;
            _currentInitialTouchPosition = currentTouchPoint;
            _startDragBounds = new Rect(TranslationX, TranslationY, Width, Height);
        }

        public void HandleDrag(Point currentTouchPoint)
        {
            if (!_isDragging) return;

            double deltaX = currentTouchPoint.X - _currentInitialTouchPosition.X;
            double deltaY = currentTouchPoint.Y - _currentInitialTouchPosition.Y;

            TranslationX = _startDragBounds.X + deltaX;
            TranslationY = _startDragBounds.Y + deltaY;
        }

        public void StartResize(Point currentTouchPoint, ResizeDirection direction)
        {
            _isResizing = true;
            _resizeDirection = direction;
            _currentInitialTouchPosition = currentTouchPoint;
            _startDragBounds = new Rect(TranslationX, TranslationY, Width, Height);
        }

        public void HandleResize(Point currentTouchPoint)
        {
            if (!_isResizing) return;

            double deltaX = currentTouchPoint.X - _currentInitialTouchPosition.X;
            double deltaY = currentTouchPoint.Y - _currentInitialTouchPosition.Y;

            double newX = _startDragBounds.X;
            double newY = _startDragBounds.Y;
            double newWidth = _startDragBounds.Width;
            double newHeight = _startDragBounds.Height;

            switch (_resizeDirection)
            {
                case ResizeDirection.TopLeft:
                    newX = _startDragBounds.X + deltaX;
                    newY = _startDragBounds.Y + deltaY;
                    newWidth = _startDragBounds.Width - deltaX;
                    newHeight = _startDragBounds.Height - deltaY;
                    break;
                case ResizeDirection.Top:
                    newY = _startDragBounds.Y + deltaY;
                    newHeight = _startDragBounds.Height - deltaY;
                    break;
                case ResizeDirection.TopRight:
                    newY = _startDragBounds.Y + deltaY;
                    newWidth = _startDragBounds.Width + deltaX;
                    newHeight = _startDragBounds.Height - deltaY;
                    break;
                case ResizeDirection.Left:
                    newX = _startDragBounds.X + deltaX;
                    newWidth = _startDragBounds.Width - deltaX;
                    break;
                case ResizeDirection.Right:
                    newWidth = _startDragBounds.Width + deltaX;
                    break;
                case ResizeDirection.BottomLeft:
                    newX = _startDragBounds.X + deltaX;
                    newWidth = _startDragBounds.Width - deltaX;
                    newHeight = _startDragBounds.Height + deltaY;
                    break;
                case ResizeDirection.Bottom:
                    newHeight = _startDragBounds.Height + deltaY;
                    break;
                case ResizeDirection.BottomRight:
                    newWidth = _startDragBounds.Width + deltaX;
                    newHeight = _startDragBounds.Height + deltaY;
                    break;
            }

            // Apply new dimensions, respecting minimum size
            if (newWidth >= MinimumWidthRequest)
            {
                WidthRequest = newWidth;
                TranslationX = newX; // Only update X position for left-side resizing
            }
            if (newHeight >= MinimumHeightRequest)
            {
                HeightRequest = newHeight;
                TranslationY = newY; // Only update Y position for top-side resizing
            }
        }

        public void EndInteraction()
        {
            _isDragging = false;
            _isResizing = false;
            _resizeDirection = ResizeDirection.None;
        }

        private void OnTitleBarPanUpdated(object? sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    StartDrag(new Point(e.TotalX, e.TotalY));
                    break;
                case GestureStatus.Running:
                    HandleDrag(new Point(e.TotalX, e.TotalY));
                    break;
                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    EndInteraction();
                    break;
            }
        }

        private void OnResizeHandlePanUpdated(object? sender, PanUpdatedEventArgs e)
        {
            ResizeDirection direction = ResizeDirection.None;
            if (sender == BottomEdge) direction = ResizeDirection.Bottom;
            else if (sender == LeftEdge) direction = ResizeDirection.Left;
            else if (sender == RightEdge) direction = ResizeDirection.Right;
            else if (sender == BottomLeftCorner) direction = ResizeDirection.BottomLeft;
            else if (sender == BottomRightCorner) direction = ResizeDirection.BottomRight;
            else if (sender == TopLeftCorner) direction = ResizeDirection.TopLeft;
            else if (sender == TopRightCorner) direction = ResizeDirection.TopRight;
            else if (sender == TopEdge) direction = ResizeDirection.Top;

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    if (direction != ResizeDirection.None)
                    {
                        StartResize(new Point(e.TotalX, e.TotalY), direction);
                    }
                    break;
                case GestureStatus.Running:
                    HandleResize(new Point(e.TotalX, e.TotalY));
                    break;
                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    EndInteraction();
                    break;
            }
        }

        private void OnCloseClicked(object? sender, EventArgs e)
        {
            IsVisible = false;
            Debug.WriteLine($"CustomWindow.xaml.cs:OnCloseClicked() called IsVisible={IsVisible}");
        }
        public void Restore()
        {
            IsVisible = true;
            Debug.WriteLine($"CustomWindow.xaml.cs: Restore() called IsVisible={IsVisible}");
        }
    }
}