using System.Collections.Generic;
using System.Text.Json;
using VkApiBot.Models.VK.Payload;

namespace VkApiBot.Models.VK.Keyboard
{
    public static class VkKeyboard
    {
        public enum ButtonColor
        {
            White,
            Blue
        }

        public enum ButtonAction
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

        public static Button CreateButton(ButtonAction type, string payload, string label, ButtonColor color, string link = null)
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

        public static string GetColorValue(ButtonColor color)
        {
            var colorValue = "";

            switch(color)
            {
                case ButtonColor.White:
                    colorValue = "secondary";
                    break;

                case ButtonColor.Blue:
                    colorValue = "primary";
                    break;
            }

            return colorValue;
        }

        private static string GetActionValue(ButtonAction type)
        {
            return type switch
            {
                ButtonAction.Text => "text",
                ButtonAction.OpenLink => "open_link",
                ButtonAction.Location => "location",
                ButtonAction.VKPay => "vkpay",
                ButtonAction.VKApps => "open_app",
                ButtonAction.Callback => "callback",
                _ => "",
            };
        }
    }
}
