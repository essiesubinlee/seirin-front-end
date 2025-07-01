
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
}