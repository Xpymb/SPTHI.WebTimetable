using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.VkKeyboard
{
    public static class VkKeyboard
    {
        public enum ButtonColor
        {
            White,
            Blue
        }

        public static string CreateKeyaboard(bool oneTime, string[] labels, ButtonColor[] colors)
        {
            var Buttons = new Button[labels.Length][];
            

            for(int i = 0; i < labels.Length; i++)
            {
                Buttons[i] = new Button[1];
                var ColorValue = GetColorValue(colors[i]);

                Buttons[i][0] = new Button { Action = new Action { ActionType = "text", Payload = "{\"\button\": \"1\"}", Label = labels[i] }, Color = ColorValue };
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

        private static string GetColorValue(ButtonColor color)
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
