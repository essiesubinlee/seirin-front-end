
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace seirin1.Data
{
  


    public class ChartData
    {
        public string Title { get; set; }
        public string Type { get; set; } // Add a Type property (e.g., "LineChart", "ColumnChart")

        public ObservableCollection<PowerPoints> Data { get; set; } // Your chart data points

       
    }



    public class PowerPoints // Example for data points
    {
        public DateTime Time { get; set; } // For LineSeries XAxis

        public double Solar { get; set; }
        public double Battery { get; set; }
        public double Load { get; set; }
        public double Weather { get; set; }


    }


    public class WeatherData
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public ObservableCollection<WeatherForecast> Data { get; set; }


    }


    public class WeatherForecast
    {
        public DateTime Time { get; set; }
        public double Forecast { get; set; }
       
    }


    
    public class EnergyData
    {
        public DateTime TimestampUTC { get; set; }
        public DateTime LocalTimestamp => TimestampUTC.ToLocalTime();
        public double SolarVoltage { get; set; }
        public double SolarCurrent { get; set; }
        public double SolarPower { get; set; }
        public double BatteryVoltage { get; set; }
        public double BatteryCurrent { get; set; }
        public double BatteryPower { get; set; }
        public double LoadVoltage { get; set; }
        public double LoadCurrent { get; set; }
        public double LoadPower { get; set; }
        public double SOC { get; set; }
        public double BatteryTemp { get; set; }

        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double SolarRadiation { get; set; }
    }

    public class ChartState : INotifyPropertyChanged
    {
        public string ChartType { get; }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(); UpdateCombined(); }
        }

        private TimeSpan _startTime;
        public TimeSpan StartTime
        {
            get => _startTime;
            set { _startTime = value; OnPropertyChanged(); UpdateCombined(); }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged(); UpdateCombined(); }
        }

        private TimeSpan _endTime;
        public TimeSpan EndTime
        {
            get => _endTime;
            set { _endTime = value; OnPropertyChanged(); UpdateCombined(); }
        }

        public DateTime CombinedStart { get; private set; }
        public DateTime CombinedEnd { get; private set; }

        public ChartState(string chartType, DateTime defaultStart)
        {
            ChartType = chartType;
            StartDate = defaultStart.Date;
            StartTime = TimeSpan.Zero;
            EndDate = DateTime.UtcNow.Date;
            EndTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));
        }

        private void UpdateCombined()
        {
            CombinedStart = StartDate.Add(StartTime);
            CombinedEnd = EndDate.Add(EndTime);
            OnPropertyChanged(nameof(CombinedStart));
            OnPropertyChanged(nameof(CombinedEnd));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


 
    public abstract class ChartContainer
    {
        public string Title { get; set; }
        public string Type { get; protected set; }
    }
}