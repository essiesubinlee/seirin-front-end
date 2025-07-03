using Microsoft.Maui.Controls;
using seirin1.Data;


namespace seirin1
{
    public class ChartWeatherTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ChartTemplate { get; set; }
        public DataTemplate WeatherTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ChartItem)
                return ChartTemplate;
            if (item is WeatherItem)
                return WeatherTemplate;

            return null;
        }
    }

}
