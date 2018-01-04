﻿using System;
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
        private IConfiguration configuration;
        private ITwitterPost twitterClient;
        public TravisCIWebHookReceiverController(IConfiguration _configration, ITwitterPost _twitterClient)
        {
            this.configuration = _configration;
            this.twitterClient = _twitterClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string value)
        {
            string debug_signature = this.Request.Headers["Signature"][0];
            string payload = this.Request.Form["payload"];

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