using System.Collections.Generic;
using System.Text;
using VkApiBot.Controllers;
using VkApiBot.gRPC.Services;
using VkApiBot.Models.VK.Keyboard;
using VkApiBot.Models.VK.Payload;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public class ScheduleCommand : Command
    {
        public override List<string> Name => new() { "Расписание пар" };

        public override string Message => "";

        public override List<string> Payload => new() { "schedule", "schedule_choosegroup", "schedule_choosedate", "schedule_result" };

        public override void Execute(Message message, VkApi client)
        {
            //var payload = ButtonPayload.DeserializePayload(message.Payload);

            //ExecutePayload(message, payload.Button, client);
        }

        public override void ExecutePayload(Message message, ButtonPayloadClass payload, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();

            var payloadArgs = ButtonPayload.DeserializeSchedulePayload(payload.Logic);

            string newMessage = payload.Command switch
            {
                "schedule_choosegroup" => ChooseGroup(listButtons),
                "schedule_choosedate" => ChooseDate(listButtons, payloadArgs.GroupName),
                "schedule_result" => GetResult(listButtons, payloadArgs.GroupName, payloadArgs.Date),
                _ => "Сервис временно не доступен, повторите попытку позже",
            };

            var homeButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, ButtonPayload.GetDefaultPayload(), "Главное меню", VkKeyboard.ButtonColorType.Blue);

            listButtons.Add(homeButton);

            var keyboard = VkKeyboard.CreateKeyboard(false, listButtons);

            SendMessage(client, userId, newMessage, keyboard);
        }


        private static string ChooseGroup(List<Button> listButtons)
        {
            var groups = ScheduleServiceAPI.GetGroupsName().Result;

            if (groups == null || groups.Count == 0)
            {
                return "Сервис временно не доступен, повторите попытку позже";
            }

            var message = "Выберите группу с помощью одной из кнопок";

            foreach (var group in groups)
            {
                var schedulePayload = ButtonPayload.CreateSchedulePayload(groupName: group.GroupName);
                var nextPayload = ButtonPayload.CreatePayload("schedule_choosedate", schedulePayload);

                var button = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, nextPayload, group.GroupName, VkKeyboard.ButtonColorType.White);

                listButtons.Add(button);
            }

            return message;
        }

        private static string ChooseDate(List<Button> listButtons, string groupName)
        {
            var dates = ScheduleServiceAPI.GetDatesSchedulesByGroup(groupName).Result;

            if (dates == null || dates.Count == 0)
            {
                return "Сервис временно не доступен, повторите попытку позже";
            }

            var message = "Выберите дату с помощью одной из кнопок";

            foreach (var date in dates)
            {
                var schedulePayload = ButtonPayload.CreateSchedulePayload(groupName: groupName, date: date.Date);
                var nextPayload = ButtonPayload.CreatePayload("schedule_result", schedulePayload);

                var button = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, nextPayload, date.Date, VkKeyboard.ButtonColorType.White);

                listButtons.Add(button);
            }

            return message;
        }

        private static string GetResult(List<Button> listButtons, string groupName, string date)
        {
            var lessons = ScheduleServiceAPI.GetScheduleByGroupName(groupName, date).Result;

            if (lessons == null || lessons.Count == 0)
            {
                return "Сервис временно не доступен, повторите попытку позже.";
            }

            var message = new StringBuilder($"Расписание группы {groupName} на {date}:\n\n");

            foreach (var lesson in lessons)
            {
                message.Append($"{lesson.Time} {lesson.Name} {lesson.Classroom} {lesson.TeacherName}\n");
            }

            var nextPayload = ButtonPayload.CreatePayload("schedule_choosegroup");

            var button = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, nextPayload, "Расписание пар", VkKeyboard.ButtonColorType.White);

            listButtons.Add(button);

            return message.ToString();
        }
    }
}