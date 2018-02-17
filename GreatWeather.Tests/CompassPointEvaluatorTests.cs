using GreatWeather.Evaluators;
using Xunit;

namespace GreatWeather.Tests
{
  public class CompassPointEvaluatorTests
  {
    [Fact]
    public void Evaluate_NorthernDegrees_ReturnsNorth()
    {
      Assert.Equal("nord", new CompassPointEvaluator().Evaluate(0));
      Assert.Equal("nord", new CompassPointEvaluator().Evaluate(360));
      Assert.Equal("nord", new CompassPointEvaluator().Evaluate(44));
      Assert.Equal("nord", new CompassPointEvaluator().Evaluate(315));
    }

    [Fact]
    public void Evaluate_EasternDegrees_ReturnsEast()
    {
      Assert.Equal("öst", new CompassPointEvaluator().Evaluate(45));
      Assert.Equal("öst", new CompassPointEvaluator().Evaluate(134));
    }

    [Fact]
    public void Evaluate_SouthernDegrees_ReturnsSouth()
    {
      Assert.Equal("syd", new CompassPointEvaluator().Evaluate(135));
      Assert.Equal("syd", new CompassPointEvaluator().Evaluate(224));
    }

    [Fact]
    public void Evaluate_WesternDegrees_ReturnsWest()
    {
      Assert.Equal("väst", new CompassPointEvaluator().Evaluate(225));
      Assert.Equal("väst", new CompassPointEvaluator().Evaluate(314));
    }
  }
}