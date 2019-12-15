using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WeatherStation.Web.Api.Models;
using WeatherStation.Web.Api.Services;
using WeatherStation.Web.Api.Hubs;

namespace WeatherStation.Web.Api.Controllers
{
    public class MeasurementController : Controller
    {
        private readonly MeasurementService _measurementService;
        private readonly IHubContext<ChatHub> _chatHub;

        public MeasurementController(MeasurementService measurementService, IHubContext<ChatHub> chatHubContext)
        {
            _measurementService = measurementService;
            _chatHub = chatHubContext;
        }

        // GET: Measurement
        public ActionResult Index()
        {
            var measurements = _measurementService.Get();

            if (measurements.Count == 0)
            {
                return NotFound();
            }

            var view = new List<MeasurementViewModel>();

            foreach (var measurement in measurements)
            {
                var m = new MeasurementViewModel
                {
                    Time = measurement.Time,
                    Temperature = measurement.Temperature,
                    AirPressure = measurement.AirPressure,
                    Humidity = measurement.Humidity
                };

                if (measurement.LocalWeatherStation != null)
                {
                    m.WeatherStationName = measurement.LocalWeatherStation.Name;
                }

                view.Add(m);
            }

            return Json(view);
        }



        [HttpGet("Measurement/search")]
        public ActionResult Search(string startTime, string endTime, string date)
        {
            if (string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime) && string.IsNullOrEmpty(date))
            {
                return NotFound();
            }

            var measurements = new List<Measurement>();
            var measurementsToDisplay = new List<MeasurementViewModel>();

            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
            {
                measurements = _measurementService.Get();
                var st = TimeSpan.ParseExact(startTime, "hhmm", null);
                var et = TimeSpan.ParseExact(endTime, "hhmm", null);


                foreach (var measurement in measurements)
                {
                    if (measurement.Time.TimeOfDay >= st && measurement.Time.TimeOfDay <= et)
                    {
                        var mvm = new MeasurementViewModel
                        {
                            Time = measurement.Time,
                            AirPressure = measurement.AirPressure,
                            Humidity = measurement.Humidity,
                            Temperature = measurement.Temperature
                        };
                        

                        if (measurement.LocalWeatherStation != null)
                        {
                            mvm.WeatherStationName = measurement.LocalWeatherStation.Name;
                        }

                        measurementsToDisplay.Add(mvm);
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
                        var mvm = new MeasurementViewModel
                        {
                            Time = measurement.Time,
                            AirPressure = measurement.AirPressure,
                            Humidity = measurement.Humidity,
                            Temperature = measurement.Temperature
                        };


                        if (measurement.LocalWeatherStation != null)
                        {
                            mvm.WeatherStationName = measurement.LocalWeatherStation.Name;
                        }

                        measurementsToDisplay.Add(mvm);
                    }
                }
            }

            if (measurementsToDisplay.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Json(measurementsToDisplay);
            }
        }

        // POST: Measurement/Create
        [Authorize(AuthenticationSchemes = "JwtBearer")]
        [HttpPost]
        public IActionResult Create([FromBody] MeasurementModel model)
        {
            try
            {
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
                    Time = DateTime.ParseExact(model.Time, "dd-MM-yyyy HH:mm:ss", null)
                };


                if (weatherStation.Measurements == null)
                {
                    weatherStation.Measurements = new List<Measurement>();
                }

                weatherStation.Measurements.Add(measurement);

                _measurementService.Create(measurement);

                // Notify Hub
                _chatHub.Clients.All.SendAsync("ReceiveMessage", "New Data in ", model.Location.Name);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Client()
        {
            return View();
        }
    }
}