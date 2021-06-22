using System;
using System.Text.Json.Serialization;

namespace ScheduleController.GoogleSheets
{
    [Serializable]
    public class TokenSerializable
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("Issued")]
        public DateTime Issued { get; set; }

        [JsonPropertyName("IssuedUtc")]
        public DateTime IssuedUtc { get; set; }
    }
}
