using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VkApiBot.Models;
using VkNet.Model;
using VkNet.Utils;

namespace VkApiBot.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CallbackController : Controller
    {
        private readonly IConfiguration _configuration;

        public CallbackController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Update([FromBody] Updates updates)
        {
            switch(updates.Type)
            {
                case "confirmation":
                {
                    return Ok(_configuration["Config:ConfirmationCode"]);
                }

                case "message_new":
                {
                    var msg = Message.FromJson(new VkResponse(updates.Object));
                    var client = Bot.Get().Result;

                    foreach(var command in Bot.Commands)
                    {
                        if(command.Contains(msg.Text))
                        {
                            command.Execute(msg, client);
                        }
                    }    

                    break;
                }
            }

            return Ok();
        }
    }
}