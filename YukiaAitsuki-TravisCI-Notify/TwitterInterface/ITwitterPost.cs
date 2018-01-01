using System;
using System.Threading.Tasks;
namespace YukiaAitsuki_TravisCI_Notify.Twitter
{
    public interface ITwitterPost
    {
        Task<long?> PostSuccessfulStatus(string url);
        Task<long?> PostFailedStatus(string url);
        Task DeleteStatus(long statusid);
    }
}