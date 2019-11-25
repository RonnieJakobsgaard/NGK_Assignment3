using Assignment3.Models.Relationship;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Assignment3
{
	public class Place

	{
        public int PlaceId { get; set; }

        public string Name { get; set; }

        public int Lat { get; set; }

        public int Lon { get; set; }
	}
}
