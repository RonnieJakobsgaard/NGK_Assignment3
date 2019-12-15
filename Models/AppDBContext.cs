using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantsDAB.Models;
using Assignment3.Models.Relationship;


namespace Assignment3
{
	public class AppDBContext : DbContext
	{
        public AppDBContext(DbContextOptions <AppDBContext> options) 
            : base(options)
        { 

        }




        public DbSet<Weather_meassurement> Meassurements { get; set; }

    }
}
