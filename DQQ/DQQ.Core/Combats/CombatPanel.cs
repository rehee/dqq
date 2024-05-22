using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
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

    public void CalculateDynamicPanel(ITarget actor, IMap map)
    {
      if (actor.Alive)
      {
        calculatedPanel = new CombatProperty(StaticPanel);
        foreach (var duration in actor.Durations)
        {
          duration.CombatPropertyCalculate(calculatedPanel, StaticPanel, map);
        }
      }
      else
      {
        calculatedPanel = StaticPanel;
      }

    }

    public bool IsDuelWield => (DynamicPanel.MainHand != null && DynamicPanel.MainHand > 0) && (DynamicPanel.OffHand != null && DynamicPanel.OffHand > 0);
  }
}
