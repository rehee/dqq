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
  }
}
