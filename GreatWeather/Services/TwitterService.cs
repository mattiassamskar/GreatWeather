using System.Configuration;

namespace GreatWeather.Services
{
  public static class TwitterService
  {
    public static async void TweetText(string text)
    {
      var consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
      var consumerKeySecret = ConfigurationManager.AppSettings["ConsumerKeySecret"];
      var accessToken = ConfigurationManager.AppSettings["AccessToken"];
      var accessTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"];

      var twitter = new TwitterApi(consumerKey, consumerKeySecret, accessToken, accessTokenSecret);
      await twitter.Tweet(text);
    }
  }
}