using System;
using System.Collections.Generic;
using System.Text;
using VkApiBot.Controllers;
using VkApiBot.gRPC.Services;
using VkApiBot.Models.VK.Keyboard;
using VkApiBot.Models.VK.Payload;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public class CallCommand : Command
    {
        public override List<string> Name => new() { "Расписание звонков" };

        public override string Message => "";

        public override List<string> Payload => new() { "call", "call_all", "call_next", "call_all_next" };

        public override void Execute(Message message, VkApi client)
        {
            var payload = ButtonPayload.DeserializePayload(message.Payload);

            //ExecutePayload(message, payload.Button, client);
        }

        public override void ExecutePayload(Message message, ButtonPayloadClass payload, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();

            var newMessage = payload.Command switch
            {
                "call" => ChooseCommands(listButtons),
                "call_next" => GetNextCall(),
                "call_all" => GetAllCalls(),
                "call_all_next" => GetAllNextCalls(),
                _ => "Сервис временно не доступен, повторите попытку позже",
            };

            if(payload.Command != "call")
            {
                var nextPayload = ButtonPayload.CreatePayload("call");

                var button = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, nextPayload, "Расписание звонков", VkKeyboard.ButtonColorType.White);

                listButtons.Add(button);
            }

            var homeButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, ButtonPayload.GetDefaultPayload(), "Главное меню", VkKeyboard.ButtonColorType.Blue);

            listButtons.Add(homeButton);

            var keyboard = VkKeyboard.CreateKeyboard(false, listButtons);

            SendMessage(client, userId, newMessage, keyboard);
        }


        private string ChooseCommands(List<Button> listButtons)
        {
            var nextCallPayload = ButtonPayload.CreatePayload("call_next");
            var nextCallButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, nextCallPayload, "Время следующего звонка", VkKeyboard.ButtonColorType.White);

            var allCallsPayload = ButtonPayload.CreatePayload("call_all");
            var allCallsButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, allCallsPayload, "Список всех звонков на сегодня", VkKeyboard.ButtonColorType.White);

            var allNextCallsPayload = ButtonPayload.CreatePayload("call_all_next");
            var allNextCallsButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, allNextCallsPayload, "Список всех следующих звонков на сегодня", VkKeyboard.ButtonColorType.White);

            listButtons.Add(nextCallButton);
            listButtons.Add(allCallsButton);
            listButtons.Add(allNextCallsButton);

            return "Выберите действие с помощью одной из кнопок.";
        }

        private static string GetNextCall()
        {
            var call = CallControllerServiceAPI.GetNextCall();

            if (call == null)
            {
                return "Сервис временно не доступен, повторите попытку позже.";
            }

            return $"Следующий звонок в {call.DateTime} - {call.Name}";
        }

        private static string GetAllCalls()
        {
            var calls = CallControllerServiceAPI.GetListCalls().Result;

            if (calls == null || calls.Count == 0)
            {
                return "Сервис временно не доступен, повторите попытку позже.";
            }

            var message = new StringBuilder($"Список звонков на сегодня:\n\n");

            foreach (var call in calls)
            {
                message.Append($"{call.DateTime} - {call.Name}\n");

                if (call.Name.Contains("конец"))
                {
                    message.Append('\n');
                }
            }

            return message.ToString();
        }

        private static string GetAllNextCalls()
        {
            var calls = CallControllerServiceAPI.GetListNextCalls().Result;

            if (calls == null || calls.Count == 0)
            {
                return "Сервис временно не доступен, повторите попытку позже.";
            }

            var message = new StringBuilder($"Список следующих звонков на сегодня:\n\n");

            foreach (var call in calls)
            {
                message.Append($"{call.DateTime} - {call.Name}\n");

                if (call.Name.Contains("конец"))
                {
                    message.Append('\n');
                }
            }

            return message.ToString();
        }
    }
}