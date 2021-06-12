using System;
using System.Collections.Generic;
using VkApiBot.Controllers;
using VkApiBot.Models.Keyboard;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public class HomeCommand : Command
    {
        public override List<string> Name => new() { "Главное меню" };

        public override string Message => "Я умный чат-бот из системы БАРСик, чтобы воспользоваться одной из моих функций, " +
                                          "нажмите на любую кнопку ниже";

        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();

            listButtons.Add(new Button
            {
                Action = new Keyboard.Action { ActionType = "text", Payload = VkKeyboard.DefaultPayload, Label = "О институте" },
                Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White)
            });
            listButtons.Add(new Button
            {
                Action = new Keyboard.Action { ActionType = "text", Payload = VkKeyboard.DefaultPayload, Label = "О боте" },
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