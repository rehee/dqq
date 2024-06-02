using System.Numerics;

namespace DQQ.Combats
{
  public interface ICombatProperty
  {
    long? MaximunLife { get; set; }
    long? Armor { get; set; }
    long? Damage { get; set; }
    decimal? AttackPerSecond { get; set; }
    decimal? ArmorPercentage { get; set; }
    decimal? Resistance { get; set; }
    decimal? DamageModifier { get; set; }
    long? AttackRating { get; set; }
    long? Defence { get; set; }
    long? DefencePercentage { get; set; }
    long? MainHand { get; set; }
    long? OffHand { get; set; }

    decimal? BlockChance { get; set; }
    decimal? BlockRecovery { get; set; }

    decimal? DodgeChance { get; set; }

    long? PhysicsResistance { get; set; }
    long? FireResistance { get; set; }
    long? ColdResistance { get; set; }
    long? LightningResistance { get; set; }
    long? ChaosResistance { get; set; }

    decimal? PhysicsDamageModifier { get; set; }
    decimal? FireDamageModifier { get; set; }
    decimal? ColdDamageModifier { get; set; }
    decimal? LightningDamageModifier { get; set; }
    decimal? ChaosDamageModifier { get; set; }
  }


}
