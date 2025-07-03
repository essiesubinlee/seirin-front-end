using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using seirin1.Data;

namespace seirin1
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<object> Items { get; set; }

        public DashboardViewModel()
        {
            var sampleChartData = new ObservableCollection<ChartData>
            {
                new ChartData { Name = "A", Height = 90, Time = DateTime.Now.AddMinutes(-30), InverterPower = 5 },
                new ChartData { Name = "B", Height = 160, Time = DateTime.Now.AddMinutes(-20), InverterPower = 6 },
                new ChartData { Name = "C", Height = 100, Time = DateTime.Now.AddMinutes(-10), InverterPower = 4 }
            };


            var forecast = new ObservableCollection<WeatherForecast>
            {
                new WeatherForecast { Icon = "mousty.jpeg", Solar_rad = 80, Humidity = 70 }
            };

            Items = new ObservableCollection<object>
            {
                new ChartItem { Title = "Power Overview", Type = "line", Data = sampleChartData },
                new ChartItem { Title = "Current Comparison", Type = "column", Data = sampleChartData },
                new ChartItem { Title = "Current Comparison", Type = "column", Data = sampleChartData },
                new ChartItem { Title = "Current Comparison", Type = "column", Data = sampleChartData },
                new ChartItem { Title = "Current Comparison", Type = "column", Data = sampleChartData },
                new WeatherItem
                {
                    Location = "Austin",
                    CurrentDate = DateTime.Today,
                    CurrentTemp = 27,
                    Forecast = forecast
                }
            };
        }

        public void AddChart()
        {
            Items.Add(new ChartItem
            {
                Title = "New Chart",
                Type = "column", // or "line"
                Data = new ObservableCollection<ChartData>
                {
                    new ChartData { Name = "C", Height = 70, Time = DateTime.Now, InverterPower = 4, FeedinPower = 1, LoadPower = 3 }
                }
            });
        }

        public void AddWeather()
        {
            Items.Add(new WeatherItem
            {
                Location = "Austin",
                CurrentDate = DateTime.Today,
                CurrentTemp = 25,
                Forecast = new ObservableCollection<WeatherForecast>            
                {
                    new WeatherForecast { Icon = "sunny.png", Solar_rad = 200, Humidity = 60 }
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
