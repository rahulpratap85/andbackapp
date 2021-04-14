using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ANDChatApp.Hubs;
using ANDChatApp.ReqDto;

namespace ANDChatApp.Controllers
{
    [Produces("application/json")]
    [Route("api/chat")]
    [ApiController]
    [ProducesResponseType(typeof(IEnumerable), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [Route("send")]                                           //path looks like this: https://localhost:44379/api/chat/send
        [HttpPost]
        public IActionResult SendRequest([FromBody] MessageDto msg)
        {
            _hubContext.Clients.All.SendAsync("ReceiveOne", msg.user, msg.msgText);
            return Ok();
        }

        [Route("Foo")]                                           //path looks like this: https://localhost:44379/api/chat/send
        [HttpGet]
        public IActionResult GetRequest()
        {            
            return Ok("Ok from get ");
        }

    }
}
