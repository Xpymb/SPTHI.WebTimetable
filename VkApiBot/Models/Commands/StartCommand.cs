using System;
using System.Collections.Generic;
using VkApiBot.Controllers;
using VkApiBot.Models.Keyboard;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public class StartCommand : Command
    {
        public override List<string> Name => new() { "начать" };

        public override string Message => "Привет! Я умный чат-бот из системы БАРСик, чтобы узнать что я умею нажмите на одну из кнопок.";

        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();

            listButtons.Add(new Button
            {
                Action = new Keyboard.Action { ActionType = "text", Payload = VkKeyboard.DefaultPayload, Label = "Привет" },
                Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White)
            });
            listButtons.Add(new Button
            {
                Action = new Keyboard.Action { ActionType = "text", Payload = VkKeyboard.DefaultPayload, Label = "привет" },
                Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.Blue)
            });
            listButtons.Add(new Button
            {
                Action = new Keyboard.Action { ActionType = "text", Payload = VkKeyboard.DefaultPayload, Label = "Приветули" },
                Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White)
            });

            var keyboard = VkKeyboard.CreateKeyaboard(false, listButtons);

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