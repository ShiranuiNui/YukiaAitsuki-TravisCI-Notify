using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace YukiaAitsuki_TravisCI_Notify.SignatureVerifier
{
    public interface ISignatureVerifier
    {
        Task<bool> Post(HttpRequest httpRequest);
    }
}