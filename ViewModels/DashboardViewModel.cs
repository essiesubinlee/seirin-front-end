using seirin1.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace seirin1.ViewModels
{
    public class DashboardViewModel
    {
        public List<Person> Data { get; set; }
        public ObservableCollection<PowerReading> PowerData { get; set; }

        public string Location { get; set; }
        public DateTime CurrentDate { get; set; }
        public double CurrentTemp { get; set; }
        public string MinMaxTemp { get; set; }
        public ObservableCollection<WeatherForecast> Forecast { get; set; }




        public DashboardViewModel()
        {
            Data = new List<Person>
            {
                new Person { Name = "David", Height = 170 },
                new Person { Name = "Michael", Height = 96 },
                new Person { Name = "Steve", Height = 65 },
                new Person { Name = "Joel", Height = 182 },
                new Person { Name = "Bob", Height = 134 }
            };

            PowerData = new ObservableCollection<PowerReading>
            {
                new PowerReading { Time = DateTime.Today.AddHours(5), InverterPower = 3000, FeedinPower = -2000, LoadPower = 5000 },
                new PowerReading { Time = DateTime.Today.AddHours(6), InverterPower = 4000, FeedinPower = -1000, LoadPower = 6000 },
                new PowerReading { Time = DateTime.Today.AddHours(7), InverterPower = 8000, FeedinPower = 1000, LoadPower = 7000 },
                // ... more data points
            };

            Location = "Austin";
            CurrentDate = DateTime.Today;
            CurrentTemp = 25;
            MinMaxTemp = "15 ℃ ~ 25 ℃";

            Forecast = new ObservableCollection<WeatherForecast>
            {
                new WeatherForecast { Date = DateTime.Today.AddDays(1), Icon = "dotnet_bot.png", Temperature = 30, MinTemp = 16 },
                new WeatherForecast { Date = DateTime.Today.AddDays(2), Icon = "mousty.jpeg", Temperature = 30, MinTemp = 16 },
                new WeatherForecast { Date = DateTime.Today.AddDays(3), Icon = "dotnet_bot.png", Temperature = 29, MinTemp = 14 },
            };
        }


    
    }
}
