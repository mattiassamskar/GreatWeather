namespace GreatWeather.Evaluators
{
  public class WeatherSymbolEvaluator : EvaluatorBase<int, string>
  {
    public WeatherSymbolEvaluator()
    {
      Add(i => i == 1, "klart");
      Add(i => i == 2, "klart till halvklart");
      Add(i => i == 3, "varierande molnighet");
      Add(i => i == 4, "halvklart");
      Add(i => i == 5, "molnigt");
      Add(i => i == 6, "gråmulet");
      Add(i => i == 7, "dimma");
      Add(i => i == 8, "lätta regnskurar");
      Add(i => i == 9, "måttliga regnskurar");
      Add(i => i == 10, "kraftiga regnskurar");
      Add(i => i == 11, "åskskurar");
      Add(i => i == 12, "lätta skurar med snöblandat regn");
      Add(i => i == 13, "måttliga skurar med snöblandat regn");
      Add(i => i == 14, "kraftiga skurar med snöblandat regn");
      Add(i => i == 15, "lätta snöbyar");
      Add(i => i == 16, "måttliga snöbyar");
      Add(i => i == 17, "kraftiga snöbyar");
      Add(i => i == 18, "lätt regn");
      Add(i => i == 19, "måttligt regn");
      Add(i => i == 20, "kraftigt regn");
      Add(i => i == 21, "åska");
      Add(i => i == 22, "lätt snöblandat regn");
      Add(i => i == 23, "måttligt snöblandat regn");
      Add(i => i == 24, "kraftigt snöblandat regn");
      Add(i => i == 25, "lätt snöfall");
      Add(i => i == 26, "måttligt snöfall");
      Add(i => i == 27, "kraftigt snöfall");
    }
  }
}