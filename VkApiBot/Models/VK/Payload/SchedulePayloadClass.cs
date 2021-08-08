using System;
using System.Text.Json.Serialization;

namespace VkApiBot.Models.VK.Payload
{
    [Serializable]
    public class SchedulePayloadClass
    {
        [JsonPropertyName("group_type")]
        public string GroupType { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("group_name")]
        public string GroupName { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("week_type")]
        public string WeekType { get; set; }
    }
}
