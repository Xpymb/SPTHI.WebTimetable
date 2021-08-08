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

        public override List<string> Payload => new() { "schedule", "schedule_choosegrouptype", "schedule_chooseclass", "schedule_chooseweektype", "schedule_choosegroup", "schedule_choosedate", "schedule_result" };

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
                "schedule_choosegrouptype" => ChooseGroupType(listButtons),
                "schedule_chooseclass" => ChooseClass(listButtons, payloadArgs.GroupType),
                "schedule_choosegroup" => ChooseGroup(listButtons, payloadArgs.GroupType, payloadArgs.Class),
                "schedule_chooseweektype" => ChooseWeekType(listButtons, payloadArgs.GroupType, payloadArgs.Class, payloadArgs.GroupName),
                "schedule_choosedate" => ChooseDate(listButtons, payloadArgs.GroupName, payloadArgs.GroupType, payloadArgs.Class, payloadArgs.WeekType),
                "schedule_result" => GetResult(listButtons, payloadArgs.GroupName, payloadArgs.Date, payloadArgs.WeekType),
                _ => "Сервис временно не доступен, повторите попытку позже (ооаоа)",
            };

            var homeButton = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, ButtonPayload.GetDefaultPayload(), "Главное меню", VkKeyboard.ButtonColorType.Blue);

            listButtons.Add(homeButton);

            var keyboard = VkKeyboard.CreateKeyboard(false, listButtons);

            SendMessage(client, userId, newMessage, keyboard);
        }

        private static string ChooseGroupType(List<Button> listButtons)
        {
            var groupsType = ScheduleServiceAPI.GetGroupType().Result;

            if (groupsType == null || groupsType.Count == 0)
            {
                return "Сервис временно не доступен, повторите попытку позже";
            }

            var message = "Выберите направление с помощью одной из кнопок";

            foreach (var groupType in groupsType)
            {
                var schedulePayload = ButtonPayload.CreateSchedulePayload(groupType: groupType.GroupType);
                var nextPayload = ButtonPayload.CreatePayload("schedule_chooseclass", schedulePayload);

                var button = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, nextPayload, groupType.GroupType, VkKeyboard.ButtonColorType.White);

                listButtons.Add(button);
            }

            return message;
        }

        private static string ChooseClass(List<Button> listButtons, string groupType)
        {
            var classesList = ScheduleServiceAPI.GetClasses(groupType).Result;

            if (classesList == null || classesList.Count == 0)
            {
                return "Сервис временно не доступен, повторите попытку позже";
            }

            var message = "Выберите курс с помощью одной из кнопок";

            foreach (var _class in classesList)
            {
                var schedulePayload = ButtonPayload.CreateSchedulePayload(_class: _class.Class, groupType: groupType);
                var nextPayload = ButtonPayload.CreatePayload("schedule_choosegroup", schedulePayload);

                var button = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, nextPayload, _class.Class, VkKeyboard.ButtonColorType.White);

                listButtons.Add(button);
            }

            return message;
        }

        private static string ChooseGroup(List<Button> listButtons, string groupType, string _class)
        {
            var groups = ScheduleServiceAPI.GetGroupsName(groupType, _class).Result;

            if (groups == null || groups.Count == 0)
            {
                return "Сервис временно не доступен, повторите попытку позже";
            }

            var message = "Выберите группу с помощью одной из кнопок";

            if (groupType.Contains("Колледж"))
            {
                foreach (var group in groups)
                {
                    var schedulePayload = ButtonPayload.CreateSchedulePayload(groupName: group.GroupName, _class: _class, groupType: groupType, weekType: groupType);
                    var nextPayload = ButtonPayload.CreatePayload("schedule_choosedate", schedulePayload);

                    var button = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, nextPayload, group.GroupName, VkKeyboard.ButtonColorType.White);

                    listButtons.Add(button);
                }
            }
            else
            {
                foreach (var group in groups)
                {
                    var schedulePayload = ButtonPayload.CreateSchedulePayload(groupName: group.GroupName, _class: _class, groupType: groupType);
                    var nextPayload = ButtonPayload.CreatePayload("schedule_chooseweektype", schedulePayload);

                    var button = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, nextPayload, group.GroupName, VkKeyboard.ButtonColorType.White);

                    listButtons.Add(button);
                }
            }

            return message;
        }

        private static string ChooseWeekType(List<Button> listButtons, string groupType, string _class, string groupName)
        {
            var weeksTypeList = ScheduleServiceAPI.GetWeeksType(groupType, _class, groupName).Result;

            if (weeksTypeList == null || weeksTypeList.Count == 0)
            {
                return "Сервис временно не доступен, повторите попытку позже";
            }

            var message = "Выберите тип недели с помощью одной из кнопок";

            foreach (var weekType in weeksTypeList)
            {
                var schedulePayload = ButtonPayload.CreateSchedulePayload(weekType: weekType.WeeksType, groupType: groupType, _class: _class, groupName: groupName);
                var nextPayload = ButtonPayload.CreatePayload("schedule_choosedate", schedulePayload);

                var button = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, nextPayload, weekType.WeeksType, VkKeyboard.ButtonColorType.White);

                listButtons.Add(button);
            }

            return message;
        }

        private static string ChooseDate(List<Button> listButtons, string groupName, string groupType, string _class, string weekType)
        {
            var dates = ScheduleServiceAPI.GetDatesSchedulesByGroup(groupName, groupType, _class, weekType).Result;

            if (dates == null || dates.Count == 0)
            {
                return "Сервис временно не доступен, повторите попытку позже";
            }

            var message = "Выберите дату с помощью одной из кнопок";

            foreach (var date in dates)
            {
                var schedulePayload = ButtonPayload.CreateSchedulePayload(groupName: groupName, date: date.Date, _class: _class, groupType: groupType, weekType: weekType);
                var nextPayload = ButtonPayload.CreatePayload("schedule_result", schedulePayload);

                var button = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, nextPayload, date.Date, VkKeyboard.ButtonColorType.White);

                listButtons.Add(button);
            }

            return message;
        }

        private static string GetResult(List<Button> listButtons, string groupName, string date, string weekType)
        {
            var lessons = ScheduleServiceAPI.GetScheduleByGroupName(groupName, date, weekType).Result;

            if (lessons == null || lessons.Count == 0)
            {
                return "Сервис временно не доступен, повторите попытку позже.";
            }

            var message = new StringBuilder($"Расписание группы {groupName} на {date}:\n\n");

            foreach (var lesson in lessons)
            {
                message.Append($"{lesson.Time} {lesson.Name} {lesson.Classroom} {lesson.TeacherName}\n");
            }

            var nextPayload = ButtonPayload.CreatePayload("schedule_choosegrouptype");

            var button = VkKeyboard.CreateButton(VkKeyboard.ButtonActionType.Text, nextPayload, "Расписание пар", VkKeyboard.ButtonColorType.White);

            listButtons.Add(button);

            return message.ToString();
        }
    }
}