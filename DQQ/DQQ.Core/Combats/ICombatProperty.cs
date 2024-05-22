using System.Numerics;

namespace DQQ.Combats
{
  public interface ICombatProperty
  {
    Int64? MaximunLife { get; set; }
    Int64? Armor { get; set; }
    Int64? Damage { get; set; }
    decimal? AttackPerSecond { get; set; }
    decimal? ArmorPercentage { get; set; }
    decimal? Resistance { get; set; }
    decimal? DamageModifier { get; set; }

    Int64? MainHand { get; set; }
    Int64? OffHand { get; set; }


  }


}
