using System.Collections.Specialized;
using System.Configuration;
using System.Net;

namespace GreatWeather.Services
{
  public static class FacebookService
  {
    public static void PostText(string text)
    {
      var url = ConfigurationManager.AppSettings["FacebookUrl"];
      var accessToken = ConfigurationManager.AppSettings["FacebookAccessToken"];

      using (var webClient = new WebClient())
      {
        webClient.UploadValues(url, new NameValueCollection { { "message", text }, { "access_token", accessToken } });
      }
    }
  }
}