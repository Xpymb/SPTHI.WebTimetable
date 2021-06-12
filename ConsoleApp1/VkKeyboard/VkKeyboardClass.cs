using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp1.VkKeyboard
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
    public class Buttons
    {
        [JsonPropertyName("")]
        public Button[] Button { get; set; }
    }

    [Serializable]
    public class Button
    {
        [JsonPropertyName("action")]
        public Action Action { get; set; }
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
