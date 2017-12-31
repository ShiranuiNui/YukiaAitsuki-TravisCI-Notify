using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTweet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YukiaAitsuki_TravisCI_Notify.SignatureVerifier;

namespace YukiaAitsuki_TravisCI_Notify.Controllers
{

    [Route("receiver/[controller]")]
    public class TravisCIWebHookReceiveController : Controller
    {
        private IConfiguration configuration;
        public TravisCIWebHookReceiveController(IConfiguration _configration)
        {
            this.configuration = _configration;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            var verifier = new SignatureVerifier.SignatureVerifier();
            var verifyResult = await verifier.Post(value, this.Request.Headers["Signature"]);
            if (!verifyResult)
            {
                return Unauthorized();
            }
            string apiKey = this.configuration.GetValue("TWITTER_APIKEY", "");
            string apiSecret = this.configuration.GetValue("TWITTER_APISECRET", "");
            string accessToken = this.configuration.GetValue("TWITTER_ACCESSTOKEN", "");
            string accessSecret = this.configuration.GetValue("TWITTER_ACCESSSECRET", "");

            var twitterToken = new CoreTweet.Tokens(Tokens.Create(apiKey, apiSecret, accessToken, accessSecret));
            var tweetresult = await twitterToken.Statuses.UpdateAsync(new { status = "認証を確認しました" });
            return Ok();
        }
    }
}