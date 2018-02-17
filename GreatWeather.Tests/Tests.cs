using GreatWeather.Services;
using Xunit;

namespace GreatWeather.Tests
{
  public class Tests
  {
    [Fact]
    public void TestReportBuilder()
    {
      var result = ReportBuilder.Build(ForecastService.GetForecast(), SunriseSunsetService.GetSunriseSunset());
    }

    [Fact]
    public void TestSunriseSunset()
    {
      var result = SunriseSunsetService.GetSunriseSunset();
    }

    [Fact]
    public void TestFacebookService()
    {
      var forecast = ForecastService.GetForecast();
      var sunriseSunset = SunriseSunsetService.GetSunriseSunset();
      var report = ReportBuilder.Build(forecast, sunriseSunset);

      FacebookService.PostText(report);
    }
  }
}