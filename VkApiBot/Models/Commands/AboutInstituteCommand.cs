using System;
using System.Collections.Generic;
using VkApiBot.Controllers;
using VkApiBot.Models.Keyboard;
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

        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();

            listButtons.Add(new Button
            {
                Action = new Keyboard.Action { ActionType = "text", Payload = VkKeyboard.DefaultPayload, Label = "О боте" },
                Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White)
            });
            listButtons.Add(new Button
            {
                Action = new Keyboard.Action { ActionType = "text", Payload = VkKeyboard.DefaultPayload, Label = "Главное меню" },
                Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.Blue)
            });

            var keyboard = VkKeyboard.CreateKeyaboard(true, listButtons);

            client.Call("messages.send", new VkNet.Utils.VkParameters
            {
                { "random_id", new Random().Next(Int32.MaxValue) },
                { "peer_id", userId },
                { "message", Message },
                { "keyboard", keyboard }
            });
        }
    }
}