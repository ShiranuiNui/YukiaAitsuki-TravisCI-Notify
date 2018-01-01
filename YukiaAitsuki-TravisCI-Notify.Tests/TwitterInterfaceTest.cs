using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using YukiaAitsuki_TravisCI_Notify.Twitter;

namespace YukiaAitsuki_TravisCI_Notify.Tests
{
    public class TwitterInterfaceTest
    {
        public TwitterInterfaceTest()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("Test_Config.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        [Fact]
        public async Task Post_OK_Test()
        {
            var twitterClient = new TwitterPost(Configuration);
            var postResult = await twitterClient.PostSuccessfulStatus("https://twitter.com/shirasayav5");
            Assert.True(postResult.HasValue);
            System.Threading.Thread.Sleep(15000);
            if (postResult.HasValue)
            {
                await twitterClient.DeleteStatus(postResult.Value);
            }
        }
    }
}