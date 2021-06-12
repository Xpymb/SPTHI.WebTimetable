using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VkApiBot.Models.Keyboard
{
    public static class VkKeyboard
    {
        public enum ButtonColor
        {
            White,
            Blue
        }

        public static string DefaultPayload = "{\"\button\": \"1\"}";

        public static string CreateKeyaboard(bool oneTime, List<Button> buttons)
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
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            return JsonSerializer.Serialize<VkKeyboardClass>(keyboard, options);
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
    }
}
