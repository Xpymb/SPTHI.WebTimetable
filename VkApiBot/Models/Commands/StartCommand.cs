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

            var payload = ButtonPayload.CreatePayload("1");

            listButtons.Add(new Button
            {
                Action = new VK.Keyboard.Action { ActionType = "text", Payload = payload, Label = "Главное меню" },
                Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.Blue)
            });

            var keyboard = VkKeyboard.CreateKeyaboard(false, listButtons);

            SendMessage(client, userId, Message, keyboard);
        }

        public override void ExecutePayload(Message message, string payload, VkApi client)
        {
            throw new NotImplementedException();
        }
    }
}