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

        public override string Message => "Я умный чат-бот из системы БАРСик, разработанный " +
                                          "Лабораторией Мобильной Робототехники \n\n" +
                                          "Ссылка на группу лаборатории: https://vk.com/roboticslab";

        public override List<string> Payload => new() { "undefined" };

        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();

            var aboutInstituteButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, ButtonPayload.GetDefaultPayload(), "О институте", VkKeyboard.ButtonColorType.White);
            var callbackButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.OpenLink, ButtonPayload.GetDefaultPayload(), "Обратная связь", VkKeyboard.ButtonColorType.Null, $"https://vk.com/id{AppSettings.AdminId}");
            var homeButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, ButtonPayload.GetDefaultPayload(), "Главное меню", VkKeyboard.ButtonColorType.Blue);

            listButtons.Add(aboutInstituteButton);
            listButtons.Add(callbackButton);
            listButtons.Add(homeButton);

            var keyboard = VkKeyboard.CreateKeyboard(false, listButtons);

            SendMessage(client, userId, Message, keyboard);
        }

        public override void ExecutePayload(Message message, ButtonPayloadClass payload, VkApi client)
        {
            throw new NotImplementedException();
        }
    }
}