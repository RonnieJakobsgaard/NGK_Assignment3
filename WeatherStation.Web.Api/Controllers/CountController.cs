﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.Hubs;
using WeatherStation.Web.Api.Models;

namespace WeatherStation.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly counter _counter;

        public CountController(IHubContext<ChatHub> chatHubContext, counter counter)
        {
            _chatHubContext = chatHubContext;
            _counter = counter;
        }

        [HttpGet("inc")]

        public async Task<IActionResult> Get()
        {
            await _chatHubContext.Clients.All.SendAsync("countUpdate", _counter.Inc());
            return Ok();
        }
    }
}