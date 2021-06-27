using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.Payload
{
    public static class ButtonPayload
    {
        public static string CreatePayload(string body)
        {
            var payload = new ButtonPayloadClass
            {
                Button = body
            };

            var options = GetDefaultOptions();

            return JsonSerializer.Serialize(payload, options);
        }

        public static ButtonPayloadClass DeserializePayload(string json)
        {
            var options = GetDefaultOptions();

            return JsonSerializer.Deserialize<ButtonPayloadClass>(json, options);
        }

        public static string CreateSchedulePayload(SchedulePayloadClass body)
        {
            var options = GetDefaultOptions();

            return JsonSerializer.Serialize(body, options);
        }

        public static SchedulePayloadClass DeserializeSchedulePayload(string json)
        {
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
    }
}
