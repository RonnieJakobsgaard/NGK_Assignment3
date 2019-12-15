using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherStation.Web.Api.Models;
using Microsoft.AspNetCore.Identity;
using WeatherStation.Web.Api.Services;


namespace WeatherStation.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        public IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationUserModel model)
        {
            var user = _accountService.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new {message = "Username or Password is incorrect"});
            }

            return Ok(user);
        }
    }
}
