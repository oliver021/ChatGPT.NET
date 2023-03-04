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
            Ctx = new ChatContext("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");
        }

        [Test]
        public async Task Test()
        {
            var response = await Ctx.NextAsync("Cloud tell me how avoid feel me bored?");

            Console.WriteLine(response?.Message);
        }
    }
}
