using BootstrapBlazor.Components;

namespace DQQ.Helper
{
  public static class LocalizationHelper
  {
    public static SelectedItem[] GetAvaliableLang { get; set; } = [
      new SelectedItem("zh","中文"),
      new SelectedItem("en-US","English"),
      ];

  }
}
