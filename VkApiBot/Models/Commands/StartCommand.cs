using System;
using System.Collections.Generic;
using VkApiBot.Controllers;
using VkApiBot.Models.VK.Keyboard;
using VkApiBot.Models.VK.Payload;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public class StartCommand : Command
    {
        public override List<string> Name => new() { "Начать", "Start" };

        public override string Message => "Привет! Я умный чат-бот из системы БАРСик, чтобы перейти в главное меню, нажмите на кнопку ниже.";

        public override List<string> Payload => new() { "undefined" };

        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();

            listButtons.Add(VkKeyboard.CreateButton(VkKeyboard.ButtonAction.Text, ButtonPayload.GetDefaultPayload(), "Главное меню", VkKeyboard.ButtonColor.Blue));

            var keyboard = VkKeyboard.CreateKeyboard(false, listButtons);

            SendMessage(client, userId, Message, keyboard);
        }

        public override void ExecutePayload(Message message, ButtonPayloadClass payload, VkApi client)
        {
            throw new NotImplementedException();
        }
    }
}