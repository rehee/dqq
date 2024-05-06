using System.Numerics;

namespace DQQ.Combats
{
  public interface ICombatProperty
  {
    BigInteger? MaximunLife { get; set; }
    BigInteger? Armor { get; set; }
    BigInteger? Damage { get; set; }
    decimal? AttackPerSecond { get; set; }
    decimal? ArmorPercentage { get; set; }
    decimal? Resistance { get; set; }

    BigInteger? MainHand { get; set; }
    BigInteger? OffHand { get; set; }

    bool? PrevioursMainHand { get; set; }
  }


}
