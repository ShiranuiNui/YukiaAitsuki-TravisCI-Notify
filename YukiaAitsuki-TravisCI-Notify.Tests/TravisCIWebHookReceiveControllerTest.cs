using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;
using YukiaAitsuki_TravisCI_Notify.Controllers;
using YukiaAitsuki_TravisCI_Notify.Twitter;

namespace YukiaAitsuki_TravisCI_Notify.Tests
{
    public class TravisCIWebHookReceiveControllerTest
    {
        [Fact]
        public async Task SuccessfulGetJsonAndPostTwitter()
        {
            string testdata = JsonConvert.SerializeObject(
                new
                {
                    result = "0",
                        url = "https://travis-ci.org/"
                });
            var twittermock = new Mock<ITwitterPost>();
            twittermock.Setup(x => x.PostSuccessfulStatus(It.IsAny<string>())).ReturnsAsync(1);
            var controller = new TravisCIWebHookReceiverController(twittermock.Object);
            var result = await controller.Post(testdata);
            Assert.IsType<OkResult>(result);
        }
    }
}