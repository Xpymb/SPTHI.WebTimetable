using System;
using System.Collections.Generic;
using VkApiBot.Controllers;
using VkApiBot.Models.VK.Keyboard;
using VkApiBot.Models.VK.Payload;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public class AboutBotCommand : Command
    {
        public override List<string> Name => new() { "О боте" };

        public override string Message => "Я умный чат-бот из системы БАРСик, разработанный выпускником КС-47д, состоящим в " +
                                          "лаборатории Мобильной робототехники";

        public override List<string> Payload => new() { "undefined" };
        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();

            var payload = ButtonPayload.CreatePayload("1");

            listButtons.Add(new Button
            {
                Action = new VK.Keyboard.Action { ActionType = "text", Payload = payload, Label = "О институте" },
                Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White)
            });
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