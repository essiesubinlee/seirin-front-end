
using seirin1.Controls;
using seirin1.Data;
using seirin1.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace seirin1.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private bool _isBusy;

        private DateTime _startDate = DateTime.UtcNow.Date;
        private DateTime _voltageStartDate = DateTime.UtcNow.Date; // Or any other default
        private DateTime _powerStartDate = DateTime.UtcNow.Date;
        private DateTime _currentStartDate = DateTime.UtcNow.Date;
        private DateTime _solarRadStartDate = DateTime.UtcNow.Date;
        private DateTime _tempStartDate = DateTime.UtcNow.Date;
        private DateTime _humidityStartDate = DateTime.UtcNow.Date;
        private DateTime _batterySOCStartDate = DateTime.UtcNow.Date;
        private DateTime _batteryTempStartDate = DateTime.UtcNow.Date;

        private TimeSpan _startTime = TimeSpan.Zero;
        private TimeSpan _voltageStartTime = TimeSpan.Zero; // Or any other default
        private TimeSpan _powerStartTime = TimeSpan.Zero;
        private TimeSpan _currentStartTime = TimeSpan.Zero;
        private TimeSpan _solarRadStartTime = TimeSpan.Zero;
        private TimeSpan _tempStartTime = TimeSpan.Zero;
        private TimeSpan _humidityStartTime = TimeSpan.Zero;
        private TimeSpan _batterySOCStartTime = TimeSpan.Zero;
        private TimeSpan _batteryTempStartTime = TimeSpan.Zero;


        private DateTime _endDate = DateTime.UtcNow.Date;
        private DateTime _voltageEndDate = DateTime.UtcNow.Date; // Or any other default
        private DateTime _powerEndDate = DateTime.UtcNow.Date;
        private DateTime _currentEndDate = DateTime.UtcNow.Date;
        private DateTime _solarRadEndDate = DateTime.UtcNow.Date;
        private DateTime _tempEndDate = DateTime.UtcNow.Date;
        private DateTime _humidityEndDate = DateTime.UtcNow.Date;
        private DateTime _batterySOCEndDate = DateTime.UtcNow.Date;
        private DateTime _batteryTempEndDate = DateTime.UtcNow.Date;




        private TimeSpan _endTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));
        private TimeSpan _voltageEndTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));// Or any other default
        private TimeSpan _powerEndTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));
        private TimeSpan _currentEndTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));
        private TimeSpan _solarRadEndTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));
        private TimeSpan _tempEndTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));
        private TimeSpan _humidityEndTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));
        private TimeSpan _batterySOCEndTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));
        private TimeSpan _batteryTempEndTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));

        //private TimeZoneInfo _selectedTimeZone = TimeZoneInfo.Utc;


        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public DateTime StartDate { get => _startDate; set { _startDate = value; OnPropertyChanged(); } }
        public DateTime PowerStartDate
        {
            get => _powerStartDate;
            set
            {
                if (_powerStartDate != value)
                {
                    _powerStartDate = value;
                    OnPropertyChanged(); // This uses [CallerMemberName] to get "PowerStartTime"
                    Debug.WriteLine($"[DEBUG-Setter] PowerStartDate updated to: {_powerStartDate}");
                    Debug.WriteLine($"[DEBUG-Setter] Val updated to: {value}");


                    // Also notify combined property if it's derived
                    OnPropertyChanged(nameof(PowerCombinedStart));
                    Debug.WriteLine($"[DEBUG-Setter] Notified PowerCombinedStart for PowerStartTime change.");
                }

            }
        }
        public DateTime VoltageStartDate { 
            get => _voltageStartDate; 
            set {
                if (_voltageStartDate != value)
                {
                    _voltageStartDate = value;
                    OnPropertyChanged(); // This uses [CallerMemberName] to get "PowerStartTime"
                    Debug.WriteLine($"[DEBUG-Setter] VoltageStartDate updated to: {_voltageStartDate}");
                    Debug.WriteLine($"[DEBUG-Setter] Val updated to: {value}");


                    // Also notify combined property if it's derived
                    OnPropertyChanged(nameof(VoltageCombinedStart));
                    Debug.WriteLine($"[DEBUG-Setter] Notified VoltageCombinedStart for PowerStartTime change.");
                }
                
            } }
        public DateTime CurrentStartDate
        {
            get => _currentStartDate;
            set
            {
                if (_currentStartDate != value)
                {
                    _currentStartDate = value;
                    OnPropertyChanged(); // This uses [CallerMemberName] to get "PowerStartTime"
                    Debug.WriteLine($"[DEBUG-Setter] CurrentStartDate updated to: {_currentStartDate}");
                    Debug.WriteLine($"[DEBUG-Setter] Val updated to: {value}");


                    // Also notify combined property if it's derived
                    OnPropertyChanged(nameof(CurrentCombinedStart));
                    Debug.WriteLine($"[DEBUG-Setter] Notified CurrentCombinedStart for CurrentStartTime change.");
                }

            }
        }

         public DateTime SolarRadStartDate { 
            get => _solarRadStartDate; 
            set {
                if (_solarRadStartDate != value)
                {
                    _solarRadStartDate = value;
                    OnPropertyChanged(); // This uses [CallerMemberName] to get "PowerStartTime"
                    Debug.WriteLine($"[DEBUG-Setter] solarRadStartDate updated to: {_solarRadStartDate}");
                    Debug.WriteLine($"[DEBUG-Setter] Val updated to: {value}");


                    // Also notify combined property if it's derived
                    OnPropertyChanged(nameof(SolarRadCombinedStart));
                    Debug.WriteLine($"[DEBUG-Setter] Notified SolarRadCombinedStart for SolarRadStartTime change.");
                }
                
            } }
        public DateTime TempStartDate
        {
            get => _tempStartDate;
            set
            {
                if (_tempStartDate != value)
                {
                    _tempStartDate = value;
                    OnPropertyChanged(); // This uses [CallerMemberName] to get "PowerStartTime"
                    Debug.WriteLine($"[DEBUG-Setter] TempStartDate updated to: {_tempStartDate}");
                    Debug.WriteLine($"[DEBUG-Setter] Val updated to: {value}");


                    // Also notify combined property if it's derived
                    OnPropertyChanged(nameof(TempCombinedStart));
                    Debug.WriteLine($"[DEBUG-Setter] Notified TempCombinedStart for TempStartTime change.");
                }

            }
        }
         public DateTime HumidityStartDate { 
            get => _humidityStartDate; 
            set {
                if (_humidityStartDate != value)
                {
                    _humidityStartDate = value;
                    OnPropertyChanged(); // This uses [CallerMemberName] to get "PowerStartTime"
                    Debug.WriteLine($"[DEBUG-Setter] HumidityStartDate updated to: {_humidityStartDate}");
                    Debug.WriteLine($"[DEBUG-Setter] Val updated to: {value}");


                    // Also notify combined property if it's derived
                    OnPropertyChanged(nameof(HumidityCombinedStart));
                    Debug.WriteLine($"[DEBUG-Setter] Notified HumidityCombinedStart for HumidityStartTime change.");
                }
                
            } }
         public DateTime BatterySOCStartDate { 
            get => _batterySOCStartDate; 
            set {
                if (_batterySOCStartDate != value)
                {
                    _batterySOCStartDate = value;
                    OnPropertyChanged(); // This uses [CallerMemberName] to get "PowerStartTime"
                    Debug.WriteLine($"[DEBUG-Setter] BatterySOCStartDate updated to: {_batterySOCStartDate}");
                    Debug.WriteLine($"[DEBUG-Setter] Val updated to: {value}");


                    // Also notify combined property if it's derived
                    OnPropertyChanged(nameof(BatterySOCCombinedStart));
                    Debug.WriteLine($"[DEBUG-Setter] Notified SOCCombinedStart for SOCStartTime change.");
                }
                
            } }

        public DateTime BatteryTempStartDate
        {
            get => _batteryTempStartDate;
            set
            {
                if (_batteryTempStartDate != value)
                {
                    _batteryTempStartDate = value;
                    OnPropertyChanged(); // This uses [CallerMemberName] to get "PowerStartTime"
                    Debug.WriteLine($"[DEBUG-Setter] PowerStartDate updated to: {_batteryTempStartDate}");
                    Debug.WriteLine($"[DEBUG-Setter] Val updated to: {value}");


                    // Also notify combined property if it's derived
                    OnPropertyChanged(nameof(BatteryTempCombinedStart));
                    Debug.WriteLine($"[DEBUG-Setter] Notified Battery Temp CombinedStart for BatteryTempStartTime change.");
                }

            }
        }
        public TimeSpan StartTime { get => _startTime; set { _startTime = value; OnPropertyChanged();} }
        public TimeSpan VoltageStartTime{ get => _voltageStartTime; set { _voltageStartTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(VoltageCombinedStart)); } }
        public TimeSpan PowerStartTime { get => _powerStartTime; set { _powerStartTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(PowerCombinedStart)); } }
        public TimeSpan CurrentStartTime { get => _currentStartTime; set { _currentStartTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(CurrentCombinedStart)); } }
        public TimeSpan SolarRadStartTime { get => _solarRadStartTime; set { _solarRadStartTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(SolarRadCombinedStart)); } }
        public TimeSpan TempStartTime { get => _tempStartTime; set { _tempStartTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(TempCombinedStart)); } }
        public TimeSpan HumidityStartTime { get => _humidityStartTime; set { _humidityStartTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(HumidityCombinedStart)); } }
        public TimeSpan BatterySOCStartTime { get => _batterySOCStartTime; set { _batterySOCStartTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(BatterySOCCombinedStart)); } }
        public TimeSpan BatteryTempStartTime { get => _batteryTempStartTime; set { _batteryTempStartTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(BatteryTempCombinedStart)); } }


        public DateTime EndDate { get => _endDate; set { _endDate = value; OnPropertyChanged();  } }
        public DateTime VoltageEndDate{ get => _voltageEndDate; set { _voltageEndDate = value; OnPropertyChanged(); OnPropertyChanged(nameof(VoltageCombinedEnd)); } }
        public DateTime PowerEndDate { get => _powerEndDate; 
            set { _powerEndDate = value; OnPropertyChanged();
                Debug.WriteLine($"PowerEndDate SETTER: {value:g}"); 
                OnPropertyChanged(nameof(PowerCombinedEnd)); } }
        public DateTime CurrentEndDate { get => _currentEndDate; set { _currentEndDate = value; OnPropertyChanged(); OnPropertyChanged(nameof(CurrentCombinedEnd)); } }
        public DateTime SolarRadEndDate { get => _solarRadEndDate; set { _solarRadEndDate = value; OnPropertyChanged(); OnPropertyChanged(nameof(SolarRadCombinedEnd)); } }
        public DateTime TempEndDate { get => _tempEndDate; set { _tempEndDate = value; OnPropertyChanged(); OnPropertyChanged(nameof(TempCombinedEnd)); } }
        public DateTime HumidityEndDate { get => _humidityEndDate; set { _humidityEndDate = value; OnPropertyChanged(); OnPropertyChanged(nameof(HumidityCombinedEnd)); } }
        public DateTime BatterySOCEndDate { get => _batterySOCEndDate; set { _batterySOCEndDate = value; OnPropertyChanged(); OnPropertyChanged(nameof(BatterySOCCombinedEnd)); } }
        public DateTime BatteryTempEndDate { get => _batteryTempEndDate; set { _batteryTempEndDate = value; OnPropertyChanged(); OnPropertyChanged(nameof(BatteryTempCombinedEnd)); } }

        public TimeSpan EndTime { get => _endTime; set { _endTime = value; OnPropertyChanged(); } }
        public TimeSpan VoltageEndTime{ get => _voltageEndTime; set { _voltageEndTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(VoltageCombinedEnd)); } }       
        public TimeSpan PowerEndTime{ get => _powerEndTime; set { _powerEndTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(PowerCombinedEnd)); } }
        public TimeSpan CurrentEndTime { get => _currentEndTime; set { _currentEndTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(CurrentCombinedEnd)); } }
        public TimeSpan SolarRadEndTime { get => _solarRadEndTime; set { _solarRadEndTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(SolarRadCombinedEnd)); } }
        public TimeSpan TempEndTime { get => _tempEndTime; set { _tempEndTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(TempCombinedEnd)); } }
        public TimeSpan HumidityEndTime { get => _humidityEndTime; set { _humidityEndTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(HumidityCombinedEnd)); } }
        public TimeSpan BatterySOCEndTime { get => _batterySOCEndTime; set { _batterySOCEndTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(BatterySOCCombinedEnd)); } }
        public TimeSpan BatteryTempEndTime { get => _batteryTempEndTime; set { _batteryTempEndTime = value; OnPropertyChanged(); OnPropertyChanged(nameof(BatteryTempCombinedEnd)); } }


        //public DateTime CombinedStart => StartDate.Add(StartTime);
        //public DateTime CombinedEnd => EndDate.Add(EndTime);

        public DateTime VoltageCombinedStart => VoltageStartDate.Add(VoltageStartTime);
        public DateTime VoltageCombinedEnd => VoltageEndDate.Add(VoltageEndTime);

        public DateTime PowerCombinedStart => PowerStartDate.Add(PowerStartTime);
        public DateTime PowerCombinedEnd => PowerEndDate.Add(PowerEndTime);

        public DateTime CurrentCombinedStart => CurrentStartDate.Add(CurrentStartTime);
        public DateTime CurrentCombinedEnd => CurrentEndDate.Add(CurrentEndTime);

        public DateTime SolarRadCombinedStart => SolarRadStartDate.Add(SolarRadStartTime);
        public DateTime SolarRadCombinedEnd => SolarRadEndDate.Add(SolarRadEndTime);

        public DateTime TempCombinedStart => TempStartDate.Add(TempStartTime);
        public DateTime TempCombinedEnd => TempEndDate.Add(TempEndTime);

        public DateTime HumidityCombinedStart => HumidityStartDate.Add(HumidityStartTime);
        public DateTime HumidityCombinedEnd => HumidityEndDate.Add(HumidityEndTime);

        public DateTime BatterySOCCombinedStart => BatterySOCStartDate.Add(BatterySOCStartTime);
        public DateTime BatterySOCCombinedEnd => BatterySOCEndDate.Add(BatterySOCEndTime);

        public DateTime BatteryTempCombinedStart => BatteryTempStartDate.Add(BatteryTempStartTime);
        public DateTime BatteryTempCombinedEnd => BatteryTempEndDate.Add(BatteryTempEndTime);

        public ObservableCollection<object> PowerItem { get; } = new ObservableCollection<object>();

        public ObservableCollection<object> CurrentItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> VoltageItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> SolarRadItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> TempItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> HumidityItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> WeatherItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> BatterySOCItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<object> BatteryTempItem { get; } = new ObservableCollection<object>();
        public ObservableCollection<string> AvailableDates { get; } = new ObservableCollection<string>();
        public ObservableCollection<TimeZoneInfo> AvailableTimeZones { get; } = new ObservableCollection<TimeZoneInfo>();

  

        public ICommand AddPowerLineChartCommand { get; }
        public ICommand AddCurrentLineChartCommand { get; }
        public ICommand AddVoltageLineChartCommand { get; }

        public ICommand AddTempLineChartCommand { get; }
        public ICommand AddHumidityLineChartCommand { get; }

        public ICommand AddSolarRadLineChartCommand { get; }

        public ICommand AddBatterySOCLineChartCommand { get; }
        public ICommand AddBatteryTempLineChartCommand { get; }

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

           

            AddPowerLineChartCommand = new Command(async () => await AddLineChart("power", PowerCombinedStart, PowerCombinedEnd));
            AddCurrentLineChartCommand = new Command(async () => await AddLineChart("current", CurrentCombinedStart, CurrentCombinedEnd));
            AddVoltageLineChartCommand = new Command(async () => await AddLineChart("voltage", VoltageCombinedStart, VoltageCombinedEnd));
            //AddColumnChartCommand = new Command(AddColumnChart);

            AddSolarRadLineChartCommand = new Command(async () => await AddLineChart("solarRad", SolarRadCombinedStart, SolarRadCombinedEnd));
            AddTempLineChartCommand = new Command(async () => await AddLineChart("temp", TempCombinedStart, TempCombinedEnd));
            AddHumidityLineChartCommand = new Command(async () => await AddLineChart("humidity", HumidityCombinedStart, HumidityCombinedEnd));
            AddBatterySOCLineChartCommand = new Command(async () => await AddLineChart("SOC", BatterySOCCombinedStart, BatterySOCCombinedEnd));
            AddBatteryTempLineChartCommand = new Command(async () => await AddLineChart("batteryTemp", BatteryTempCombinedStart, BatteryTempCombinedEnd));

            //AddWeatherCommand = new Command(AddWeather);
            DeleteItemCommand = new Command<object>(DeleteItem);

            var todayStart = DateTime.UtcNow.Date; // 00:00:00 today
            var todayEnd = DateTime.UtcNow.Date.AddHours(23).AddMinutes(59); // 23:59 today

            // Update all start/end dates
            _startDate = todayStart;
            _endDate = todayEnd;

            _powerStartDate = todayStart;
            _powerEndDate = todayEnd;

            _voltageStartDate = todayStart;
            _voltageEndDate = todayEnd;


            // Initial to show when the window first pops up
            AddLineChart("power", _powerStartDate, _powerEndDate);

            AddLineChart("voltage", _voltageStartDate, _voltageEndDate);
            AddLineChart("current", CurrentCombinedStart, CurrentCombinedEnd);
            
            AddLineChart("solarRad", SolarRadCombinedStart, SolarRadCombinedEnd);
            AddLineChart("humidity", HumidityCombinedStart, HumidityCombinedEnd);
            AddLineChart("temp", TempCombinedStart, TempCombinedEnd);
            AddLineChart("SOC", BatterySOCCombinedStart, BatterySOCCombinedEnd);
            AddLineChart("batteryTemp", BatteryTempCombinedStart, BatteryTempCombinedEnd);
            //AddCurrentChart();
            //AddColumnChart();
            //AddWeather();

        }

        public async Task RefreshChart(string chartType)
        {
            Debug.WriteLine("This is a debug message");

            try
            {
                IsBusy = true;
                Debug.WriteLine($"Before refresh - PowerStartDate: {PowerStartDate:g}, PowerEndDate: {PowerEndDate:g}");


                var defaultStart = DateTime.UtcNow.Date.Add(TimeSpan.Zero);

                var defaultEnd = DateTime.UtcNow.Date.Add(TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59)));
                Debug.WriteLine($"default: {defaultStart:g}, PowerEndDate: {defaultEnd:g}");
                Debug.WriteLine($"start: {_startDate:g}, PowerEndDate: {_endDate:g}");

                // Reset dates for this chart type
                Debug.WriteLine("Refreshchart");
                Debug.WriteLine($"window type: {chartType}");

                switch (chartType)
                {
                    
                    case "power":
                        _powerStartDate = defaultStart;
                        _powerEndDate = defaultEnd;
                        PowerStartDate = defaultStart; 
                        PowerEndDate = defaultEnd;
         
                        OnPropertyChanged(nameof(PowerStartDate));
                        OnPropertyChanged(nameof(PowerEndDate));

                        await Task.Delay(50);
                        await AddLineChart(chartType, PowerCombinedStart, PowerCombinedEnd);
                       
                        Debug.WriteLine($"after refresh - _powerStartDate: {_powerStartDate:g}, PowerEndDate: {_powerEndDate:g}");
                        Debug.WriteLine($"after refresh - PowerStartDate: {PowerStartDate:g}, PowerEndDate: {PowerEndDate:g}");

                        break;

                    case "voltage":
                        _voltageStartDate = defaultStart;
                        _voltageEndDate = defaultEnd;
                        VoltageStartDate = defaultStart;
                        VoltageEndDate = defaultEnd;

                        OnPropertyChanged(nameof(VoltageStartDate));
                        OnPropertyChanged(nameof(VoltageEndDate));

                        await Task.Delay(50);
                        await AddLineChart(chartType, VoltageCombinedStart, VoltageCombinedEnd);
                        Debug.WriteLine($"after refresh - default: {defaultStart:g}, PowerEndDate: {defaultEnd:g}");
                        Debug.WriteLine($"after refresh - _voltageStartDate: {_voltageStartDate:g}, PowerEndDate: {_voltageEndDate:g}");
                        Debug.WriteLine($"after refresh - voltageStartDate: {VoltageStartDate:g}, PowerEndDate: {VoltageEndDate:g}");

                        break;

                    case "current":
                        _currentStartDate = defaultStart;
                        _currentEndDate = defaultEnd.Date;
                        CurrentStartDate = defaultStart;
                        CurrentEndDate = defaultEnd.Date;

                        OnPropertyChanged(nameof(CurrentStartDate));
                        OnPropertyChanged(nameof(CurrentEndDate));

                        await Task.Delay(50);
                        await AddLineChart(chartType, CurrentCombinedStart, CurrentCombinedEnd);
                        break;

                    case "solarRad":
                        _solarRadStartDate = defaultStart;
                        _solarRadEndDate = defaultEnd.Date;
                        SolarRadStartDate = defaultStart;
                        SolarRadEndDate = defaultEnd.Date;

                        OnPropertyChanged(nameof(SolarRadStartDate));
                        OnPropertyChanged(nameof(SolarRadEndDate));

                        await Task.Delay(50);
                        await AddLineChart(chartType, SolarRadCombinedStart, SolarRadCombinedEnd);
                        break;

                    case "humidity":
                        _humidityStartDate = defaultStart;
                        _humidityEndDate = defaultEnd.Date;
                        HumidityStartDate = defaultStart;
                        HumidityEndDate = defaultEnd.Date;

                        OnPropertyChanged(nameof(HumidityStartDate));
                        OnPropertyChanged(nameof(HumidityEndDate));

                        await Task.Delay(50);
                        await AddLineChart(chartType, HumidityCombinedStart, HumidityCombinedEnd);
                        break;

                    case "temp":
                        _tempStartDate = defaultStart;
                        _tempEndDate = defaultEnd.Date;
                        TempStartDate = defaultStart;
                        TempEndDate = defaultEnd.Date;

                        OnPropertyChanged(nameof(TempStartDate));
                        OnPropertyChanged(nameof(TempEndDate));

                        await Task.Delay(50);
                        await AddLineChart(chartType, TempCombinedStart, TempCombinedEnd);
                        break;

                    case "SOC":
                        _batterySOCStartDate = defaultStart;
                        _batterySOCEndDate = defaultEnd.Date;
                        BatterySOCStartDate = defaultStart;
                        BatterySOCEndDate = defaultEnd.Date;

                        OnPropertyChanged(nameof(BatterySOCStartDate));
                        OnPropertyChanged(nameof(BatterySOCEndDate));

                        await Task.Delay(50);
                        Debug.WriteLine($"after refresh - default: {defaultStart:g}, PowerEndDate: {defaultEnd:g}");
                        Debug.WriteLine($"after refresh - _voltageStartDate: {_batterySOCStartDate:g}, PowerEndDate: {_batterySOCEndDate:g}");
                        Debug.WriteLine($"after refresh - voltageStartDate: {BatterySOCStartDate:g}, PowerEndDate: {BatterySOCEndDate:g} \n");


                        await AddLineChart(chartType, BatterySOCCombinedStart, BatterySOCCombinedEnd);
                        break;

                    case "batteryTemp":
                        _batteryTempStartDate = defaultStart;
                        _batteryTempEndDate = defaultEnd.Date;
                        BatteryTempStartDate = defaultStart;
                        BatteryTempEndDate = defaultEnd.Date;

                        OnPropertyChanged(nameof(BatteryTempStartDate));
                        OnPropertyChanged(nameof(BatteryTempEndDate));

                        await Task.Delay(50);
                        await AddLineChart(chartType, BatteryTempCombinedStart, BatteryTempCombinedEnd);
                        break;


                        // Add cases for all chart types...
                }

                // Now refresh the chart data
                
            }
            finally
            {
                IsBusy = false;
            }
        }



        public async Task AddLineChart(string type, DateTime start, DateTime end)
        {
            try
            {
                IsBusy = true;

                var dates = GetDateRange(start.Date, end.Date);
                var allData = new List<EnergyData>();

                foreach (var date in dates)
                {
                    var filename = $"data_{date:yyyy-MM-dd}.csv";
                    var fileData = await CsvReader.ReadEnergyCsvFile(filename);
                    allData.AddRange(fileData);
                }

                Debug.WriteLine("inside addline \n");

                var filteredData = allData
                .Where(d => d.TimestampUTC >= start && d.TimestampUTC <= end)
                .OrderBy(d => d.TimestampUTC)
                .ToList();
                if (type == "power")
                {
                    UpdatePowerCharts(filteredData);
   
                }
                else if (type == "current")
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
                else if (type == "SOC")
                {
                    UpdateSOCCharts(filteredData);
                }

                else if (type == "batteryTemp")
                {
                    UpdateBatteryTempCharts(filteredData);
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
                Title = $"Power Data ({PowerCombinedStart:g} - {PowerCombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Solar = d.SolarPower,
                        Battery = d.BatteryPower,              
                        Load = d.LoadPower,
                        Weather = float.NaN
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
                Title = $"Current Data ({CurrentCombinedStart:g} - {CurrentCombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Solar = d.SolarCurrent,
                        Battery = d.BatteryCurrent,
                        Load = d.LoadCurrent,
                        Weather = float.NaN
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
                Title = $"Voltage Data ({VoltageCombinedStart:g} - {VoltageCombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Solar = d.SolarVoltage,
                        Battery = d.BatteryVoltage,
                        Load = d.LoadVoltage,
                        Weather = float.NaN
                    }))
            };

            VoltageItem.Add(chartData);
        }

        private void UpdateSolarRadCharts(List<EnergyData> data)
        {
            SolarRadItem.Clear();

            var chartData = new ChartData
            {
                Title = $"Solar Radiation Data ({SolarRadCombinedStart:g} - {SolarRadCombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Weather = d.SolarRadiation,
                        Solar = float.NaN,
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
                Title = $"Temperature Data ({TempCombinedStart:g} - {TempCombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Weather = d.Temperature,
                        Solar = float.NaN,
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
                Title = $"Humidity Data ({HumidityCombinedStart:g} - {HumidityCombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Weather = d.Humidity,
                        Solar = float.NaN,
                        Battery = float.NaN,  // This will hide the line
                        Load = float.NaN
                    }))
            };

            HumidityItem.Add(chartData);
        }

        private void UpdateSOCCharts(List<EnergyData> data)
        {
            BatterySOCItem.Clear();

            var chartData = new ChartData
            {
                Title = $"Battery SOC Data ({BatterySOCCombinedStart:g} - {BatterySOCCombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Weather = float.NaN,
                        Solar = float.NaN,
                        Battery = d.SOC,  // This will hide the line
                        Load = float.NaN
                    }))
            };

            BatterySOCItem.Add(chartData);
        }

        private void UpdateBatteryTempCharts(List<EnergyData> data)
        {
            BatteryTempItem.Clear();

            var chartData = new ChartData
            {
                Title = $"Battery Temperature Data ({BatteryTempCombinedStart:g} - {BatteryTempCombinedEnd:g})",
                Type = "LineChart",
                Data = new ObservableCollection<PowerPoints>(
                    data.Select(d => new PowerPoints
                    {
                        Time = d.TimestampUTC,
                        Weather = float.NaN,
                        Solar = float.NaN,
                        Battery = d.BatteryTemp,  // This will hide the line
                        Load = float.NaN
                    }))
            };

            BatteryTempItem.Add(chartData);
        }



        private void DeleteItem(object item)
        {
            if (PowerItem.Contains(item))
            {
                PowerItem.Remove(item);
            }
            if (VoltageItem.Contains(item))
            {
                VoltageItem.Remove(item);
            }
            if (WeatherItem.Contains(item))
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