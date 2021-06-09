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
            var userId = message.UserId;

            client.Call("messages.send", new VkNet.Utils.VkParameters
            {
                { "message", Message },
                { "peer_id", userId },
                { "random_id", new Random().Next(Int32.MinValue, Int32.MinValue) },
                { "group_id", AppSettings.GroupId },
                { "v", AppSettings.ApiVersion }
            });

            /*client.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
            {
                UserId = userId,
                RandomId = new Random().Next(Int32.MinValue, Int32.MinValue),
                Message = Message,
                GroupId = AppSettings.GroupId,
                
            });*/
        }
    }
}