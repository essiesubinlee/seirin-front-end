// In Charts.xaml.cs
using seirin1.ViewModels;
namespace seirin1.Pages;
public partial class Charts : ContentPage
{
    private DashboardViewModel ViewModel => BindingContext as DashboardViewModel;

    public Charts()
    {
        InitializeComponent();
    }

    private double panX, panY;
    void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Running:
                // Translate and pan.
                double boundsX = Content.Width;
                double boundsY = Content.Height;
                Content.TranslationX = Math.Clamp(panX + e.TotalX, -boundsX, boundsX);
                Content.TranslationY = Math.Clamp(panY + e.TotalY, -boundsY, boundsY);
                break;

            case GestureStatus.Completed:
                // Store the translation applied during the pan
                panX = Content.TranslationX;
                panY = Content.TranslationY;
                break;
        }
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
    private void OnAddTempChartClicked(object sender, EventArgs e)
    {
        ViewModel?.AddTempLineChartCommand.Execute(null);
    }
    private void OnAddHumidityChartClicked(object sender, EventArgs e)
    {
        ViewModel?.AddHumidityLineChartCommand.Execute(null);
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