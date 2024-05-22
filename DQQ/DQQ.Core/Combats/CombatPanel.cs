using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Combats
{
  public class CombatPanel
  {
    public CombatProperty StaticPanel { get; set; } = new CombatProperty();

    private CombatProperty? calculatedPanel { get; set; }
    public CombatProperty DynamicPanel => calculatedPanel ?? StaticPanel;

    public void CalculateDynamicPanel()
    {
      calculatedPanel = StaticPanel;
    }
  }
}
