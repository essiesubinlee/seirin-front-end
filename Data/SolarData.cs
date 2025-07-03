
using System.Collections.ObjectModel;

namespace seirin1.Data
{
    public class SolarData
    {
        public double SolarVoltage { get; set; }
        public double SolarCurrent { get; set; }
        public DateTime Timestamp { get; set; }
    }
    /// <summary>
    /// Represents a person with a name and height.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the height of the person.
        /// </summary>	
        public double Height { get; set; }
    }
    public class PowerReading
    {
        public DateTime Time { get; set; }
        public double InverterPower { get; set; }
        public double FeedinPower { get; set; }
        public double LoadPower { get; set; }
    }


    public class ChartData
    {
        public string Title { get; set; }
        public string Type { get; set; } // Add a Type property (e.g., "LineChart", "ColumnChart")
        public ObservableCollection<ChartPoint> Data { get; set; } // Your chart data points
    }

    public class ChartPoint // Example for data points
    {
        public DateTime Time { get; set; } // For LineSeries XAxis
        public double InverterPower { get; set; } // For LineSeries YAxis

        public string Name { get; set; } // For ColumnSeries XAxis
        public double Height { get; set; } // For ColumnSeries YAxis
    }


    public class WeatherForecast
    {
        public string Icon { get; set; }
        public double Temperature { get; set; }

        public double Humidity { get; set; }
        public double Solar_rad {get;set;}
       
    }


    public class WeatherData
    {
        public string Location { get; set; }
        public DateTime CurrentDate { get; set; }
        public double CurrentTemp { get; set; }
        public ObservableCollection<WeatherForecast> Forecast { get; set; }
    }


}