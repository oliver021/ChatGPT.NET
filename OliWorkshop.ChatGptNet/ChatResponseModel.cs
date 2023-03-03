using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OliWorkshop.ChatGptNet
{
    public partial class ModelRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("choices")]
        public Choice[] Choices { get; set; }

        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }
    }

    public partial class Choice
    {
        [JsonPropertyName("index")]
        public long Index { get; set; }

        [JsonPropertyName("message")]
        public ModelMessage Message { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }

    public class Usage
    {
        [JsonPropertyName("prompt_tokens")]
        public long PromptTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        public long CompletionTokens { get; set; }

        [JsonPropertyName("total_tokens")]
        public long TotalTokens { get; set; }
    }
}
