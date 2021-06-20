using System;
using System.Collections.Generic;
using VkApiBot.Controllers;
using VkApiBot.Models.VK.Keyboard;
using VkApiBot.Models.VK.Payload;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public class AboutInstituteCommand : Command
    {
        public override List<string> Name => new() { "О институте" };

        public override string Message => "Снежинский физико-технический институт (с 2001 года — академия) — федеральное " +
                                          "государственное образовательное учреждение высшего образования. Единственный вуз " +
                                          "в Снежинске (бывший Челябинск-70). Входит в состав федерального государственного " +
                                          "автономного образовательного учреждения высшего образования «Национальный " +
                                          "исследовательский ядерный университет «МИФИ» (СФТИ НИЯУ МИФИ).";

        public override List<string> Payload => new() { "undefined" };
        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();

            var payload = ButtonPayload.CreatePayload("1");

            listButtons.Add(new Button
            {
                Action = new VK.Keyboard.Action { ActionType = "text", Payload = payload, Label = "О боте" },
                Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White)
            });
            listButtons.Add(new Button
            {
                Action = new VK.Keyboard.Action { ActionType = "text", Payload = VkKeyboard.DefaultPayload, Label = "Главное меню" },
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