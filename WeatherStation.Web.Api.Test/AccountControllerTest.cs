using System;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WeatherStation.Web.Api.Services;
using NSubstitute;
using WeatherStation.Web.Api.Controllers;
using WeatherStation.Web.Api.Models;

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

        //[Test]
        //public void IsAuthenticated()
        //{
        //    _authenticationUserModel.Username
        //    _uut.Authenticate(_authenticationUserModel);


        //}

        [Test]
        public void UserIsNull()
        {
            var response = _uut.Authenticate(null);
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual("Username or Password is incorrect", response);

        }
    }
}
