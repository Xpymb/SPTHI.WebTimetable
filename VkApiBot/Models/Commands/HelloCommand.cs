using System;
using System.Collections.Generic;
using VkApiBot.Controllers;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public class HelloCommand : Command
    {
        public override List<string> Name => new() { "привет", "приветули" };

        public override string Message => "Привет!";

        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId.ToString();

            client.Call("messages.send", new VkNet.Utils.VkParameters
            {
                { "random_id", new Random().Next(Int32.MaxValue) },
                { "peer_id", 207753605 },
                { "message", Message },
            });

            /*
            client.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
            {
                PeerId = userId,
                RandomId = new Random().Next(0, Int32.MaxValue),
                Message = Message,
            });*/
        }
    }
}