using System;
using GreatWeather.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace GreatWeather
{
  public static class PublishWeatherReportFunction
  {
    [FunctionName("PublishWeatherReport")]
    public static void Run([TimerTrigger("0 0 4 * * *")]TimerInfo myTimer, TraceWriter log)
    {
      log.Info($"Timer trigger function executed at: {DateTime.Now}");

      var forecast = ForecastService.GetForecast();
      var sunriseSunset = SunriseSunsetService.GetSunriseSunset();
      var report = ReportBuilder.Build(forecast, sunriseSunset);

      FacebookService.PostText(report);
    }
  }
}
