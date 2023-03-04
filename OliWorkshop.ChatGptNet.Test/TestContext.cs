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
            var response = await Ctx.NextAsync("Cloud tell me how avoid feel me bored?");

            Console.WriteLine(response?.Message);
        }

        [Test]
        public async Task Test2()
        {
            var response = await Ctx.PushAsync(
               Model 
            );

            Console.WriteLine(response?.Message);
        }
    }
}
