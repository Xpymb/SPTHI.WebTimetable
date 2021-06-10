using System;
using System.Collections.Generic;
using VkNet;
using VkNet.Model;

namespace VkApiBot.Models.Commands
{
    public class HelloCommand : Command
    {
        public override List<string> Name => new() { "привет", "приветули" };

        public override string Message => "Привет!";

        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId;
                        
            client.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
            {
                PeerId = userId,
                RandomId = new Random().Next(Int32.MinValue, Int32.MinValue),
                Message = Message,
            });
        }
    }
}