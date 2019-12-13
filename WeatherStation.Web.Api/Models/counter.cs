using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation.Web.Api.Models
{
    public class counter
    {
        long value = 0;

        public long Inc()
        {
            return ++value;
        }
    }
}
