using System;
using System.Threading.Tasks;
namespace YukiaAitsuki_TravisCI_Notify.Twitter
{
    public interface ITwitterPost
    {
        Task PostSuccessfulStatus();
        Task PostFailedStatus();
    }
}