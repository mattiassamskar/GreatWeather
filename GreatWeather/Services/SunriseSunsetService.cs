using System;
using System.Net;
using Newtonsoft.Json;

namespace GreatWeather.Services
{
  public static class SunriseSunsetService
  {
    public static SunriseSunset GetSunriseSunset()
    {
      const string endpoint =
        "https://api.sunrise-sunset.org/json?lat=59.395466&lng=17.979318&formatted=0";

      using (var webClient = new WebClient())
      {
        var json = webClient.DownloadString(endpoint);
        return JsonConvert.DeserializeObject<SunriseSunset>(json,
          new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Utc });
      }
    }
  }

  public class SunriseSunset
  {
    public Results Results { get; set; }
  }

  public class Results
  {
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
  }
}