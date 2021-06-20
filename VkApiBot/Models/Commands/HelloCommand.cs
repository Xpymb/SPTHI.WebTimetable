using System;
using System.Collections.Generic;
using VkApiBot.Controllers;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public class HelloCommand : Command
    {
        public override List<string> Name => new() { "Привет", "Приветули" };

        public override string Message => "Привет!";

        public override List<string> Payload => new() { "undefined" };

        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId;

            SendMessage(client, userId, Message);
        }

        public override void ExecutePayload(Message message, string payload, VkApi client)
        {
            throw new NotImplementedException();
        }
    }
}