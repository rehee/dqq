using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Combats
{
  public class CombatProperty : ICombatProperty
  {
    public CombatProperty()
    {

    }
    public CombatProperty(ICombatProperty property)
    {
      MaximunLife = property.MaximunLife;
      Armor = property.Armor;
      Damage = property.Damage;
      AttackPerSecond = property.AttackPerSecond;
      ArmorPercentage = property.ArmorPercentage;
      Resistance = property.Resistance;
      MainHand = property.MainHand;
      OffHand = property.OffHand;
      MaximunLife = property.MaximunLife;
      DamageModifier = property.DamageModifier;
      AttackRating = property.AttackRating;
      Defence = property.Defence;
      DefencePercentage = property.DefencePercentage;
      BlockChance = property.BlockChance;
      BlockRecovery = property.BlockRecovery;
      DodgeChance = property.DodgeChance;
      PhysicsResistance = property.PhysicsResistance;
      FireResistance = property.FireResistance;
      ColdResistance = property.ColdResistance;
      LightningResistance = property.LightningResistance;
      ChaosResistance = property.ChaosResistance;
      PhysicsDamageModifier = property.PhysicsDamageModifier;
      FireDamageModifier = property.FireDamageModifier;
      ColdDamageModifier = property.ColdDamageModifier;
      LightningDamageModifier = property.LightningDamageModifier;
      ChaosDamageModifier = property.ChaosDamageModifier;
      
    }
    public long? MaximunLife { get; set; }
    public long? Armor { get; set; }
    public long? Damage { get; set; }
    public decimal? AttackPerSecond { get; set; }
    public decimal? ArmorPercentage { get; set; }
    public decimal? Resistance { get; set; }
    public long? MainHand { get; set; }
    public long? OffHand { get; set; }
    public decimal? DamageModifier { get; set; }
    public long? AttackRating { get; set; }

    public long? Defence { get; set; }
    public decimal? DefencePercentage { get; set; }
    public decimal? BlockChance { get; set; }
    public decimal? BlockRecovery { get; set; }
    public decimal? DodgeChance { get; set; }
    public long? PhysicsResistance { get; set; }
    public long? FireResistance { get; set; }
    public long? ColdResistance { get; set; }
    public long? LightningResistance { get; set; }
    public long? ChaosResistance { get; set; }
    public decimal? PhysicsDamageModifier { get; set; }
    public decimal? FireDamageModifier { get; set; }
    public decimal? ColdDamageModifier { get; set; }
    public decimal? LightningDamageModifier { get; set; }
    public decimal? ChaosDamageModifier { get; set; }
  }
}
