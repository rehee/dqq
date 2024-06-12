using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Components.Parameters
{
  public class BeforeDamageTakenParameter
  {


    public static BeforeDamageTakenParameter New(ComponentTickParameter? parameter, DQQProfile? source, params DamageDeal[]? damages)
    {
      return new BeforeDamageTakenParameter
      {
        From = parameter?.From,
        To = parameter?.SelectedTarget,
        Map = parameter?.Map,
        Source = source,
        Damages = damages?.ToList() ?? null
      };
    }
    public static BeforeDamageTakenParameter New(ITarget? from, ITarget? to, IMap? map, DQQProfile? source, params DamageDeal[]? damages)
    {
      return new BeforeDamageTakenParameter
      {
        From = from,
        To = to,
        Map = map,
        Source = source,
        Damages = damages?.ToList() ?? null
      };
    }
    public ITarget? From { get; set; }
    public ITarget? To { get; set; }
    public List<DamageDeal>? Damages { get; set; }
    public IMap? Map { get; set; }
    public DQQProfile? Source { get; set; }
  }
}
