namespace OliWorkshop.ChatGptNet
{
    using System.Diagnostics;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The model message to send requests.
    /// </summary>
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class MessageModel
    {
        public const string UserAuthor = "user";
        public const string AssistantAuthor = "assistant";
        public const string SystemAuthor = "system";

        public static MessageModel ByUser(string message)
        {
            return new MessageModel(UserAuthor, message);
        }

        public static MessageModel ByAssitant(string message)
        {
            return new MessageModel(AssistantAuthor, message);
        }

        public static MessageModel BySystem(string message)
        {
            return new MessageModel(SystemAuthor, message);
        }

        public MessageModel(string role, string content)
        {
            Role = role;
            Content = content;
        }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"role: {Role}; {Content}";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
