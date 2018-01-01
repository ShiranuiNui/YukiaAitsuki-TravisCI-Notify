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
            await next.Invoke(context);
        }
    }
}