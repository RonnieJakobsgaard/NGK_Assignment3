using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Models
{
    public class WeatherStation
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }


        // Relationship
        public List<Measurement> Measurements { get; set; }
    }
}
