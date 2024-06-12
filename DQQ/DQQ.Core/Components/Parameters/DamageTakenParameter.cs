using DQQ.Commons;
using DQQ.Components.Stages.Maps;
using DQQ.Components.Stages;
using DQQ.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Components.Parameters
{
  public class DamageTakenParameter
  {
    public static DamageTakenParameter New(BeforeDamageTakenParameter? parameter, DamageTaken? damage)
    {
      return new DamageTakenParameter
      {
        From = parameter?.From,
        To = parameter?.To,
        Damage = damage,
        Map = parameter?.Map,
        Source = parameter?.Source
      };
    }
    public ITarget? From { get; set; }
    public ITarget? To { get; set; }
    public DamageTaken? Damage { get; set; }
    public IMap? Map { get; set; }
    public DQQProfile? Source { get; set; }
  }
}
