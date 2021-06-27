using System.Text.Json;

namespace VkApiBot.Models.VK.Payload
{
    public static class ButtonPayload
    {
        public static string CreatePayload(string command, string logic = null)
        {
            var payload = new ButtonPayloadClass
            {
                Command = command,
                Logic = logic,
            };

            var options = GetDefaultOptions();

            return JsonSerializer.Serialize(payload, options);
        }

        public static ButtonPayloadClass DeserializePayload(string json)
        {
            if(json == null)
            {
                return new ButtonPayloadClass();
            }

            var options = GetDefaultOptions();

            return JsonSerializer.Deserialize<ButtonPayloadClass>(json, options);
        }

        public static string CreateSchedulePayload(string groupName = null, string _class = null, string date = null)
        {
            var payload = new SchedulePayloadClass
            {
                GroupName = groupName,
                Class = _class,
                Date = date,
            };

            var options = GetDefaultOptions();

            return JsonSerializer.Serialize(payload, options);
        }

        public static SchedulePayloadClass DeserializeSchedulePayload(string json)
        {
            if(json == null)
            {
                return new SchedulePayloadClass();
            }

            var options = GetDefaultOptions();

            return JsonSerializer.Deserialize<SchedulePayloadClass>(json, options);
        }

        private static JsonSerializerOptions GetDefaultOptions()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            return options;
        }

        public static string GetDefaultPayload()
        {
            return CreatePayload("");
        }
    }
}
