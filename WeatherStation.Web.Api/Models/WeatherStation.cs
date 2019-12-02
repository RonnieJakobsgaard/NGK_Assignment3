using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation.Web.Api.Models
{
    public class LocalWeatherStation
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }


        // Many measurements
        public List<Measurement> Measurements { get; set; }
    }
}
