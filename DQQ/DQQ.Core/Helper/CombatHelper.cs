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
