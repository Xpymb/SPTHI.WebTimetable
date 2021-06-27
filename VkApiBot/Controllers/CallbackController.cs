using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using VkApiBot.Models;
using VkApiBot.Models.VK.Payload;

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
                    var payload = ButtonPayload.DeserializePayload(msg.Payload);
                    var client = Bot.Get();

                    foreach(var command in Bot.Commands)
                    {
                        if ((payload.Command != null) && (command.ContainsPayload(payload.Command)))
                        {
                            command.ExecutePayload(msg, payload, client);
                            break;
                        }
                        else if (command.Contains(msg.Text))
                        {
                            command.Execute(msg, client);
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