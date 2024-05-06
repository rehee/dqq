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
    public static void CombatPropertySummary(this ICombatProperty main, IEnumerable<ICombatProperty?>? subs)
    {
      if (subs?.Any() != true)
      {
        return;
      }
      foreach (var sub in subs)
      {
        if (sub == null)
        {
          continue;
        }
        if (sub.MaximunLife != null)
        {
          main.MaximunLife = main.MaximunLife ?? 0 + sub.MaximunLife;
        }
        if (sub.Armor != null)
        {
          main.Armor = main.Armor ?? 0 + sub.Armor;
        }
        if (sub.ArmorPercentage != null)
        {
          main.ArmorPercentage = main.ArmorPercentage ?? 0 + sub.ArmorPercentage;
        }
        if (sub.Resistance != null)
        {
          main.Resistance = main.Resistance ?? 0 + sub.Resistance;
        }
        if (sub.Damage != null)
        {
          main.Damage = main.Damage ?? 0 + sub.Damage;
        }
        if (sub.MainHand != null)
        {
          main.MainHand = main.MainHand ?? 0 + sub.MainHand;
        }
        if (sub.OffHand != null)
        {
          main.OffHand = main.OffHand ?? 0 + sub.OffHand;
        }
      }

      var speeds = subs.Where(b => b.AttackPerSecond != null).ToArray();
      if (speeds.Length == 1)
      {
        main.AttackPerSecond = speeds.FirstOrDefault()?.AttackPerSecond;
      }
      if (speeds.Length > 1)
      {
        main.AttackPerSecond = speeds.Take(2).Select(b => b.AttackPerSecond).Average() * DQQGeneral.DuelwieldAttackSpeed;
      }
    }
  }
}
