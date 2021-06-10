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
            var userId = (long)message.FromId;

            client.Call("messages.send", new VkNet.Utils.VkParameters
            {
                { "random_id", new Random().Next(0, Int32.MaxValue) },
                { "peer_id", userId },
                { "message", Message },
                { "v", "5.130" }
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