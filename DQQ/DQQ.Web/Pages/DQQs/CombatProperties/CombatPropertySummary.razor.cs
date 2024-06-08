using DQQ.Combats;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.CombatProperties
{
  public partial class CombatPropertySummary
  {
    [Parameter]
    public ICombatProperty? CombatProperty { get; set; }
  }
}