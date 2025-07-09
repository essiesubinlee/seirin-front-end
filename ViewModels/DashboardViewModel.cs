using seirin1.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using seirin1.ViewModels;

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
            AddLineChartCommand = new Command(async () => await AddEnergyChart());
            AddColumnChartCommand = new Command(AddColumnChart);
            AddWeatherCommand = new Command(AddWeather);
            DeleteItemCommand = new Command<object>(DeleteItem);


            // Add some initial items for testing
            AddEnergyChart();
            AddColumnChart();
            AddWeather();

        }
        //public void AddLineChart()
        //{
        //    var lineChartData = new ChartData
        //    {
        //        Title = "Inverter Power Over Time",
        //        Type = "LineChart",
        //        Data = new ObservableCollection<ChartPoint>
        //    {
        //        new ChartPoint { Time = DateTime.Now.AddHours(-3), InverterPower = 50, Power = 40 },
        //        new ChartPoint { Time = DateTime.Now.AddHours(-2), InverterPower = 70, Power = 20 },
        //        new ChartPoint { Time = DateTime.Now.AddHours(-1), InverterPower = 60, Power = 80 },
        //        new ChartPoint { Time = DateTime.Now, InverterPower = 90, Power = 20 }
        //    }
        //    };

        //    Items.Add(lineChartData);
        //}


        public async Task AddEnergyChart()
        {
            try
            {
                var energyData = await CsvReader.ReadEnergyCsvFile("data_2025-06-05.csv");

                var recentData = energyData
                    .OrderBy(e => e.Timestamp)
                    .Take(100);

                var chartData = new ChartData
                {
                    Title = "Energy",
                    Type = "LineChart",
                    Data = new ObservableCollection<ChartPoint>(
                        recentData.Select(e => new ChartPoint
                        {
                            Time = e.Timestamp,
                            SolarPower = e.SolarPower,
                            BatteryPower = e.BatteryPower,
                            LoadPower = e.LoadPower
                        }))

                };
                Items.Add(chartData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading energy data: {ex.Message}");
                // Fallback to sample data if real data fails
            }

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
