using DQQ.Combats;
using DQQ.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
  public static class CombatHelper
  {
    public static void CombatPropertySummary(this ICombatProperty main, IEnumerable<CombatPropertySum?>? subs)
    {
      if (subs?.Any() != true)
      {
        return;
      }
      foreach (var sub in subs)
      {
        if (sub == null || sub.Property == null || sub.Slot == null)
        {
          continue;
        }
        if (sub.Property.MaximunLife != null)
        {
          main.MaximunLife = main.MaximunLife ?? 0 + sub.Property.MaximunLife;
        }
        if (sub.Property.Armor != null)
        {
          main.Armor = main.Armor ?? 0 + sub.Property.Armor;
        }
        if (sub.Property.ArmorPercentage != null)
        {
          main.ArmorPercentage = main.ArmorPercentage ?? 0 + sub.Property.ArmorPercentage;
        }
        if (sub.Property.Resistance != null)
        {
          main.Resistance = main.Resistance ?? 0 + sub.Property.Resistance;
        }
        if (sub.Property.Damage != null)
        {
          main.Damage = main.Damage ?? 0 + sub.Property.Damage;
        }
        if (sub.Slot == Enums.EnumEquipSlot.MainHand && sub.Property.MainHand != null)
        {
          main.MainHand = main.MainHand ?? 0 + sub.Property.MainHand ?? 0;
        }
        if (sub.Slot == Enums.EnumEquipSlot.OffHand && sub.Property.OffHand != null)
        {
          main.OffHand = main.OffHand ?? 0 + sub.Property.OffHand;
        }
        if (sub.Property.AttackRating != null)
        {
          main.AttackRating = main.AttackRating.DefaultValue() + sub.Property.AttackRating;
        }

        if (sub.Property.Defence != null)
        {
          main.Defence = main.Defence.DefaultValue() + sub.Property.Defence;
        }
        if (sub.Property.DefencePercentage != null)
        {
          main.DefencePercentage = main.DefencePercentage.DefaultValue() + sub.Property.DefencePercentage;
        }
        if (sub.Property.BlockChance != null)
        {
          main.BlockChance = main.BlockChance.DefaultValue() + sub.Property.BlockChance;
        }
        if (sub.Property.BlockRecovery != null)
        {
          main.BlockRecovery = main.BlockRecovery.DefaultValue() + sub.Property.BlockRecovery;
        }
        if (sub.Property.DodgeChance != null)
        {
          main.DodgeChance = main.DodgeChance.DefaultValue() + sub.Property.DodgeChance;
        }
        if (sub.Property.PhysicsResistance != null)
        {
          main.PhysicsResistance = main.PhysicsResistance.DefaultValue() + sub.Property.PhysicsResistance;
        }

        if (sub.Property.FireResistance != null)
        {
          main.FireResistance = main.FireResistance.DefaultValue() + sub.Property.FireResistance;
        }
        if (sub.Property.ColdResistance != null)
        {
          main.ColdResistance = main.ColdResistance.DefaultValue() + sub.Property.ColdResistance;
        }
        if (sub.Property.LightningResistance != null)
        {
          main.LightningResistance = main.LightningResistance.DefaultValue() + sub.Property.LightningResistance;
        }
        if (sub.Property.ChaosResistance != null)
        {
          main.ChaosResistance = main.ChaosResistance.DefaultValue() + sub.Property.ChaosResistance;
        }
        if (sub.Property.PhysicsDamageModifier != null)
        {
          main.PhysicsDamageModifier = main.PhysicsDamageModifier.DefaultValue() + sub.Property.PhysicsDamageModifier;
        }
        if (sub.Property.FireDamageModifier != null)
        {
          main.FireDamageModifier = main.FireDamageModifier.DefaultValue() + sub.Property.FireDamageModifier;
        }
        if (sub.Property.ColdDamageModifier != null)
        {
          main.ColdDamageModifier = main.ColdDamageModifier.DefaultValue() + sub.Property.ColdDamageModifier;
        }
        if (sub.Property.LightningDamageModifier != null)
        {
          main.LightningDamageModifier = main.LightningDamageModifier.DefaultValue() + sub.Property.LightningDamageModifier;
        }
        if (sub.Property.ChaosDamageModifier != null)
        {
          main.ChaosDamageModifier = main.ChaosDamageModifier.DefaultValue() + sub.Property.ChaosDamageModifier;
        }
        
      }

      var mainHandSpeed = subs.Where(b => b?.Slot == Enums.EnumEquipSlot.MainHand && b?.Property?.AttackPerSecond != null).Select(b => b?.Property?.AttackPerSecond).FirstOrDefault();
      var offHandSpeed = subs.Where(b => b?.Slot == Enums.EnumEquipSlot.OffHand && b?.Property?.AttackPerSecond != null).Select(b => b?.Property?.AttackPerSecond).FirstOrDefault();
      if (mainHandSpeed != null && offHandSpeed == null)
      {
        main.AttackPerSecond = mainHandSpeed;
      }
      if (mainHandSpeed != null && offHandSpeed != null)
      {
        main.AttackPerSecond = ((mainHandSpeed + offHandSpeed) / 2) * DQQGeneral.DuelwieldAttackSpeed;
      }
    }
  }
}
