using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.DQQComponents
{
  public partial class SelectComponent
  {
    [Parameter]
    public bool? Value { get; set; }

    public IEnumerable<SelectedItem>? Items { get; set; } = new SelectedItem[] { new SelectedItem("true", "ÊÇ"), new SelectedItem("true", "·ñ") };
  }
}