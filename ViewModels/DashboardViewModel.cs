
using seirin1.Data;
using seirin1.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace seirin1
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private DateTime _startDate = DateTime.UtcNow.Date.AddDays(-1);
        private TimeSpan _startTime = TimeSpan.Zero;
        private DateTime _endDate = DateTime.UtcNow.Date;
        private TimeSpan _endTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));
        private bool _isBusy;
        //private TimeZoneInfo _selectedTimeZone = TimeZoneInfo.Utc;


        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(); }
        }

        public TimeSpan StartTime
        {
            get => _startTime;
            set { _startTime = value; OnPropertyChanged(); }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged(); }
        }
        public TimeSpan EndTime
        {
            get => _endTime;
            set { _endTime = value; OnPropertyChanged(); }
        }

        public DateTime CombinedStart => StartDate.Add(StartTime);
        public DateTime CombinedEnd => EndDate.Add(EndTime);

        public ObservableCollection<object> PowerItem { get; } = new ObservableCollection<object>();

        public ObservableCollection<object> CurrentItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> VoltageItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> SolarRadItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> TempItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> HumidityItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> WeatherItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<string> AvailableDates { get; } = new ObservableCollection<string>();
        public ObservableCollection<TimeZoneInfo> AvailableTimeZones { get; } = new ObservableCollection<TimeZoneInfo>();

        public ICommand AddPowerLineChartCommand { get; }
        public ICommand AddCurrentLineChartCommand { get; }
        public ICommand AddVoltageLineChartCommand { get; }

        public ICommand AddTempLineChartCommand { get; }
        public ICommand AddHumidityLineChartCommand { get; }

        public ICommand AddSolarRadLineChartCommand { get; }

        public ICommand AddWeatherCommand { get; }
        public ICommand DeleteItemCommand { get; }




        public DashboardViewModel()
        {

            PowerItem = new ObservableCollection<object>();
            VoltageItem = new ObservableCollection<object>();
            WeatherItem = new ObservableCollection<object>();
            CurrentItem = new ObservableCollection<object>();
            SolarRadItem = new ObservableCollection<object>();
            TempItem = new ObservableCollection<object>();
            HumidityItem = new ObservableCollection<object>();

            AddPowerLineChartCommand = new Command(async () => await AddLineChart("power"));
            AddCurrentLineChartCommand = new Command(async() => await AddLineChart("current"));
            AddVoltageLineChartCommand = new Command(async () => await AddLineChart("voltage"));
            //AddColumnChartCommand = new Command(AddColumnChart);

            AddSolarRadLineChartCommand = new Command(async() => await AddLineChart("solarRad"));
            AddTempLineChartCommand = new Command(async () => await AddLineChart("temp"));
            AddHumidityLineChartCommand = new Command(async () => await AddLineChart("humidity"));

            //AddWeatherCommand = new Command(AddWeather);
            DeleteItemCommand = new Command<object>(DeleteItem);


            // Add some initial items for testing
            AddLineChart("power");
            AddLineChart("solarRad");
            //AddCurrentChart();
            //AddColumnChart();
            //AddWeather();

        }
  


        public async Task AddLineChart(string type)
        {
            try
            {
                IsBusy = true;

                var dates = GetDateRange(CombinedStart.Date, CombinedEnd.Date);
                var allData = new List<EnergyData>();
                
                foreach (var date in dates)
                {
                    var filename = $"data_{date:yyyy-MM-dd}.csv";
                    var fileData = await CsvReader.ReadEnergyCsvFile(filename);
                    allData.AddRange(fileData);
                }


                var filteredData = allData
                .Where(d => d.TimestampUTC >= CombinedStart && d.TimestampUTC <= CombinedEnd)
                .OrderBy(d => d.TimestampUTC)
                .ToList();
                if (type == "power")
                {
                    UpdatePowerCharts(filteredData);
                }
                else if(type == "current")
                {
                    UpdateCurrentCharts(filteredData);
                }
                else if (type == "voltage")
                {
                    UpdateVoltageCharts(filteredData);
                }
                else if (type == "solarRad")
                {
                    UpdateSolarRadCharts(filteredData);
                }
                else if (type == "temp")
                {
                    UpdateTempCharts(filteredData);
                }
                else if (type == "humidity")
                {
                    UpdateHumidityCharts(filteredData);
                }
         
            }
            finally
            {
                IsBusy = false;
            }


        }

       
        private IEnumerable<DateTime> GetDateRange(DateTime start, DateTime end)
        {
            for (var date = start; date <= end; date = date.AddDays(1))
                yield return date;
        }

        private void UpdatePowerCharts(List<EnergyData> data)
        {
            // Clear existing charts
            PowerItem.Clear();

            // Create new chart with filtered data
            var chartData = new ChartData
            {
                Title = $"Power Data ({CombinedStart:g} - {CombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Solar = d.SolarPower,
                        Battery = d.BatteryPower,
                        Load = d.LoadPower
                    }))
            };

            PowerItem.Add(chartData);
        }

        private void UpdateCurrentCharts(List<EnergyData> data)
        {
            // Clear existing charts
            CurrentItem.Clear();

            // Create new chart with filtered data
            var chartData = new ChartData
            {
                Title = $"Current Data ({CombinedStart:g} - {CombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Solar = d.SolarCurrent,
                        Battery = d.BatteryCurrent,
                        Load = d.LoadCurrent
                    }))
            };

            CurrentItem.Add(chartData);
        }

        private void UpdateVoltageCharts(List<EnergyData> data)
        {
            // Clear existing charts
            VoltageItem.Clear();

            // Create new chart with filtered data
            var chartData = new ChartData
            {
                Title = $"Voltage Data ({CombinedStart:g} - {CombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Solar = d.SolarVoltage,
                        Battery = d.BatteryVoltage,
                        Load = d.LoadVoltage
                    }))
            };

            VoltageItem.Add(chartData);
        }

        private void UpdateSolarRadCharts(List<EnergyData> data)
        {
            SolarRadItem.Clear();

            var chartData = new ChartData
            {
                Title = $"Solar Radiation Data ({CombinedStart:g} - {CombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Solar = d.SolarRadiation,
                        Battery = float.NaN,  // This will hide the line
                        Load = float.NaN
                    }))
            };

            SolarRadItem.Add(chartData);
        }

        private void UpdateTempCharts(List<EnergyData> data)
        {
            TempItem.Clear();

            var chartData = new ChartData
            {
                Title = $"Temperature Data ({CombinedStart:g} - {CombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Solar = d.Temperature,
                        Battery = float.NaN,  // This will hide the line
                        Load = float.NaN
                    }))
            };

            TempItem.Add(chartData);
        }

        private void UpdateHumidityCharts(List<EnergyData> data)
        {
            HumidityItem.Clear();

            var chartData = new ChartData
            {
                Title = $"Humidity Data ({CombinedStart:g} - {CombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Solar = d.Humidity,
                        Battery = float.NaN,  // This will hide the line
                        Load = float.NaN
                    }))
            };

            HumidityItem.Add(chartData);
        }




        //private void AddColumnChart()
        //{
        //    var columnChartData = new ChartData
        //    {
        //        Title = "Product Sales",
        //        Type = "ColumnChart",
        //        Data = new ObservableCollection<ChartPoint>
        //    {
        //        new ChartPoint { Name = "Product A", Height = 120 },
        //        new ChartPoint { Name = "Product B", Height = 180 },
        //        new ChartPoint { Name = "Product C", Height = 150 },
        //        new ChartPoint { Name = "Product D", Height = 90 }
        //    }
        //    };
        //    ColItem.Add(columnChartData);
        //}


        //private void AddWeather()
        //{
        //    // ... (your existing weather data creation)
        //    WeatherItem.Add(new WeatherData
        //    {
        //        Location = "Austin, TX",
        //        CurrentDate = DateTime.Now,
        //        CurrentTemp = 30,
        //        Forecast = new ObservableCollection<WeatherForecast>
        //    {
        //        new WeatherForecast { Humidity = 60, Solar_rad = 800 }
        //    }
        //    });
        //}

        private void DeleteItem(object item)
        {
            if (PowerItem.Contains(item))
            {
                PowerItem.Remove(item);
            }
            if(VoltageItem.Contains(item))
            {
                VoltageItem.Remove(item);
            }
            if(WeatherItem.Contains(item))
            {
                WeatherItem.Remove(item);
            }
            if (CurrentItem.Contains(item))
            {
                CurrentItem.Remove(item);
            }
            if (SolarRadItem.Contains(item))
            {
                SolarRadItem.Remove(item);
            }
            if (TempItem.Contains(item))
            {
                TempItem.Remove(item);
            }
            if (HumidityItem.Contains(item))
            {
                HumidityItem.Remove(item);
            }
        }

        
    }
}