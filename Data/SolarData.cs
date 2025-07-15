
using System.Collections.ObjectModel;

namespace seirin1.Data
{
  
    public class SolarData
    {
        public double SolarVoltage { get; set; }
        public double SolarCurrent { get; set; }
        public DateTime Timestamp { get; set; }
    }

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

        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double SolarRadiation { get; set; }
    }


    public class BatteryInfo
    {
        public double SOC { get; set; }
        public double BatteryTemp { get; set; }
    }

    public abstract class ChartContainer
    {
        public string Title { get; set; }
        public string Type { get; protected set; }
    }
}