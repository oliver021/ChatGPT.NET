namespace OliWorkshop.ChatGptNet.Test
{
    public class BasicTest
    {
        ChatAPI Client { get; set; }

        [SetUp]
        public void Setup()
        {
            Client = new ChatAPI("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");
        }

        [Test]
        public async Task Test()
        {
            var response = await Client.SendAsync(new[] { 
                ModelMessage.ByUser("Hello!! tell me a joke")
            });

            Console.WriteLine(response?.Choices[0].Message);
        }
    }
}