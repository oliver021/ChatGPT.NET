namespace OliWorkshop.ChatGptNet
{
    using System.Text.Json.Serialization;

    public partial class ModelMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
