namespace GreatWeather.Evaluators
{
  public class CompassPointEvaluator : EvaluatorBase<float, string>
  {
    public CompassPointEvaluator()
    {
      Add(point => point >= 315 || point >= 0 && point < 45, "nord");
      Add(point => point >= 45 && point < 135, "öst");
      Add(point => point >= 135 && point < 225, "syd");
      Add(point => point >= 225 && point < 315, "väst");
    }
  }
}