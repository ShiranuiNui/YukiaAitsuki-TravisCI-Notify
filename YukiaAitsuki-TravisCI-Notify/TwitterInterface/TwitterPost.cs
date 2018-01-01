using System.Threading.Tasks;
using CoreTweet;
using Microsoft.Extensions.Configuration;

namespace YukiaAitsuki_TravisCI_Notify.Twitter
{
    public class TwitterPost : ITwitterPost
    {
        private readonly Tokens TwitterToken;
        private readonly string UserScreenName;
        private readonly string NickName;
        public TwitterPost(IConfiguration configuration)
        {
            string apiKey = configuration.GetValue("TWITTER_APIKEY", "");
            string apiSecret = configuration.GetValue("TWITTER_APISECRET", "");
            string accessToken = configuration.GetValue("TWITTER_ACCESSTOKEN", "");
            string accessSecret = configuration.GetValue("TWITTER_ACCESSSECRET", "");
            this.UserScreenName = configuration.GetValue("TWITTER_SCREEN_NAME", "");
            this.NickName = configuration.GetValue("NICK_NAME", this.UserScreenName);
            this.TwitterToken = new Tokens(Tokens.Create(apiKey, apiSecret, accessToken, accessSecret));
        }
        public async Task<long?> PostSuccessfulStatus(string url)
        {
            try
            {
                string statusString = $"@{this.UserScreenName}\n" +
                    $"お疲れ様です。{this.NickName}さん。\n" +
                    "テストが成功したようですよ。これで一息付けますね。\n" +
                    "詳細はリンクからご確認下さい\n\n" +
                    $"{url}";
                var result = await this.TwitterToken.Statuses.UpdateAsync(status => statusString);
                if (result.Id != 0)
                {
                    return result.Id;
                }
                else
                {
                    return null;
                }
            }
            catch (TwitterException)
            {
                return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        public async Task<long?> PostFailedStatus(string url)
        {
            try
            {
                string statusString = $"@{this.UserScreenName}\n" +
                    "お疲れ様です。{this.NickName}さん。\n" +
                    "テストが失敗したようです……。もうひと頑張り……ですかね。\n" +
                    "詳細はリンクからご確認下さい\n\n" +
                    "{url}";
                var result = await this.TwitterToken.Statuses.UpdateAsync(status => statusString);
                if (result.Id != 0)
                {
                    return result.Id;
                }
                else
                {
                    return null;
                }
            }
            catch (TwitterException)
            {
                return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        public Task DeleteStatus(long statusid)
        {
            return this.TwitterToken.Statuses.DestroyAsync(statusid);
        }
    }
}