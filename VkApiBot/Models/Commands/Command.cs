using System;
using System.Collections.Generic;
using VkApiBot.Controllers;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public abstract class Command
    {
        public abstract List<string> Name { get; }
        public abstract List<string> Payload { get; }
        public abstract string Message { get; }

        public abstract void Execute(Message message, VkApi client);
        public abstract void ExecutePayload(Message message, string payload, VkApi client);

        public bool Contains(string message)
        {
            foreach(var name in Name)
            {
                if(message.ToLower().Contains(name.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsPayload(string payloadArg)
        {
            if(payloadArg == null || payloadArg == "")
            {
                return false;
            }

            foreach (var payload in Payload)
            {
                if (payloadArg.Contains(payload))
                {
                    return true;
                }
            }

            return false;
        }

        protected void SendMessage(VkApi client, ulong userId, string message)
        {
            client.Call("messages.send", new VkNet.Utils.VkParameters
            {
                { "random_id", new Random().Next(Int32.MaxValue) },
                { "peer_id", userId },
                { "message", message },
            });
        }

        protected void SendMessage(VkApi client, ulong userId, string message, string keyboard)
        {
            client.Call("messages.send", new VkNet.Utils.VkParameters
            {
                { "random_id", new Random().Next(Int32.MaxValue) },
                { "peer_id", userId },
                { "message", message },
                { "keyboard", keyboard }
            });
        }
    }
}