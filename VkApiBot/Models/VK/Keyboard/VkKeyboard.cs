using System.Collections.Generic;
using System.Text.Json;
using VkApiBot.Models.VK.Payload;

namespace VkApiBot.Models.VK.Keyboard
{
    public static class VkKeyboard
    {
        public enum ButtonColorType
        {
            White,
            Blue
        }

        public enum ButtonActionType
        {
            Text,
            OpenLink,
            Location,
            VKPay,
            VKApps,
            Callback
        }

        public static string CreateKeyboard(bool oneTime, List<Button> buttons)
        {
            var Buttons = new Button[buttons.Count][];
            
            for(int i = 0; i < buttons.Count; i++)
            {
                Buttons[i] = new Button[1];

                Buttons[i][0] = buttons[i];
            }

            var keyboard = new VkKeyboardClass
            {
                OneTime = oneTime,
                Buttons = Buttons
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Serialize<VkKeyboardClass>(keyboard, options);
        }

        public static Button CreateButton(ButtonActionType type, string payload, string label, ButtonColorType color, string link = null)
        {
            var actionVal = GetActionValue(type);

            var action = new Action
            {
                ActionType = actionVal,
                Label = label,
                Payload = payload,
                Link = link,
            };

            return new Button { Action = action, Color = GetColorValue(color) };
        }

        public static string GetColorValue(ButtonColorType color)
        {
            var colorValue = "";

            switch(color)
            {
                case ButtonColorType.White:
                    colorValue = "secondary";
                    break;

                case ButtonColorType.Blue:
                    colorValue = "primary";
                    break;
            }

            return colorValue;
        }

        private static string GetActionValue(ButtonActionType type)
        {
            return type switch
            {
                ButtonActionType.Text => "text",
                ButtonActionType.OpenLink => "open_link",
                ButtonActionType.Location => "location",
                ButtonActionType.VKPay => "vkpay",
                ButtonActionType.VKApps => "open_app",
                ButtonActionType.Callback => "callback",
                _ => "",
            };
        }
    }
}
