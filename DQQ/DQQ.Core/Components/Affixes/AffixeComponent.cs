using DQQ.Combats;
using DQQ.Components.Parameters;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Affixes;
using DQQ.Profiles.Items.Equipments;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Components.Affixes
{
  public class AffixeComponent : DQQComponent
  {
    public EnumAffixeNumber AffixeNumber { get; set; }
    public AffixeProfile? AffixeProfile => DQQPool.TryGet<AffixeProfile, EnumAffixeNumber>(AffixeNumber);
    public AffixPower[]? Powers { get; set; }
    public void SetProperty(ICombatProperty? property)
    {
      if (property == null || Powers?.Any() != true)
      {
        return;
      }
      foreach (var p in Powers)
      {
        p.SetProperty(property);
      }
    }

    public async Task<ContentResponse<bool>> AfterDealingDamage(AfterTakeDamageParameter? parameter)
    {
      await Task.CompletedTask;
      var result = new ContentResponse<bool>();
      if (AfterDealingDamageCount > 0)
      {
        return result;
      }
      if (AffixeProfile == null)
      {
        return result;
      }
      AfterDealingDamageCount = AffixeProfile.AfterDealingDamageCount;
      await AffixeProfile.AfterDealingDamage(parameter);
      result.SetSuccess(true);
      return result;
    }
  }
}
