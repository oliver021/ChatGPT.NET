using System;
using System.Collections.Generic;
using System.Text;

namespace OliWorkshop.ChatGptNet
{
    public static class OpenAIConst
    {
        public const string CHAT_ENDPOINT = "https://api.openai.com/v1/chat/completions";
        public const string CHAT_35_TURBO = "gpt-3.5-turbo";
        public const string CHAT_LATEST = CHAT_35_TURBO;
        public const string CHAT_DEFAULT_MODEL = CHAT_35_TURBO;
    }
}
