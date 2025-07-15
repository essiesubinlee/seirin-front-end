using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using seirin1.Controls;
using seirin1.ViewModels; // Assuming DashboardViewModel is in seirin1.ViewModels based on your DashboardViewModel.cs
using System;
using System.Diagnostics;

namespace seirin1.Pages
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            // Initialize window positions when page appears
            this.Appearing += OnPageAppearing;
        }

        private void OnPageAppearing(object sender, EventArgs e)
        {
            // Center windows when page first appears if they are visible

            CenterWindow(CustomWindow1);
            CenterWindow(CustomWindow2);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // Re-center windows when page size changes if they are visible
            if (width > 0 && height > 0)
            {
                CenterWindow(CustomWindow1);
                CenterWindow(CustomWindow2);
            }
        }

        private void CenterWindow(CustomWindow window)
        {
            // Only center if the window is currently visible, otherwise Width/Height might not be calculated correctly.
            // Or ensure your WidthRequest/HeightRequest are always set on the CustomWindow.
            if (window == null || !window.IsVisible) return;

            // Use ActualWidth and ActualHeight if available, otherwise fall back to Request
            // Note: ActualWidth/Height become available after layout pass.
            var windowWidth = window.Width > 0 ? window.Width : window.WidthRequest;
            var windowHeight = window.Height > 0 ? window.Height : window.HeightRequest;

            // Fallback to reasonable defaults if measurements aren't available
            if (double.IsNaN(windowWidth) || windowWidth <= 0) windowWidth = 300;
            if (double.IsNaN(windowHeight) || windowHeight <= 0) windowHeight = 200;

            // Calculate center position relative to the content page
            var centerX = (this.Width - windowWidth) / 2;
            var centerY = (this.Height - windowHeight) / 2;

            // Apply position with bounds checking
            window.TranslationX = Math.Max(0, centerX);
            window.TranslationY = Math.Max(0, centerY);
        }
   
        // Removed direct button click handlers, now handled by ViewModel Commands
        // private void OnShowWindow1Click(object sender, EventArgs e) { ... }
        // private void OnShowWindow2Click(object sender, EventArgs e) { ... }
        // private void OnAddLineChartClicked(object sender, EventArgs e) { ... }

        private void OnAddLineChartClicked(object sender, EventArgs e)
        {
            
            // Ensure the BindingContext is a DashboardViewModel
            if (this.BindingContext is DashboardViewModel vm)
            {
                vm.AddPowerLineChartCommand.CanExecute(null);
            }
            else
            {
                Debug.WriteLine("OnAddLineChartClicked: BindingContext is not DashboardViewModel.");
            }
        }
    }
}