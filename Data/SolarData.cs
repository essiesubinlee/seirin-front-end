
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
        public string Name { get; set; }
        public double Height { get; set; }
        public DateTime Time { get; set; }
        public double InverterPower { get; set; }
        public double FeedinPower { get; set; }
        public double LoadPower { get; set; }
    }

    public class ChartItem
    {
        public string Title { get; set; }
        public string Type { get; set; } // "line" or "column"
        public ObservableCollection<ChartData> Data { get; set; }
    }


    public class WeatherForecast
    {
        public string Icon { get; set; }
        public double Temperature { get; set; }

        public double Humidity { get; set; }
        public double Solar_rad {get;set;}
       
    }


    public class WeatherItem
    {
        public string Location { get; set; }
        public DateTime CurrentDate { get; set; }
        public double CurrentTemp { get; set; }
        public ObservableCollection<WeatherForecast> Forecast { get; set; }
    }


}