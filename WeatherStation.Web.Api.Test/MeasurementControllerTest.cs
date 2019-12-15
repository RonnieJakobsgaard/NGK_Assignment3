using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using NSubstitute;
using WeatherStation.Web.Api.Models;
using WeatherStation.Web.Api.Controllers;
using WeatherStation.Web.Api.Services;

namespace WeatherStation.Web.Api.Test
{
    [TestFixture]
    class MeasurementControllerTest
    {
        private MeasurementService _measurementService;
        private MeasurementController _uut;
        [SetUp]
        public void SetUp()
        {
            _measurementService = Substitute.For<MeasurementService>();
            _uut = new MeasurementController(_measurementService);
        }

        [Test]
        public void SearchReturnNotFound()
        {
            
            var testresponse = _uut.Search("", "", "");
            Assert.IsInstanceOf<NotFoundResult>(testresponse);
        }


        [Test]
        public void SearchReturnMeasurement()
        {
            var testmeasurement = new List<Measurement>();
            var testmeasurement2 = new List<Measurement>();
           
            var testresponse = _uut.Search("1", "2", "3");
            Assert.Contains(, testmeasurement);

        }
    }
}
