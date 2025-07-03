namespace seirin1.Pages;

public partial class Charts : ContentPage
{
    public Charts()
    {
        InitializeComponent();
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
        if (sender is Button btn && BindingContext is DashboardViewModel vm)
        {
            if (btn.BindingContext is object item && vm.Items.Contains(item))
                vm.Items.Remove(item);
        }
    }

    private void OnAddChartClicked(object sender, EventArgs e)
    {
        if (BindingContext is DashboardViewModel vm)
            vm.AddChart();
    }

    private void OnAddWeatherClicked(object sender, EventArgs e)
    {
        if (BindingContext is DashboardViewModel vm)
            vm.AddWeather();
    }

}
