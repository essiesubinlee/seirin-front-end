using System;
using System.Collections.Generic;
using System.Globalization; // Required for CultureInfo.InvariantCulture
using System.IO;          // Required for StreamReader
using System.Linq;        // Required for .Any()
using System.Text;        // Required for Encoding.UTF8
using System.Threading.Tasks; // Required for async operations
using Microsoft.Maui.Storage; // Required for FileSystem.OpenAppPackageFileAsync
using seirin1.Data;

namespace seirin1.ViewModels
{
    public static class CsvReader
    {
        public static async Task<List<EnergyData>> ReadEnergyCsvFile(string fileName)
        {
            var energyDataList = new List<EnergyData>();

            try
            {
                //open the csv file and reading characters from encoding
                using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                using var reader = new StreamReader(stream, Encoding.UTF8);

                string header = await reader.ReadLineAsync();
                if (header == null)
                {
                    Console.WriteLine("CSV file doens't have data");
                    return energyDataList;
                }

                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var values = line.Split(',');
                    if (values.Length > 12)
                    {
                        if (DateTimeOffset.TryParse(values[0], CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTimeOffset timestamp) &&
                            Double.TryParse(values[1], CultureInfo.InvariantCulture, out double solarVoltage) &&
                            Double.TryParse(values[2], CultureInfo.InvariantCulture, out double solarCurrent) &&
                            Double.TryParse(values[3], CultureInfo.InvariantCulture, out double batteryVoltage) &&
                            Double.TryParse(values[4], CultureInfo.InvariantCulture, out double batteryCurrent) &&
                            Double.TryParse(values[5], CultureInfo.InvariantCulture, out double loadVoltage) &&
                            Double.TryParse(values[6], CultureInfo.InvariantCulture, out double loadCurrent) &&
                            Double.TryParse(values[7], CultureInfo.InvariantCulture, out double batteryTemp) &&
                            Double.TryParse(values[8], CultureInfo.InvariantCulture, out double batterySOC) &&
                            Double.TryParse(values[9], CultureInfo.InvariantCulture, out double solarPower) &&
                            Double.TryParse(values[10], CultureInfo.InvariantCulture, out double batteryPower) &&
                            Double.TryParse(values[11], CultureInfo.InvariantCulture, out double loadPower) &&
                            Double.TryParse(values[12], CultureInfo.InvariantCulture, out double accuPrecip) &&
                            Double.TryParse(values[13], CultureInfo.InvariantCulture, out double dewPoint) &&
                            Double.TryParse(values[14], CultureInfo.InvariantCulture, out double feelsLike) &&
                            Double.TryParse(values[15], CultureInfo.InvariantCulture, out double humidity) &&
                            Double.TryParse(values[16], CultureInfo.InvariantCulture, out double precip) &&
                            Double.TryParse(values[17], CultureInfo.InvariantCulture, out double pressure) &&
                            Double.TryParse(values[18], CultureInfo.InvariantCulture, out double skyCover) &&
                            Double.TryParse(values[19], CultureInfo.InvariantCulture, out double solarRad) &&
                            Double.TryParse(values[20], CultureInfo.InvariantCulture, out double temp) &&
                            Double.TryParse(values[21], CultureInfo.InvariantCulture, out double wind) &&
                            Double.TryParse(values[22], CultureInfo.InvariantCulture, out double windDirection) &&
                            Double.TryParse(values[23], CultureInfo.InvariantCulture, out double windGusts)
                            )
                        {
                            energyDataList.Add(new EnergyData
                            {
                                Timestamp = timestamp.UtcDateTime,
                                SolarPower = solarPower,
                                SolarCurrent = solarCurrent,
                                SolarVoltage = solarVoltage,
                                LoadCurrent = loadCurrent,
                                LoadVoltage = loadVoltage,
                                LoadPower = loadPower,
                                BatteryCurrent = batteryCurrent,
                                BatteryPower = batteryPower,
                                BatteryVoltage = batteryVoltage
                            });
                        }
                        else
                        {
                            Console.WriteLine($"Skipping malformed data line in '{fileName}' : {line}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Skipping wrong count column line in '{fileName}' : {line}");
                    }


                }


            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File '{fileName}' Not Found");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading file '{fileName}' : {ex.Message}");
            }

            return energyDataList;
        }
    }
    


}