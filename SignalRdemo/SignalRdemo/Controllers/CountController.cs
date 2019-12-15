using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRdemo.Hubs;
using SignalRdemo.Models;
using System.Threading.Tasks;

namespace SignalRdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> Get() 
        {
            await _chatHubContext.Clients.All.SendAsync("countUpdate", _counter.Inc());
            return Ok();
        }
    }
}
