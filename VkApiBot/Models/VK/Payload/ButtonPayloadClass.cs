using System;
using System.Text.Json.Serialization;

namespace VkApiBot.Models.VK.Payload
{
    [Serializable]
    public class ButtonPayloadClass
    {
        [JsonPropertyName("button")]
        public string Button { get; set; }
    }
}
