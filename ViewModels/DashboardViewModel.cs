using seirin1.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace seirin1
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<object> Items { get; set; }

        public ICommand AddLineChartCommand { get; }
        public ICommand AddColumnChartCommand { get; }
        public ICommand AddWeatherCommand { get; }
        public ICommand DeleteItemCommand { get; }


        public DashboardViewModel()
        {

            Items = new ObservableCollection<object>();
            AddLineChartCommand = new Command(AddLineChart);
            AddColumnChartCommand = new Command(AddColumnChart);
            AddWeatherCommand = new Command(AddWeather);
            DeleteItemCommand = new Command<object>(DeleteItem);

            // Add some initial items for testing
            AddLineChart();
            AddColumnChart();
            AddWeather();

        }
        public void AddLineChart()
        {
            var lineChartData = new ChartData
            {
                Title = "Inverter Power Over Time",
                Type = "LineChart",
                Data = new ObservableCollection<ChartPoint>
            {
                new ChartPoint { Time = DateTime.Now.AddHours(-3), InverterPower = 50 },
                new ChartPoint { Time = DateTime.Now.AddHours(-2), InverterPower = 70 },
                new ChartPoint { Time = DateTime.Now.AddHours(-1), InverterPower = 60 },
                new ChartPoint { Time = DateTime.Now, InverterPower = 90 }
            }
            };
            Items.Add(lineChartData);
        }

        private void AddColumnChart()
        {
            var columnChartData = new ChartData
            {
                Title = "Product Sales",
                Type = "ColumnChart",
                Data = new ObservableCollection<ChartPoint>
            {
                new ChartPoint { Name = "Product A", Height = 120 },
                new ChartPoint { Name = "Product B", Height = 180 },
                new ChartPoint { Name = "Product C", Height = 150 },
                new ChartPoint { Name = "Product D", Height = 90 }
            }
            };
            Items.Add(columnChartData);
        }


        private void AddWeather()
        {
            // ... (your existing weather data creation)
            Items.Add(new WeatherData
            {
                Location = "Austin, TX",
                CurrentDate = DateTime.Now,
                CurrentTemp = 30,
                Forecast = new ObservableCollection<WeatherForecast>
            {
                new WeatherForecast { Humidity = 60, Solar_rad = 800 }
            }
            });
        }

        private void DeleteItem(object item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
