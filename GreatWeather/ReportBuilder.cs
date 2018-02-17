using System;
using System.Globalization;
using GreatWeather.Evaluators;
using GreatWeather.Services;

namespace GreatWeather
{
  public static class ReportBuilder
  {
    public static string Build(Forecast forecast, SunriseSunset sunriseSunset)
    {
      var weatherSymbolEvaluator = new WeatherSymbolEvaluator();

      var morningWeather = weatherSymbolEvaluator.Evaluate(forecast.GetWeatherSymbol(5));
      var morningTemperature = forecast.GetTemperature(5);

      var middayWeather = weatherSymbolEvaluator.Evaluate(forecast.GetWeatherSymbol(10));
      var middayTemperature = forecast.GetTemperature(10);

      var eveningWeather = weatherSymbolEvaluator.Evaluate(forecast.GetWeatherSymbol(16));
      var eveningTemperature = forecast.GetTemperature(16);

      var windDirection = new CompassPointEvaluator().Evaluate(forecast.GetWindDirection(10));
      var windSpeed = forecast.GetWindSpeed(10);
      var wind = char.ToUpper(windDirection[0]) + windDirection.Substring(1) + "lig vind " + windSpeed + " m/s";

      var sunrise = ConvertToSwedishTime(sunriseSunset.Results.Sunrise);
      var sunset = ConvertToSwedishTime(sunriseSunset.Results.Sunset);

      var report = "På morgonen " + morningWeather + " och " + morningTemperature + " °C. Framåt dagen " + middayWeather +
                   " och " + middayTemperature + " °C. Till kvällen " + eveningWeather + " och " + eveningTemperature + " °C." +
                   Environment.NewLine + "Solen går upp " + sunrise + " och ner " + sunset + "." + 
                   Environment.NewLine + wind + ".";

      return report;
    }

    private static string ConvertToSwedishTime(DateTime dateTime)
    {
      return TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"))
        .ToString("HH:mm", new CultureInfo("sv-SE"));
    }
  }
}