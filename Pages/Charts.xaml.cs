// In Charts.xaml.cs
namespace seirin1.Pages;
public partial class Charts : ContentPage
{
    private DashboardViewModel ViewModel => BindingContext as DashboardViewModel;

    public Charts()
    {
        InitializeComponent();
    }

    private void OnAddLineChartClicked(object sender, EventArgs e)
    {
        ViewModel?.AddPowerLineChartCommand.Execute(null);
    }

    private void OnAddCurrentChartClicked(object sender, EventArgs e)
    {
        ViewModel?.AddCurrentLineChartCommand.Execute(null);
    }

    private void OnAddVoltageChartClicked(object sender, EventArgs e)
    {
        ViewModel?.AddVoltageLineChartCommand.Execute(null);
    }
    private void OnAddSolarRadChartClicked(object sender, EventArgs e)
    {
        ViewModel?.AddSolarRadLineChartCommand.Execute(null);
    }

    

    private void OnAddWeatherClicked(object sender, EventArgs e)
    {
        ViewModel?.AddWeatherCommand.Execute(null);
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext != null)
        {
            ViewModel?.DeleteItemCommand.Execute(button.BindingContext);
        }
    }
}