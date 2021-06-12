using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VkApiBot.Models.Keyboard
{
    [Serializable]
    public class VkKeyboardClass
    {
        [JsonPropertyName("one_time")]
        public bool OneTime { get; set; }

        [JsonPropertyName("buttons")]
        public Button[][] Buttons { get; set; }
    }

    [Serializable]
    public class Button
    {
        [JsonPropertyName("action")]
        public Action Action { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }
    }

    [Serializable]
    public class Action
    {
        [JsonPropertyName("type")]
        public string ActionType { get; set; }

        [JsonPropertyName("payload")]
        public string Payload { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }
    }
}
