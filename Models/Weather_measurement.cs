using Assignment3.Models.Relationship;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Assignment3
{
	public class Weather_meassurement
    { 
        public int Weather_meassurementId { get; set; }

        public DateTime Time { get; set; }
         
        public float Temperature { get; set; }
        
        public int Humidity { get; set; }

        public int AirPressure { get; set; }


    }
}
