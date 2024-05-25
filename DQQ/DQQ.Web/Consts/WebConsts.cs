namespace DQQ.Consts
{
  public static class WebConsts
  {
    public static string? URL { get; set; }
    public static string DocumentURL => $"{URL}swagger";
    public const string CoCosURL = "cocos/index.html";
  }
}
