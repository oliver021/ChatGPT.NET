using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OliWorkshop.ChatGptNet
{
    /// <summary>
    /// Chat API Class perform endpoint http comunication.
    /// </summary>
    public class ChatAPI
    {
        public HttpClient Client { get; set; }
        
        public string OpenAIKey { get; set; }

        /// <summary>
        /// Construction by api key
        /// </summary>
        /// <param name="key"></param>
        public ChatAPI(string key) { 
            OpenAIKey = key;
            Client = new HttpClient();
        }

        /// <summary>
        /// Basic param full construct
        /// </summary>
        /// <param name="client"></param>
        /// <param name="key"></param>
        public ChatAPI(HttpClient client, string key)
        {
            Client = client;
            OpenAIKey = key;
        }

        /// <summary>
        /// Help with chat context building task. The idea behind of this method is use following
        /// the next example:
        /// </summary>
        /// <example>
        /// await chatIntance.SendAsync(ModelMessage.BySystem("message..."), ModelMessage.ByUser("ask somethings..."));
        /// </example>
        /// <param name="message"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<ChatResponseModel> SendAllAsync(params MessageModel[] messages)
        {
            return SendAsync(messages);
        }

        /// <summary>
        /// Simple quick message sender method avoid setting temperature parameter and assume
        /// that the message is from user.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<ChatResponseModel> SendAsync(string message, CancellationToken cancellation = default)
        {
            return SendAsync(new []{MessageModel.ByUser(message)}, -1, cancellation);
        }

        /// <summary>
        /// Send a request for chat endpoints
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<ChatResponseModel> SendAsync(MessageModel[] messages, long temperature = -1, CancellationToken cancellation = default)
        {
            try
            {
                // create chat request
                var request = new ChatRequestModel();
                request.Model = OpenAIConst.CHAT_DEFAULT_MODEL;
                request.Messages = messages;
                
                // set the temperature param if is set
                if (temperature > 0)
                {
                    request.Temperature = temperature;
                }

                // create a body content
                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                
                // create a message instance
                var message = new HttpRequestMessage(HttpMethod.Post, OpenAIConst.CHAT_ENDPOINT);
                message.Content = content;
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", OpenAIKey);
                
                // send it
                var response = await Client.SendAsync(message, cancellation);
                
                // ensure property response
                response.EnsureSuccessStatusCode();
                
                // check cancellation
                cancellation.ThrowIfCancellationRequested();
                return JsonSerializer.Deserialize<ChatResponseModel>(await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException)
            {
                var response = new ChatResponseModel();
                response.Success = false;
                response.Reason = "Some did fail with http request";
                return response;
            }
            catch (JsonException)
            {
                var response = new ChatResponseModel();
                response.Success = false;
                response.Reason = "Json serializer couldn't work well";
                return response;
            }
            catch (OperationCanceledException)
            {
                var response = new ChatResponseModel();
                response.Success = false;
                response.Reason = "The operation was canceled by user or process.";
                return response;
            }
        }
    }
}
