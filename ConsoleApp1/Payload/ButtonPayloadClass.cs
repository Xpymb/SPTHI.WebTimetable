using System;
using System.Text.Json.Serialization;

namespace ConsoleApp1.Payload
{
    [Serializable]
    public class ButtonPayloadClass
    {
        [JsonPropertyName("button")]
        public string Button { get; set; }
    }
}