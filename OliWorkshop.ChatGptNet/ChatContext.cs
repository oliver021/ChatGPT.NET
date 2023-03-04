using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OliWorkshop.ChatGptNet
{
    public class ChatContext
    {
        public ChatAPI ChatAPI { get; set; }

        public ChatContext(string openAiKey)
        {
            ChatAPI= new ChatAPI(openAiKey);
        }

        public ChatContext(string openAiKey, HttpClient httpClient)
        {
            ChatAPI= new ChatAPI(httpClient, openAiKey);
        }

        public string OpenAiKey { get; }

        public async Task<ChatContextIteration> NextAsync(string message)
        {
            return new ChatContextIteration(await ChatAPI.SendAsync(message));
        }

        public async Task<ChatContextIteration> PushAsync(params MessageModel[] messages)
        {
            return new ChatContextIteration(await ChatAPI.SendAsync(messages));
        }

        public async Task<ChatContextIteration> PushAsync(IEnumerable<MessageModel> messages)
        {
            return null;
        }
    }
}
