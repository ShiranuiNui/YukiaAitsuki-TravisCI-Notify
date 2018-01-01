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
    public class TravisCIWebHookReceiverController : Controller
    {
        private IConfiguration configuration;
        public TravisCIWebHookReceiverController(IConfiguration _configration)
        {
            this.configuration = _configration;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string value)
        {
            string debug_signature = this.Request.Headers["Signature"][0];
            string payload = this.Request.Form["payload"];

            var verifier = new SignatureVerifier.SignatureVerifier();
            var verifyResult = await verifier.Post(payload, this.Request.Headers["Signature"][0]);
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