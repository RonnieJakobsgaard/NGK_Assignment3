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
        public ActionResult Index(string startTime, string endTime)
        {
            if (!string.IsNullOrEmpty(startTime) || !string.IsNullOrEmpty(endTime))
            {
                //var sDate = DateTime.ParseExact(startDate, "dd-MM-yyyy", null);
                //var eDate = DateTime.ParseExact(startDate, "dd-MM-yyyy", null);
                //var measurements = _measurementService.Search(sDate, eDate);

                // Test data
                var lws = new LocalWeatherStation()
                {
                    Name = "TestStation",
                    Latitude = 2.555,
                    Longitude = 95.002
                };

                var measurements = new List<Measurement>()
                {
                    new Measurement()
                    {
                        AirPressure = 5, Humidity = 5, LocalWeatherStation = lws, MeasurementId = 1,
                        Time = new DateTime(2019, 1, 1), Temperature = 2
                    },
                    new Measurement()
                    {
                        AirPressure = 2000, Humidity = 95, LocalWeatherStation = lws, MeasurementId = 2,
                        Time = new DateTime(2019, 1, 10), Temperature = 5
                    }
                };

                return View(measurements);
            }

            return View(_measurementService.Get());
        }



        [HttpGet("Measurement/search")]
        public ActionResult Search(string startTime, string endTime, string date)
        {
            if (string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime) && string.IsNullOrEmpty(date))
            {
                return NotFound();
            }

            LocalWeatherStation lws = new LocalWeatherStation();

            if (!string.IsNullOrEmpty(startTime) || !string.IsNullOrEmpty(endTime))
            {
                lws = new LocalWeatherStation()
                {
                    Name = "StartTime-EndTime",
                    Latitude = 2.555,
                    Longitude = 95.002
                };
            }

            if (!string.IsNullOrEmpty(date))
            {
                lws = new LocalWeatherStation()
                {
                    Name = "Date",
                    Latitude = 2.555,
                    Longitude = 95.002
                };
            }


            IEnumerable<Measurement> measurements = new List<Measurement>()
                {
                    new Measurement()
                    {
                        AirPressure = 5, Humidity = 5, LocalWeatherStation = lws, MeasurementId = 1,
                        Time = new DateTime(2019, 1, 1), Temperature = 2
                    },

                    new Measurement()
                    {
                        AirPressure = 2000, Humidity = 95, LocalWeatherStation = lws, MeasurementId = 2,
                        Time = new DateTime(2019, 1, 10), Temperature = 5
                    }
                };

            return View(measurements);
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
        public ActionResult Create([FromBody] Measurement measurement)
        {
            try
            {
                // TODO: Add insert logic here
                _measurementService.Create(measurement);


                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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