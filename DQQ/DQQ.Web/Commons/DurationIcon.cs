using BootstrapBlazor.Components;

namespace DQQ.Commons
{
  public class DurationIcon
  {
    public static DurationIcon New(Color iconColor, String iconName)
    {
      return new DurationIcon()
      {
        IconColor = iconColor,
        IconName = iconName
      };
    }
    public Color IconColor { get; set; }
    public string? IconName { get; set; }
  }
}
