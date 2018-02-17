using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace GreatWeather.Services
{
  public static class ForecastService
  {
    public static Forecast GetForecast()
    {
      const string endpoint =
        "https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/17.979318/lat/59.395466/data.json";

      using (var webClient = new WebClient())
      {
        var json = webClient.DownloadString(endpoint);
        return JsonConvert.DeserializeObject<Forecast>(json);
      }
    }
  }

  public class Forecast
  {
    public List<DataPoint> TimeSeries { get; set; }

    public int GetTemperature(int hour)
    {
      return GetValue("t", hour);
    }

    public int GetWeatherSymbol(int hour)
    {
      return GetValue("Wsymb2", hour);
    }

    public int GetWindDirection(int hour)
    {
      return GetValue("wd", hour);
    }

    public int GetWindSpeed(int hour)
    {
      return GetValue("ws", hour);
    }

    private int GetValue(string name, int hour)
    {
      var result = TimeSeries.FirstOrDefault(t => t.ValidTime.Hour == hour)
        ?.Parameters.FirstOrDefault(p => p.Name == name)
        ?.Values.FirstOrDefault();

      if (!result.HasValue) throw new ArgumentOutOfRangeException();

      return (int)Math.Round(result.Value);
    }

    public class DataPoint
    {
      public DateTime ValidTime { get; set; }
      public List<Parameter> Parameters { get; set; }

      public class Parameter
      {
        public string Name { get; set; }
        public string LevelType { get; set; }
        public int Level { get; set; }
        public string Unit { get; set; }
        public List<float> Values { get; set; }
      }
    }
  }

}