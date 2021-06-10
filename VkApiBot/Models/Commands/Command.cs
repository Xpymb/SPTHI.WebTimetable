using System.Collections.Generic;
using VkApiBot.Controllers;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public abstract class Command
    {
        public abstract List<string> Name { get; }
        public abstract string Message { get; }

        public abstract void Execute(Message message, VkApi client);
        
        public bool Contains(string message)
        {
            foreach(var name in Name)
            {
                if(message.ToLower().Contains(name))
                {
                    return true;
                }
            }

            return false;
        }
    }
}