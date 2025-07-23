using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using seirin1.Controls;
using seirin1.ViewModels;
using System;
using System.Diagnostics;

namespace seirin1.Pages
{
    public partial class CustomCharts : ContentPage
    {
        public CustomCharts()
        {
            InitializeComponent();

            // Initialize window positions when page appears
            //this.Appearing += OnPageAppearing;
        }

        private void OnPageAppearing(object sender, EventArgs e)
        {
            // Center windows when page first appears
            if (BindingContext is DashboardViewModel vm)
            {
                CenterWindow(CustomWindow1);
                CenterWindow(CustomWindow2);
                CenterWindow(CustomWindow3);
                CenterWindow(CustomWindow4);
                CenterWindow(CustomWindow5);
                CenterWindow(CustomWindow6);
                CenterWindow(CustomWindow7);
                CenterWindow(CustomWindow8);
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // Re-center windows when page size changes
            if (width > 0 && height > 0)
            {
                CenterWindow(CustomWindow1);
                CenterWindow(CustomWindow2);
                CenterWindow(CustomWindow3);
                CenterWindow(CustomWindow4);
                CenterWindow(CustomWindow5);
                CenterWindow(CustomWindow6);
                CenterWindow(CustomWindow7);
                CenterWindow(CustomWindow8);
            }
        }



        private void CenterWindow(CustomWindow window)
        {
            if (window == null || !window.IsVisible) return;

            window.HorizontalOptions = LayoutOptions.Center;
            window.VerticalOptions = LayoutOptions.Start;
            window.Margin = new Thickness(0, 20, 0, 0); // Top margin

            // Make sure it's visible and in the container
           
        }


        public int Window1ZIndex { get; set; }
        public int Window2ZIndex { get; set; }
        public int Window3ZIndex { get; set; }
        public int Window4ZIndex { get; set; }
        public int Window5ZIndex { get; set; }
        public int Window6ZIndex { get; set; }
        public int Window7ZIndex { get; set; }
        public int Window8ZIndex { get; set; }



        private void OnShowWindow1Click(object sender, EventArgs e)
        {
            CustomWindow1.IsVisible = true;
            CustomWindow1.WindowType = "voltage";
            CustomWindow1.SetContentBindingContext(this.BindingContext);
            CustomWindow1.OnRefreshRequested -= HandleRefreshRequest;
            CustomWindow1.OnRefreshRequested += HandleRefreshRequest;
            //CenterWindow(CustomWindow1);
            BringToFront(1);
        }

        private void OnShowWindow2Click(object sender, EventArgs e)
        {
            CustomWindow2.IsVisible = true;
            CustomWindow2.WindowType = "power";
            CustomWindow2.SetContentBindingContext(this.BindingContext);
            CustomWindow2.OnRefreshRequested -= HandleRefreshRequest;       
            CustomWindow2.OnRefreshRequested += HandleRefreshRequest;
            //CenterWindow(CustomWindow2);
            BringToFront(2);
        }

        private void OnShowWindow3Click(object sender, EventArgs e)
        {
            CustomWindow3.IsVisible = true;
            CustomWindow3.WindowType = "current";

            CustomWindow3.SetContentBindingContext(this.BindingContext);
            CustomWindow3.OnRefreshRequested -= HandleRefreshRequest;
            CustomWindow3.OnRefreshRequested += HandleRefreshRequest;
            //CenterWindow(CustomWindow3);
            BringToFront(3);
        }

        private void OnShowWindow4Click(object sender, EventArgs e)
        {
            CustomWindow4.IsVisible = true;
            CustomWindow4.WindowType = "solarRad";

            CustomWindow4.SetContentBindingContext(this.BindingContext);
            CustomWindow4.OnRefreshRequested -= HandleRefreshRequest;
            CustomWindow4.OnRefreshRequested += HandleRefreshRequest;
            //CenterWindow(CustomWindow4);
            BringToFront(4);
        }

        private void OnShowWindow5Click(object sender, EventArgs e)
        {
            CustomWindow5.IsVisible = true;
            CustomWindow5.WindowType = "temp";

            CustomWindow5.SetContentBindingContext(this.BindingContext);
            CustomWindow5.OnRefreshRequested -= HandleRefreshRequest;
            CustomWindow5.OnRefreshRequested += HandleRefreshRequest;
            //CenterWindow(CustomWindow5);
            BringToFront(5);
        }

        private void OnShowWindow6Click(object sender, EventArgs e)
        {
            CustomWindow6.IsVisible = true;
            CustomWindow6.WindowType = "humidity";

            CustomWindow6.SetContentBindingContext(this.BindingContext);
            CustomWindow6.OnRefreshRequested -= HandleRefreshRequest;
            CustomWindow6.OnRefreshRequested += HandleRefreshRequest;
            //CenterWindow(CustomWindow6);
            BringToFront(6);
        }
        private void OnShowWindow7Click(object sender, EventArgs e)
        {
            CustomWindow7.IsVisible = true;
            CustomWindow7.WindowType = "SOC";

            CustomWindow7.SetContentBindingContext(this.BindingContext);
            CustomWindow7.OnRefreshRequested -= HandleRefreshRequest;
            CustomWindow7.OnRefreshRequested += HandleRefreshRequest;
            //CenterWindow(CustomWindow6);
            BringToFront(7);
        }
        private void OnShowWindow8Click(object sender, EventArgs e)
        {
            CustomWindow8.IsVisible = true;
            CustomWindow8.WindowType = "batteryTemp";

            CustomWindow8.SetContentBindingContext(this.BindingContext);

            CustomWindow8.OnRefreshRequested -= HandleRefreshRequest;
            CustomWindow8.OnRefreshRequested += HandleRefreshRequest;
            //CenterWindow(CustomWindow6);
            BringToFront(8);
        }


        private async void HandleRefreshRequest(string windowType)


        {
            if (BindingContext is DashboardViewModel vm)
            {
                try
                {
                    await vm.RefreshChart(windowType); // This now handles both date reset and reload
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error refreshing {windowType}: {ex.Message}");
                    // Consider showing an alert to the user
                }
            }
        }



        private int _currentZIndex = 0;
        private void BringToFront(int windowNumber)
        {
            // Reset all ZIndexes to 0
            Window1ZIndex = 0;
            Window2ZIndex = 0;
            Window3ZIndex = 0;
            Window4ZIndex = 0;
            Window5ZIndex = 0;
            Window6ZIndex = 0;
            Window7ZIndex = 0;
            Window8ZIndex = 0;

            // Set the selected window to highest ZIndex
            switch (windowNumber)
            {
                case 1: Window1ZIndex = ++_currentZIndex; break;
                case 2: Window2ZIndex = ++_currentZIndex; break;
                case 3: Window3ZIndex = ++_currentZIndex; break;
                case 4: Window4ZIndex = ++_currentZIndex; break;
                case 5: Window5ZIndex = ++_currentZIndex; break;
                case 6: Window6ZIndex = ++_currentZIndex; break;
                case 7: Window7ZIndex = ++_currentZIndex; break;
                case 8: Window8ZIndex = ++_currentZIndex; break;
            }

            // Notify property changes
            OnPropertyChanged(nameof(Window1ZIndex));
            OnPropertyChanged(nameof(Window2ZIndex));
            OnPropertyChanged(nameof(Window3ZIndex));
            OnPropertyChanged(nameof(Window4ZIndex));
            OnPropertyChanged(nameof(Window5ZIndex));
            OnPropertyChanged(nameof(Window6ZIndex));
            OnPropertyChanged(nameof(Window7ZIndex));
            OnPropertyChanged(nameof(Window8ZIndex));
            //AbsoluteLayout.SetZIndex(window, _maxZIndex);

        }





    }
}