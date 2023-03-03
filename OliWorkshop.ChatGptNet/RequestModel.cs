namespace OliWorkshop.ChatGptNet
{
    using System;
    using System.Collections.Generic;

    using System.Text.Json;
    using System.Text.Json.Serialization;

    public partial class ModelRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("temperature")]
        public long Temperature { get; set; }

        [JsonPropertyName("messages")]
        public ModelMessage[] Messages { get; set; }
    }
}
