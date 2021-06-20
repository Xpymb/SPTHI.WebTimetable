using System;
using System.Collections.Generic;
using VkApiBot.Controllers;
using VkApiBot.gRPC.Services;
using VkApiBot.Models.VK.Keyboard;
using VkApiBot.Models.VK.Payload;
using VkNet;

namespace VkApiBot.Models.Commands
{
    public class ScheduleCommand : Command
    {
        public override List<string> Name => new() { "Расписание" };

        public override string Message => "Я умный чат-бот из системы БАРСик, разработанный выпускником КС-47д, состоящим в " +
                                          "лаборатории Мобильной робототехники";

        public override List<string> Payload => new() { "schedule", "schedule_choosegroup", "schedule_choosedate", "schedule_result" };
        public override void Execute(Message message, VkApi client)
        {
            var payload = ButtonPayload.DeserializePayload(message.Payload);

            ExecutePayload(message, payload.Button, client);
        }

        public override void ExecutePayload(Message message, string payload, VkApi client)
        {
            var userId = message.FromId;
            var listButtons = new List<Button>();
            var msg = ".";

            var payloadArgs = payload.Split(" ");
            var command = payloadArgs[0];

            switch (command)
            {
                case "schedule_choosegroup":
                {
                    var groupsName = ScheduleServiceAPI.GetGroupsName().Result;
                    
                    foreach(var groupName in groupsName)
                    {
                        var nextPayload = ButtonPayload.CreatePayload($"schedule_choosedate {groupName.GroupName}");

                        listButtons.Add(new Button
                        {
                            Action = new VK.Keyboard.Action { ActionType = "text", Payload = nextPayload, Label = groupName.GroupName },
                            Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White),
                        });
                    }

                    if(listButtons.Count > 0)
                    {
                        msg = "Выберите группу с помощью одной из кнопок.";
                    }
                    else
                    {
                        msg = "Ошибка сервиса.";
                    }
                    
                    break;
                }
                case "schedule_choosedate":
                {
                    var groupName = payloadArgs[1];

                    var dates = ScheduleServiceAPI.GetDatesSchedulesByGroup(groupName).Result;
                    
                    foreach (var date in dates)
                    {
                        var nextPayload = ButtonPayload.CreatePayload($"schedule_result {message.Text} {date.Date}");

                        listButtons.Add(new Button
                        {
                            Action = new VK.Keyboard.Action { ActionType = "text", Payload = nextPayload, Label = date.Date },
                            Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White),
                        });
                    }

                    if (listButtons.Count > 0)
                    {
                        msg = "Выберите дату с помощью одной из кнопок.";
                    }
                    else
                    {
                        msg = "Внутренняя ошибка сервиса, попробуйте позже.";
                    }

                    break;
                }
                case "schedule_result":
                {
                    var groupName = payloadArgs[1];
                    var date = payloadArgs[2];

                    var lessons = ScheduleServiceAPI.GetScheduleByGroupName(groupName, date).Result;

                    msg = $"Расписание группы {groupName} на {date}:\n\n";

                    foreach (var lesson in lessons)
                    {
                        msg += $"{lesson.Time} {lesson.Name} {lesson.Classroom} {lesson.TeacherName}\n";
                    }

                    if (lessons.Count == 0)
                    {
                        msg = "Внутренняя ошибка сервиса, попробуйте позже.";
                    }

                    var nextPayload = ButtonPayload.CreatePayload("schedule_choosegroup");

                    listButtons.Add(new Button
                    {
                        Action = new VK.Keyboard.Action { ActionType = "text", Payload = nextPayload, Label = "Расписание" },
                        Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White),
                    });

                    break;
                }
            }

            listButtons.Add(new Button
            {
                Action = new VK.Keyboard.Action { ActionType = "text", Payload = VkKeyboard.DefaultPayload, Label = "Главное меню" },
                Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.Blue),
            });

            var keyboard = VkKeyboard.CreateKeyaboard(false, listButtons);

            SendMessage(client, userId, msg, keyboard);
        }
    }
}