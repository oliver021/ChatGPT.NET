using System;
using System.Collections.Generic;
using System.Text;

namespace OliWorkshop.ChatGptNet
{
    public class ChatContextIteration
    {
        public ChatContextIteration(ChatResponseModel response)
        {
            Response = response;
        }

        public ChatResponseModel Response { get; }

        /// <summary>
        /// Quick access to the first choice's message.
        /// </summary>
        public string Message => Response.First.Message.Content;
    }
}
