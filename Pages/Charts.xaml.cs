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
        ViewModel?.AddLineChartCommand.Execute(null);
    }

    private void OnAddColumnChartClicked(object sender, EventArgs e)
    {
        ViewModel?.AddColumnChartCommand.Execute(null);
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