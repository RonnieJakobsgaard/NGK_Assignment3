using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using WeatherStation.Web.Api.Models;
using WeatherStation.Web.Api.Services;

namespace WeatherStation.Web.Api.Controllers
{
    public class MeasurementController : Controller
    {
        private readonly MeasurementService _measurementService;

        public MeasurementController(MeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        // GET: Measurement
        public ActionResult Index()
        {
            return View(_measurementService.Get());
        }



        [HttpGet("Measurement/search")]
        public ActionResult Search(string startTime, string endTime, string date)
        {
            if (string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime) && string.IsNullOrEmpty(date))
            {
                return NotFound();
            }

            var measurements = new List<Measurement>();
            var measurementsToDisplay = new List<Measurement>();

            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
            {
                measurements = _measurementService.Get();
                var st = TimeSpan.ParseExact(startTime, "hhmm", null);
                var et = TimeSpan.ParseExact(endTime, "hhmm", null);


                foreach (var measurement in measurements)
                {
                    if (measurement.Time.TimeOfDay >= st && measurement.Time.TimeOfDay <= et)
                    {
                        measurementsToDisplay.Add(measurement);
                    }
                }
            }

            if (!string.IsNullOrEmpty(date))
            {
                measurements = _measurementService.Get();
                DateTime searchTime = DateTime.ParseExact(date, "dd-MM-yyyy", null);
                
                foreach (var measurement in measurements)
                {
                    if (measurement.Time.Date >= searchTime.Date && measurement.Time.Date <= searchTime.Date)
                    {
                        measurementsToDisplay.Add(measurement);
                    }
                }
            }

            if (measurementsToDisplay.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return View(measurementsToDisplay);
            }
        }

        // GET: Measurement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Measurement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Measurement/Create
        [Authorize(AuthenticationSchemes = "JwtBearer")]
        [HttpPost]
        public ActionResult Create([FromBody] MeasurementModel model)
        {
            try
            {
                // TODO: Add insert logic here

                var weatherStation = _measurementService.FindWeatherStation(model.Location.Name);

                if (weatherStation == null)
                {
                    weatherStation = new LocalWeatherStation
                    {
                        Name = model.Location.Name,
                        Longitude = model.Location.Longitude,
                        Latitude = model.Location.Latitude
                    };
                }

                var measurement = new Measurement
                {
                    LocalWeatherStation = weatherStation,
                    Temperature = model.Temperature,
                    AirPressure = model.AirPressure,
                    Humidity = model.Humidity,
                    Time = DateTime.Now
                };


                if (weatherStation.Measurements == null)
                {
                    weatherStation.Measurements = new List<Measurement>();
                }

                weatherStation.Measurements.Add(measurement);

                _measurementService.Create(measurement);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: Measurement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Measurement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Measurement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Measurement/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}