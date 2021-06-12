using System;
using System.Collections.Generic;
using VkApiBot.Controllers;
using VkApiBot.Models.Keyboard;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public class StartCommand : Command
    {
        public override List<string> Name => new() { "Начать", "Start" };

        public override string Message => "Привет! Я умный чат-бот из системы БАРСик, чтобы перейти в главное меню, нажмите на кнопку ниже.";

        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();

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