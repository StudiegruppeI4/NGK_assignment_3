using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGKHandIn3.Models
{
    public class WeatherObservation
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string LocationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Temperature { get; set; }
        public int HumidityPercentage { get; set; }
        public double AirPressure { get; set; }
    }
}
