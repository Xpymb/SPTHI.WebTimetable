﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace VkApiBot.Models.VK.Payload
{
    public static class ButtonPayload
    {
        public static string CreatePayload(string body)
        {
            var payload = new ButtonPayloadClass
            {
                Button = body
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            return JsonSerializer.Serialize<ButtonPayloadClass>(payload, options);
        }

        public static ButtonPayloadClass DeserializePayload(string json)
        {
            return JsonSerializer.Deserialize<ButtonPayloadClass>(json);
        }
    }
}