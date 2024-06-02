using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Components.Parameters
{
  public class ComponentTickParameter
  {
    public static ComponentTickParameter New(ITarget? from, IEnumerable<ITarget>? friendlyTargets, IEnumerable<ITarget>? enemyTargets, IMap? map)
    {
      return new ComponentTickParameter
      {
        From = from,
        FriendlyTargets = friendlyTargets,
        EnemyTargets = enemyTargets,
        Map = map
      };
    }
    public static ComponentTickParameter New(ComponentTickParameter? parameter, ITarget? secondaryTarget)
    {
      return new ComponentTickParameter
      {
        From = parameter?.From,
        FriendlyTargets = parameter?.FriendlyTargets,
        EnemyTargets = parameter?.EnemyTargets,
        Map = parameter?.Map,
        SecondaryTarget = secondaryTarget
      };
    }
    public ITarget? From { get; set; }
    public ITarget? SecondaryTarget { get; set; }
    public IEnumerable<ITarget>? FriendlyTargets { get; set; }
    public IEnumerable<ITarget>? EnemyTargets { get; set; }
    public IMap? Map { get; set; }

    public ITarget? SelectedTarget => SecondaryTarget ?? From?.Target;
  }
}
