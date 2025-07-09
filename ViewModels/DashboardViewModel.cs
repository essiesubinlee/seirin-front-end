
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
        public ObservableCollection<object> ColItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> WeatherItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<string> AvailableDates { get; } = new ObservableCollection<string>();
        public ObservableCollection<TimeZoneInfo> AvailableTimeZones { get; } = new ObservableCollection<TimeZoneInfo>();

        public ICommand AddLineChartCommand { get; }
        public ICommand AddColumnChartCommand { get; }
        public ICommand AddWeatherCommand { get; }
        public ICommand DeleteItemCommand { get; }




        public DashboardViewModel()
        {

            PowerItem = new ObservableCollection<object>();
            ColItem = new ObservableCollection<object>();
            WeatherItem = new ObservableCollection<object>();

            AddLineChartCommand = new Command(async () => await AddEnergyChart());
            AddColumnChartCommand = new Command(AddColumnChart);
            AddWeatherCommand = new Command(AddWeather);
            DeleteItemCommand = new Command<object>(DeleteItem);


            // Add some initial items for testing
            AddEnergyChart();
            AddColumnChart();
            AddWeather();

        }
  


        public async Task AddEnergyChart()
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

                UpdateCharts(filteredData);
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

        private void UpdateCharts(List<EnergyData> data)
        {
            // Clear existing charts
            PowerItem.Clear();

            // Create new chart with filtered data
            var chartData = new ChartData
            {
                Title = $"Energy Data ({CombinedStart:g} - {CombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<ChartPoint>(
                    data.Select(d => new ChartPoint
                    {
                        Time = d.TimestampUTC,
                        SolarPower = d.SolarPower,
                        BatteryPower = d.BatteryPower,
                        LoadPower = d.LoadPower
                    }))
            };

            PowerItem.Add(chartData);
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
            ColItem.Add(columnChartData);
        }


        private void AddWeather()
        {
            // ... (your existing weather data creation)
            WeatherItem.Add(new WeatherData
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
            if (PowerItem.Contains(item))
            {
                PowerItem.Remove(item);
            }
            if(ColItem.Contains(item))
            {
                ColItem.Remove(item);
            }
            if(WeatherItem.Contains(item))
            {
                WeatherItem.Remove(item);
            }
        }

        
    }
}