using System;
using System.Text.Json.Serialization;

namespace VkApiBot.Models.VK.Payload
{
    [Serializable]
    public class ButtonPayloadClass
    {
        [JsonPropertyName("command")]
        public string Command { get; set; }

        [JsonPropertyName("logic")]
        public string Logic { get; set; }
    }
}
