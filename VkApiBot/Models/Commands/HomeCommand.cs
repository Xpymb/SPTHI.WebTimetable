using System;
using System.Collections.Generic;
using VkApiBot.Controllers;
using VkApiBot.Models.VK.Keyboard;
using VkApiBot.Models.VK.Payload;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public class HomeCommand : Command
    {
        public override List<string> Name => new() { "Главное меню" };

        public override string Message => "Я умный чат-бот из системы БАРСик, чтобы воспользоваться одной из моих функций, " +
                                          "нажмите на любую кнопку ниже";

        public override List<string> Payload => new() { "undefined" };

        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();

            var aboutInstituteButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, ButtonPayload.GetDefaultPayload(), "О институте", VkKeyboard.ButtonColorType.White);

            var aboutBotButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, ButtonPayload.GetDefaultPayload(), "О боте", VkKeyboard.ButtonColorType.White);

            var schedulePayload = ButtonPayload.CreatePayload("schedule_choosegroup");
            var scheduleButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, schedulePayload, "Расписание пар", VkKeyboard.ButtonColorType.White);

            var callPayload = ButtonPayload.CreatePayload("call");
            var callButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, callPayload, "Расписание звонков", VkKeyboard.ButtonColorType.White);

            listButtons.Add(aboutInstituteButton);
            listButtons.Add(aboutBotButton);
            listButtons.Add(scheduleButton);
            listButtons.Add(callButton);

            var keyboard = VkKeyboard.CreateKeyboard(false, listButtons);

            SendMessage(client, userId, Message, keyboard);
        }

        public override void ExecutePayload(Message message, ButtonPayloadClass payload, VkApi client)
        {
            throw new NotImplementedException();
        }
    }
}