using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OliWorkshop.ChatGptNet
{
    /// <summary>
    /// This model class sets the data responsed by endpoint.
    /// </summary>
    public class ChatResponseModel
    {
        /// <summary>
        /// Quick access the first choice. Often this will be unique choice.
        /// </summary>
        public Choice First => Choices[0];


        /// <summary>
        /// Response id.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Type of the object.
        /// </summary>

        [JsonPropertyName("object")]
        public string Object { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("created")]
        public long Created { get; set; }

        /// <summary>
        /// The choices offers by the model.
        /// </summary>
        [JsonPropertyName("choices")]
        public Choice[] Choices { get; set; }

        /// <summary>
        /// The tracking usage properties.
        /// </summary>
        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }

        /// <summary>
        /// If the response is success you got true.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// When the Success is false this property will be set.
        /// </summary>
        public string Reason { get; internal set; }
    }

    public partial class Choice
    {
        [JsonPropertyName("index")]
        public long Index { get; set; }

        [JsonPropertyName("message")]
        public MessageModel Message { get; set; }

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
