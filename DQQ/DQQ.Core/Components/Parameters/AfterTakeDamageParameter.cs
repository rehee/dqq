using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Components.Parameters
{
  public class AfterTakeDamageParameter
  {
    public static AfterTakeDamageParameter New(ComponentTickParameter? parameter, DamageTaken? damage)
    {
      return new AfterTakeDamageParameter
      {
        From = parameter?.From,
        To = parameter?.SelectedTarget,
        Damage = damage,
        Map = parameter?.Map
      };
    }
    public ITarget? From { get; set; }
    public ITarget? To { get; set; }
    public DamageTaken? Damage { get; set; }
    public IMap? Map { get; set; }

  }
}
