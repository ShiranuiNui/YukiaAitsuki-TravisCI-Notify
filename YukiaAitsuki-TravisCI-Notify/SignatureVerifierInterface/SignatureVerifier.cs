using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace YukiaAitsuki_TravisCI_Notify.SignatureVerifier
{
    public class SignatureVerifier
    {
        public async Task<bool> Post(string body, string signature)
        {
            using(var verifyRequest = new HttpRequestMessage())
            {
                verifyRequest.Method = HttpMethod.Post;
                verifyRequest.RequestUri = new Uri("http://signature-verifier/");
                verifyRequest.Headers.Add("Signature", signature);
                verifyRequest.Content = new StringContent(body);
                using(var client = new HttpClient())
                {
                    var verifyResult = await client.SendAsync(verifyRequest);
                    return verifyResult.IsSuccessStatusCode;
                }
            }
        }
    }
}