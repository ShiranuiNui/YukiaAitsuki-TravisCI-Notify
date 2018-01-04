using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using YukiaAitsuki_TravisCI_Notify.SignatureVerifier;
namespace YukiaAitsuki_TravisCI_Notify.Middreware
{
    public class VerifySignatureMiddreware
    {
        private readonly RequestDelegate next;
        public VerifySignatureMiddreware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            /*
            var verifier = new SignatureVerifier.SignatureVerifier();
            var verifyResult = await verifier.Post(payload, this.Request.Headers["Signature"][0]);
            if (!verifyResult)
            {
                return Unauthorized();
            }
            */
            await next.Invoke(context);
        }
    }
}