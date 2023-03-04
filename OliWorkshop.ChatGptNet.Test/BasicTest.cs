namespace OliWorkshop.ChatGptNet.Test
{
    public class BasicTest
    {
        ChatAPI Client { get; set; }

        [SetUp]
        public void Setup()
        {
            Client = new ChatAPI(Environment.GetEnvironmentVariable("OpenAIKey"));
        }

        [Test]
        public async Task Test()
        {
            var response = await Client.SendAsync(new[] { 
                MessageModel.ByUser("Hello!! tell me a joke")
            });

            Console.WriteLine(response?.Choices[0].Message);
        }
    }
}