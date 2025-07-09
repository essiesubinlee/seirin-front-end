using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using seirin1; // Access CsvReaderService
using seirin1.Data; // Access EnergyData
using seirin1.ViewModels;

namespace seirin1.tester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string csvFileName = "data_2025-06-05.csv";

            List<EnergyData> parsedData = await CsvReader.ReadEnergyCsvFile(csvFileName);

            if (parsedData.Any())
            {
                Console.WriteLine($"Successfully parsed {parsedData.Count} data rows.");
                Console.WriteLine("\n--- First 5 rows ---");
                foreach (var data in parsedData.Take(5)) // Print first 5 rows
                {
                    Console.WriteLine($"Timestamp: {data.TimestampUTC:yyyy-MM-dd HH:mm:ss}, " +
                                      $"Solar Power: {data.SolarPower:F2}W, " +
                                      $"Load Power: {data.LoadPower:F2}W, " +
                                      $"Battery Power: {data.BatteryPower:F2}V");
                }
            }
            else
            {
                Console.WriteLine("No data was parsed. Check for errors above.");
            }

            Console.WriteLine("\nCSV parsing test finished. Press any key to exit.");
            Console.ReadKey(); // Keep console open until a key is pressed
        }
    }
}
