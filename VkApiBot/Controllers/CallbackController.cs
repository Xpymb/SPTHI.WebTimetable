using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using VkApiBot.Models;
using VkApiBot.Models.VK.Payload;
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

        [HttpPost]
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
                    var msg = updates.Object.Message;
                    var payload = msg.Payload != null ? ButtonPayload.DeserializePayload(msg.Payload) : new ButtonPayloadClass { Button = "" };
                    var client = Bot.Get();

                    foreach(var command in Bot.Commands)
                    {
                        if(command.Contains(msg.Text))
                        {
                            command.Execute(msg, client);
                            break;
                        }
                        else if(payload.Button != "" && command.ContainsPayload(payload.Button))
                        {
                            command.ExecutePayload(msg, payload.Button, client);
                            break;
                        }
                    }

                    break;
                }
            }

            return Ok("ok");
        }
    }
}