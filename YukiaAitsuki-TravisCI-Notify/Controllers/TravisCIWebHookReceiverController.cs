using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTweet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YukiaAitsuki_TravisCI_Notify.SignatureVerifier;
using YukiaAitsuki_TravisCI_Notify.Twitter;

namespace YukiaAitsuki_TravisCI_Notify.Controllers
{

    [Route("receiver/[controller]")]
    public class TravisCIWebHookReceiverController : Controller
    {
        private ITwitterPost twitterClient;
        public TravisCIWebHookReceiverController(ITwitterPost _twitterClient)
        {
            this.twitterClient = _twitterClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string value)
        {
            string payload = string.IsNullOrEmpty(value) ? this.Request.Form["payload"].ToString() : value;

            var jsonObject = JObject.Parse(payload);
            int resultvalue = (int) jsonObject["result"];
            string url = (string) jsonObject["build_url"];
            long? twitterId = resultvalue == 0 ? await this.twitterClient.PostSuccessfulStatus(url) : await this.twitterClient.PostFailedStatus(url);
            if (twitterId.HasValue && twitterId > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}