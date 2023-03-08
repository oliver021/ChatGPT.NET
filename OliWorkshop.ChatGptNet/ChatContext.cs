using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OliWorkshop.ChatGptNet
{
    /// <summary>
    /// This is a powerful tool for managing chats with endpoints
    /// using the OpenAI API. With its two main methods - next and push - ChatContext
    /// allows users to easily send messages and communicate with the
    /// chat.3.5.turbo endpoint. The next method is used to send a message to the endpoint,
    /// while the push method is used to send a group of messages to the endpoint. 
    /// By leveraging the power of OpenAI, ChatContext makes it easy to have natural,
    /// engaging conversations with groups of people. 
    /// 
    /// The ChatContext class is the main container for all chat functionality with OpenAI's GPT model. 
    /// It provides a seamless way to use GPT Turbo and keeps track of message history to build context 
    /// for each iteration. The class uses the OpenAI SDK to implement new features and takes in a token 
    /// and system message to start a chat session. Once initialized, you can use the NextAsync method 
    /// to send a message to the endpoint and receive a response from the GPT model. The Reset method 
    /// clears the message history and starts a new chat session, while the GetMessageHistory method 
    /// provides access to the message history.
    /// </summary>
    public class ChatContext
    {
        /// <summary>
        /// The chat api instance to communicate with endpoints.
        /// </summary>
        ChatAPI ChatAPI { get; set; }

        internal List<Tuple<string, string>> Messages { get; set; } = new List<Tuple<string, string>>();

        /// <summary>
        /// Initiualize only with OpenAI API Key.
        /// </summary>
        /// <param name="openAiKey"></param>
        public ChatContext(string openAiKey)
        {
            ChatAPI= new ChatAPI(openAiKey);
        }

        /// <summary>
        /// Initialized with Key and Instance of http client.
        /// </summary>
        /// <param name="openAiKey"></param>
        /// <param name="httpClient"></param>
        public ChatContext(string openAiKey, HttpClient httpClient)
        {
            ChatAPI= new ChatAPI(httpClient ?? new HttpClient(), openAiKey);
        }

        /// <summary>
        /// Full initialization for Chat Context.
        /// </summary>
        /// <param name="openAiKey"></param>
        /// <param name="systemMsg"></param>
        /// <param name="httpClient"></param>
        public ChatContext(string openAiKey, string systemMsg, HttpClient httpClient = null)
        {
            ChatAPI = new ChatAPI(httpClient, openAiKey);
            Messages.Add(new Tuple<string, string>(MessageModel.SystemAuthor, systemMsg));
        }

        /// <summary>
        /// Send a message to interact with chat.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ChatContextIteration> NextAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("The message string cannot be null or white space message");

            // store the user message before it'll send
            Messages.Add(Tuple.Create(MessageModel.UserAuthor, message));

            // send it!!
            var response = new ChatContextIteration(await ChatAPI.SendAsync(GetMessages()));

            // record the message to send again in the next iteration
            Messages.Add(Tuple.Create(MessageModel.AssistantAuthor, response.Message));

            return response;
        }

        /// <summary>
        /// Put meny message at same time to create a context
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public async Task<ChatContextIteration> PushAsync(params MessageModel[] messages)
        {
            return new ChatContextIteration(await ChatAPI.SendAsync(messages));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="temperature"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<ChatContextIteration> PushAsync(IEnumerable<MessageModel> messages, long temperature = -1, CancellationToken cancellation = default)
        {
            return new ChatContextIteration(await ChatAPI.SendAsync(messages.ToArray(), temperature, cancellation));
        }

        /// <summary>
        /// Get message history from context records.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MessageModel> GetMessageHistory() 
            // simple map
            => Messages.Select(m => new MessageModel(m.Item1, m.Item2));

        /// <summary>
        /// Sets or changes the position of the system message at the top of the conversation.
        /// Use with caution as this may result in unexpected behavior.
        /// </summary>
        /// <param name="message"></param>
        public void SetOrChangeMessageSystem(string message)
        {
            if (Messages.Count > 0)
            {
                if (Messages[0].Item1.Equals(MessageModel.SystemAuthor))
                {
                    Messages = new List<Tuple<string, string>>()
                    {
                        new Tuple<string, string>(MessageModel.SystemAuthor, message)
                    }
                    .Concat(Messages)
                    .ToList();
                }
                else
                {
                    Messages[0] = new Tuple<string, string>(MessageModel.SystemAuthor, message);
                }
            }
            else
            {
                Messages.Add(new Tuple<string, string>(MessageModel.SystemAuthor, message));
            }
        }

        /// <summary>
        /// Delete message history for get started again with same instance.
        /// Would be useful to recycle instance and avoid enffort in merory usage system.
        /// </summary>
        public void Reset() => Messages.Clear();

        /// <summary>
        /// Helper to set the message roll.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private MessageModel[] GetMessages()
        // map record to message models, It's a walk in the park
        => Messages.Select(m => new MessageModel(m.Item1, m.Item2)).ToArray();
    }
}
