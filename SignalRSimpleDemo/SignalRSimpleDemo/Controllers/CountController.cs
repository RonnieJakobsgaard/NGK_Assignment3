using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalRSimpleDemo.Hubs;
using SignalRSimpleDemo.Models;

namespace SignalRSimpleDemo.Controllers
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

        // GET: api/Count/inc
        [HttpGet("inc")]
        public async Task<IActionResult> Inc()
        {
            await _chatHubContext.Clients.All.SendAsync("countUpdate", _counter.Inc());
            return Ok();
        }
    }
}
