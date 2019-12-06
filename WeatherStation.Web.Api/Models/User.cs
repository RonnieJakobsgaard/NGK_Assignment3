using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation.Web.Api.Models
{
    public class User
    {
        public string UserName { get; set; }

        public int Password { get; set; }

        //has many weatherstaions
        public List<LocalWeatherStation> WeatherStations { get; set; }
    }
}

//lav kode til user registration