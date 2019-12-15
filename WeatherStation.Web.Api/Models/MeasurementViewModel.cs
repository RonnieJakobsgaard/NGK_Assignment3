using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation.Web.Api.Models
{
    public class MeasurementViewModel
    {
        public string WeatherStationName { get; set; }
        public DateTime Time { get; set; }

        public float Temperature { get; set; }

        public float Humidity { get; set; }
        public float AirPressure { get; set; }
    }
}
