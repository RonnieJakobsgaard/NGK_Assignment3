using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WeatherStation.Web.Api.Models
{
    public class MeasurementModel
    {
        [BindProperty]
        public Place Location { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float AirPressure { get; set; }
        public string Time { get; set; }
    }

    public class Place
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
