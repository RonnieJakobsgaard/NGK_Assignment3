using System;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WeatherStation.Web.Api.Services;
using NSubstitute;
using WeatherStation.Web.Api.Controllers;
using WeatherStation.Web.Api.Models;
using System.Web.Http.Results;
using NSubstitute.ReturnsExtensions;
using BadRequestResult = Microsoft.AspNetCore.Mvc.BadRequestResult;

namespace WeatherStation.Web.Api.Test
{
    [TestFixture]
    public class AccountControllerTest
    {
        private IAccountService _accountService;
        private AuthenticationUserModel _authenticationUserModel;
        private AccountController _uut;


        [SetUp]
        public void SetUp()
        {
            _accountService = Substitute.For<IAccountService>();
            _authenticationUserModel = Substitute.For<AuthenticationUserModel>();

            _uut = new AccountController(_accountService);
        }

        [Test]
        public void UserIsNotNull()
        {
            _accountService.Authenticate(null, null).Returns(new UserWithoutPassword());
            var testresponse = _uut.Authenticate(_authenticationUserModel);
            Assert.IsInstanceOf<OkObjectResult>(testresponse);
        }

        [Test]
        public void UserIsNull()
        {
            var testreponse = _uut.Authenticate(_authenticationUserModel);

            Assert.IsInstanceOf<BadRequestObjectResult>(testreponse);
        }
    }
}
