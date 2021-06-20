using System;
using System.Collections.Generic;
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
                case "call":
                {
                    var nextCallPayload = ButtonPayload.CreatePayload($"call_next");
                    var allCallsPayload = ButtonPayload.CreatePayload($"call_all");
                    var allNextCallsPayload = ButtonPayload.CreatePayload($"call_all_next");

                    listButtons.Add(new Button
                    {
                        Action = new VK.Keyboard.Action { ActionType = "text", Payload = nextCallPayload, Label = "Следующий звонок" },
                        Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White),
                    });
                    listButtons.Add(new Button
                    {
                        Action = new VK.Keyboard.Action { ActionType = "text", Payload = allCallsPayload, Label = "Все звонки на сегодня" },
                        Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White),
                    });
                    listButtons.Add(new Button
                    {
                        Action = new VK.Keyboard.Action { ActionType = "text", Payload = allNextCallsPayload, Label = "Все следующие звонки на сегодня" },
                        Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White),
                    });

                    if (listButtons.Count > 0)
                    {
                        msg = "Выберите действие с помощью одной из кнопок.";
                    }
                    else
                    {
                        msg = "Ошибка сервиса.";
                    }

                    break;
                }
                case "call_next":
                {
                    var call = CallControllerServiceAPI.GetNextCall();

                    if(call != null)
                    {
                        msg = $"Следующий звонок в {call.DateTime} - {call.Name}";
                    }
                    else
                    {
                        msg = "Внутренняя ошибка сервиса, попробуйте позже.";
                    }

                    break;
                }
                case "call_all":
                {
                    var calls = CallControllerServiceAPI.GetListCalls().Result;

                    if (calls != null)
                    {
                        msg = $"Список звонков на сегодня:\n\n";

                        foreach (var call in calls)
                        {
                            msg += $"{call.DateTime} - {call.Name}\n";

                            if (call.Name.Contains("конец"))
                            {
                                msg += "\n";
                            }
                        }
                    }
                    else
                    {
                        msg = "Внутренняя ошибка сервиса, попробуйте позже.";
                    }

                    break;
                }
                case "call_all_next":
                {
                    var calls = CallControllerServiceAPI.GetListNextCalls().Result;

                    if (calls != null)
                    {
                        msg = $"Список следующих звонков на сегодня:\n\n";

                        foreach (var call in calls)
                        {
                            msg += $"{call.DateTime} - {call.Name}\n";

                            if (call.Name.Contains("конец"))
                            {
                                msg += "\n";
                            }
                            }
                    }
                    else
                    {
                        msg = "Внутренняя ошибка сервиса, попробуйте позже.";
                    }

                    break;
                }
            }

            var nextPayload = ButtonPayload.CreatePayload("call");

            listButtons.Add(new Button
            {
                Action = new VK.Keyboard.Action { ActionType = "text", Payload = nextPayload, Label = "Расписание звонков" },
                Color = VkKeyboard.GetColorValue(VkKeyboard.ButtonColor.White),
            });

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