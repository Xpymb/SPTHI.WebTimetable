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

        public override string Message => "Вы находитесь в главном меню, для чтобы воспользоваться одной из моих функций, " +
                                          "нажмите на любую кнопку ниже &#128071;";

        public override List<string> Payload => new() { "undefined" };

        public override void Execute(Message message, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();

            var aboutInstituteButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, ButtonPayload.GetDefaultPayload(), "Об институте", VkKeyboard.ButtonColorType.White);

            var aboutBotButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, ButtonPayload.GetDefaultPayload(), "О чат-боте", VkKeyboard.ButtonColorType.White);

            var schedulePayload = ButtonPayload.CreatePayload("schedule_choosegrouptype");
            var scheduleButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, schedulePayload, "Расписание пар", VkKeyboard.ButtonColorType.White);

            var callPayload = ButtonPayload.CreatePayload("call");
            var callButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, callPayload, "Расписание звонков", VkKeyboard.ButtonColorType.White);

            var callbackButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.OpenLink, ButtonPayload.GetDefaultPayload(), "Сообщить о проблеме", VkKeyboard.ButtonColorType.Null, AppSettings.CallbackForm);

            listButtons.Add(scheduleButton);
            listButtons.Add(callButton);
            listButtons.Add(aboutInstituteButton);
            listButtons.Add(aboutBotButton);
            listButtons.Add(callbackButton);

            var keyboard = VkKeyboard.CreateKeyboard(false, listButtons);

            SendMessage(client, userId, Message, keyboard);
        }

        public override void ExecutePayload(Message message, ButtonPayloadClass payload, VkApi client)
        {
            throw new NotImplementedException();
        }
    }
}