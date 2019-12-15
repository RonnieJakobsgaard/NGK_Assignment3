using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherStation.Web.Api.Data;
using WeatherStation.Web.Api.Models;

namespace WeatherStation.Web.Api.Services
{
    public class MeasurementService
    {
        private readonly ApplicationDbContext _db;

        public MeasurementService()
        {
            _db = new ApplicationDbContext();
        }

        public List<Measurement> Get()
        {
            return _db.Measurements.ToList();
        }

        public Measurement Get(int id)
        {
            return _db.Measurements.Single(m => m.MeasurementId == id);
        }

        public void Create(Measurement measurement)
        {
            _db.Add(measurement);
            _db.SaveChanges();
        }

        public void Update(int id, Measurement measurementIn)
        {
            var measurement = _db.Measurements.Single(m => m.MeasurementId == id);

            measurement = measurementIn;

            _db.Measurements.Update(measurement);
            _db.SaveChanges();
        }

        public void Remove(Measurement measurement)
        {
            _db.Measurements.Remove(measurement);
        }

        public void Remove(int id)
        {
            var measurement = _db.Measurements.Single(m => m.MeasurementId == id);
            _db.Remove(measurement);
            _db.SaveChanges();
        }

        public List<Measurement> Search(DateTime startDate, DateTime endDate)
        {
            return _db.Measurements.Where(m => m.Time >= startDate && m.Time <= endDate).ToList();
        }

        public LocalWeatherStation FindWeatherStation(string name)
        {
            return _db.WeatherStations.SingleOrDefault(w => w.Name == name);
        }

        public List<LocalWeatherStation> GetAllWeatherStations()
        {
            return _db.WeatherStations.ToList();
        }
    }
}
