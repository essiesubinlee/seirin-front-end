using Microsoft.Maui.Controls;
using seirin1.Data;


namespace seirin1
{
    // In your ChartWeatherTemplateSelector.cs
    public class ChartWeatherTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ChartTemplate { get; set; } // This will now be a generic chart template
        public DataTemplate WeatherTemplate { get; set; }
        public DataTemplate LineChartTemplate { get; set; } // New template for Line charts
        public DataTemplate ColumnChartTemplate { get; set; } // New template for Column charts

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ChartData chartData) // Assuming ChartData is your model for chart items
            {
                if (chartData.Type == "LineChart") // You'll need to define these types in your ChartData
                {
                    return LineChartTemplate;
                }
                else if (chartData.Type == "ColumnChart")
                {
                    return ColumnChartTemplate;
                }
                // Fallback or other chart types
                return ChartTemplate;
            }
            else if (item is WeatherData) // Assuming WeatherData is your model for weather items
            {
                return WeatherTemplate;
            }
            return null;
        }
    }
}