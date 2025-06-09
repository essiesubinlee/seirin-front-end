using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace seirin1.Services
{
    public class SolarData
    {
        public double SolarVoltage { get; set; }
        public double SolarCurrent { get; set; }
        public double LoadVoltage { get; set; }
        public double LoadCurrent { get; set; }
        public double BatteryVoltage { get; set; }
        public double BatteryCurrent { get; set; }
        public double SolarPower { get; set; }
        public double LoadPower { get; set; }
        public double BatteryPower { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class FirebaseService
    {
        private readonly HttpClient _client;
        private const string ProjectId = "vocuslabsserver"; // <- CHANGE THIS
        private const string Collection = "data_log_seirin";
        private List<SolarData>? _cachedData;
        private DateTime _lastFetchTime = DateTime.MinValue;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        public FirebaseService()
        {
            _client = new HttpClient();

        }
    
        public async Task<List<SolarData>> FetchWithCacheAsync()
        {
            // Check if cache is still valid
            if (_cachedData != null && (DateTime.UtcNow - _lastFetchTime) < _cacheDuration)
            {
                return _cachedData;
            }

            // Refresh cache
            _cachedData = await FetchLatest30Async();
            _lastFetchTime = DateTime.UtcNow;

            return _cachedData;
        }


        public async Task<List<SolarData>> FetchLatest30Async()
        {
            var url = $"https://firestore.googleapis.com/v1/projects/{ProjectId}/databases/(default)/documents/{Collection}?pageSize=30";

            var response = await _client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP Error: {response.StatusCode}");
                System.Diagnostics.Debug.WriteLine(json);
                throw new Exception("Could not load data from Firebase.");
            }

            System.Diagnostics.Debug.WriteLine("RAW FIREBASE RESPONSE:");
            System.Diagnostics.Debug.WriteLine(json);

            var dataList = new List<SolarData>();

            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                if (!doc.RootElement.TryGetProperty("documents", out var documents)) return dataList;

                foreach (var docItem in documents.EnumerateArray())
                {
                    if (!docItem.TryGetProperty("fields", out var fields)) continue;

                    double s_voltage = TryGetDouble(fields, "Solar Voltage (V)");
                    double s_current = TryGetDouble(fields, "Solar Current (A)");
                    double s_power   = TryGetDouble(fields, "Solar Power (W)");

                    double l_voltage = TryGetDouble(fields, "Load Voltage (V)");
                    double l_current = TryGetDouble(fields, "Load Current (A)");
                    double l_power   = TryGetDouble(fields, "Load Power (W)");

                    double b_voltage = TryGetDouble(fields, "Battery Voltage (V)");
                    double b_current = TryGetDouble(fields, "Battery Current (A)");
                    double b_power   = TryGetDouble(fields, "Battery Power (W)");

                    DateTime timestamp = TryGetTimestamp(fields, "Timestamp");

                    dataList.Add(new SolarData
                    {
                        SolarVoltage = s_voltage,
                        SolarCurrent = s_current,
                        SolarPower = s_power,
                        LoadVoltage = l_voltage,
                        LoadCurrent = l_current,
                        LoadPower = l_power,
                        BatteryVoltage = b_voltage,
                        BatteryCurrent = b_current,
                        BatteryPower = b_power,

                        Timestamp = timestamp
                    });
                }
            }

            return dataList;
        }

        private double TryGetDouble(JsonElement fields, string key)
        {
            if (fields.TryGetProperty(key, out var element))
            {
                if (element.TryGetProperty("doubleValue", out var valDouble) ||
                     element.TryGetProperty("integerValue", out valDouble))
                {
                    return double.TryParse(valDouble.ToString(), out var result) ? result : 0.0;
                }

            }

            return 0.0;
               
        }

        private DateTime TryGetTimestamp(JsonElement fields, string key)
        {
            if (fields.TryGetProperty(key, out var element) &&
                element.TryGetProperty("timestampValue", out var valTimestamp))
            {
                if (DateTime.TryParse(valTimestamp.ToString(), out var result))
                    return result;
            }

            return DateTime.MinValue;
        }
    }
}


