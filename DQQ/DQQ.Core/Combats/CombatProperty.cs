using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Combats
{
  public class CombatProperty : ICombatProperty
  {
    public long? MaximunLife { get; set; }
    public long? Armor { get; set; }
    public long? Damage { get; set; }
    public decimal? AttackPerSecond { get; set; }
    public decimal? ArmorPercentage { get; set; }
    public decimal? Resistance { get; set; }
    public long? MainHand { get; set; }
    public long? OffHand { get; set; }
  }
}
