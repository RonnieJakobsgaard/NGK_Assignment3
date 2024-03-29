﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherStation.Web.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace WeatherStation.Web.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext"));
            }
        }

        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<LocalWeatherStation> WeatherStations { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Measurement>()
                .HasKey(k => new { k.MeasurementId });


            modelBuilder.Entity<LocalWeatherStation>()
                .HasKey(k => new { k.WeatherStationId });

            modelBuilder.Entity<User>()
                .HasKey(k => new {k.Username});

            modelBuilder.Entity<User>()
                .Property(id => id.UserId)
                .ValueGeneratedOnAdd();

            // One To Many
            modelBuilder.Entity<LocalWeatherStation>()
                .HasMany<Measurement>(m => m.Measurements)
                .WithOne(m => m.LocalWeatherStation);


            base.OnModelCreating(modelBuilder);
        }
    }

   
}
