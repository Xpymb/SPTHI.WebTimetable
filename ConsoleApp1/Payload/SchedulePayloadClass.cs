using System;
using System.Text.Json.Serialization;

namespace ConsoleApp1.Payload
{
    [Serializable]
    public class SchedulePayloadClass
    {
        [JsonPropertyName("command")]
        public string Command { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("group_name")]
        public string GroupName { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }
    }
}
