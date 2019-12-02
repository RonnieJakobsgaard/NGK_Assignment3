using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation.Web.Api.Models
{
    public class Measurement
    {
        public int MeasurementId { get; set; }
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float AirPressure { get; set; }

        // One WeatherStation
        public LocalWeatherStation LocalWeatherStation { get; set; }
    }
}
