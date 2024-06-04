using DQQ.Combats;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
  public static class EquipmentHelper
  {
    public static async Task AfterDealingDamage(this AfterDealingDamageParameter? parameter)
    {
      if (parameter == null || parameter?.From == null)
      {
        return;
      }
      if (parameter?.From is IEquippableCharacter character)
      {
        if (character.Equips?.Any() != true)
        {
          return;
        }
        foreach (var c in character.Equips.ToArray())
        {
          if (c.Value == null)
          {
            continue;
          }
          await c.Value.AfterDealingDamage(parameter);
        }
      }
    }
  }
}
