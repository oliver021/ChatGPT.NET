using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OliWorkshop.ChatGptNet
{
    public class ChatContext
    {
        public ChatContext(string openAiKey)
        {
            OpenAiKey = openAiKey;
        }

        public string OpenAiKey { get; }

        public async Task<ChatContextIteration> NextAsync(string message)
        {
            return null;
        }

        public async Task<ChatContextIteration> PushAsync(IEnumerable<ModelMessage> messages)
        {
            return null;
        }
    }
}
