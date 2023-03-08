using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OliWorkshop.ChatGptNet.Test
{
    public class TestContext
    {
        ChatContext Ctx { get; set; }

        [SetUp]
        public void Setup()
        {
            Ctx = new ChatContext(Environment.GetEnvironmentVariable("OpenAIKey"));
        }

        [Test]
        public async Task Test()
        {
            var response = await Ctx.NextAsync("Could tell me how avoid feel me bored?");

            Console.WriteLine(response?.Message);
        }

        [Test]
        public async Task TestFollowChat()
        {
            var response = await Ctx.PushAsync(MessageModel.ByUser("I like the red color for my car!"));

            if (response.IsSuccess is false)
            {
                Console.WriteLine(response.ErrorMessage);
                return;
            }

            Console.WriteLine(response?.Message);

            response = await Ctx.PushAsync(MessageModel.ByUser("Which color I told the previous message"));

            Console.WriteLine(response?.Message);
        }

        [Test]
        public async Task TestFollowUsingNext()
        {
            var response = await Ctx.NextAsync("I like the red color for my car!");

            if (response.IsSuccess is false)
            {
                Console.WriteLine("failed...");
                Console.WriteLine(response.ErrorMessage);
                return;
            }

            Console.WriteLine(response?.Message);

            response = await Ctx.NextAsync("Which color I told the previous message");

            Console.WriteLine(response?.Message);
        }

        [Test]
        public async Task TestAndDebugHistoryOutput()
        {
            var response = await Ctx.NextAsync("I want to tell you the blue is my favorite color for my rug!");

            if (response.IsSuccess is false)
            {
                Console.WriteLine("failed...");
                Console.WriteLine(response.ErrorMessage);
                return;
            }

            await Ctx.NextAsync("Which color I told the previous message");

            Console.WriteLine(JsonConvert.SerializeObject(Ctx.GetMessageHistory()));
        }

        [Test]
        public async Task TestFollowUsingPushSequence()
        {
            var response = await Ctx.PushAsync(MessageModel.ByUser("I like the red color for my car!"));

            Console.WriteLine(response?.Message);

            response = await Ctx.PushAsync(
                MessageModel.ByUser("I like the red color for my car!"),
                MessageModel.ByAssitant(response?.Message),
                MessageModel.ByUser("Which color I told the previous message"));

            Console.WriteLine(response?.Message);

        }

        [Test]
        public async Task TestFollowChat3()
        {
            var response = await Ctx.NextAsync("I like the red color for my car!");

            Console.WriteLine(response?.Message);

            response = await Ctx.NextAsync("Which color I told the previous message");

            Console.WriteLine(response?.Message);
        }
    }
}
