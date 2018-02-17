using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GreatWeather.Services
{
  public class TwitterApi
  {
    private const string TwitterApiBaseUrl = "https://api.twitter.com/1.1/";
    readonly string _consumerKey;
    readonly string _accessToken;
    readonly HMACSHA1 _sigHasher;
    readonly DateTime _epochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public TwitterApi(string consumerKey, string consumerKeySecret, string accessToken, string accessTokenSecret)
    {
      _consumerKey = consumerKey;
      _accessToken = accessToken;
      _sigHasher = new HMACSHA1(new ASCIIEncoding().GetBytes($"{consumerKeySecret}&{accessTokenSecret}"));
    }

    public Task<string> Tweet(string text)
    {
      var data = new Dictionary<string, string>
      {
        {"status", text},
        {"trim_user", "1"}
      };

      return SendRequest("statuses/update.json", data);
    }

    private Task<string> SendRequest(string url, Dictionary<string, string> data)
    {
      var fullUrl = TwitterApiBaseUrl + url;
      var timestamp = (int) (DateTime.UtcNow - _epochUtc).TotalSeconds;

      data.Add("oauth_consumer_key", _consumerKey);
      data.Add("oauth_signature_method", "HMAC-SHA1");
      data.Add("oauth_timestamp", timestamp.ToString());
      data.Add("oauth_nonce", "a");
      data.Add("oauth_token", _accessToken);
      data.Add("oauth_version", "1.0");
      data.Add("oauth_signature", GenerateSignature(fullUrl, data));

      var oAuthHeader = GenerateOAuthHeader(data);
      var formData = new FormUrlEncodedContent(data.Where(kvp => !kvp.Key.StartsWith("oauth_")));
      return SendRequest(fullUrl, oAuthHeader, formData);
    }

    private string GenerateSignature(string url, Dictionary<string, string> data)
    {
      var sigString = string.Join("&",
        data.Union(data).Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}")
          .OrderBy(s => s));

      var fullSigData = $"POST&{Uri.EscapeDataString(url)}&{Uri.EscapeDataString(sigString)}";
      return Convert.ToBase64String(_sigHasher.ComputeHash(new ASCIIEncoding().GetBytes(fullSigData)));
    }

    private string GenerateOAuthHeader(Dictionary<string, string> data)
    {
      return "OAuth " + string.Join(", ",
               data.Where(kvp => kvp.Key.StartsWith("oauth_"))
                 .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}=\"{Uri.EscapeDataString(kvp.Value)}\"")
                 .OrderBy(s => s));
    }

    private async Task<string> SendRequest(string fullUrl, string oAuthHeader, FormUrlEncodedContent formData)
    {
      using (var http = new HttpClient())
      {
        http.DefaultRequestHeaders.Add("Authorization", oAuthHeader);
        var httpResp = await http.PostAsync(fullUrl, formData);
        return await httpResp.Content.ReadAsStringAsync();
      }
    }
  }
}