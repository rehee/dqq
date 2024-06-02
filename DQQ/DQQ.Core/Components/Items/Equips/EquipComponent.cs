using DQQ.Combats;
using DQQ.Components.Affixes;
using DQQ.Components.Parameters;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles.Items;
using DQQ.Profiles.Items.Equipments;
using ReheeCmf.Commons.Jsons.Options;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Text.Json;

namespace DQQ.Components.Items.Equips
{
  public class EquipComponent : ItemComponent, IEquptment
  {
    public override bool Avaliable => EquipType != null;
    public EnumRarity Rarity { get; set; }
    public EnumEquipType? EquipType { get; set; }
    public EnumItemType ItemType { get; set; }

    public EnumEquipSlot? EquipSlot => EquipType?.GetMainSlot();
    public EnumEquipSlot? SecondEquipSlot => EquipType?.GetSecondSlot();
    public EquipProfile? EquipProfile => ItemProfile as EquipProfile;
    public AffixeComponent[]? Affixes { get; set; }

    public CombatProperty? Property { get; set; }

    public override void Initialize(IDQQEntity entity)
    {
      base.Initialize(entity);
      Property = new CombatProperty();
      if (entity is ICombatProperty cp)
      {
        cp.SetCompatProperty(Property);
      }
      if (entity is ItemEntity item)
      {
        if (!String.IsNullOrEmpty(item.AffixesJson))
        {
          try
          {

            Affixes = JsonSerializer.Deserialize<AffixeComponent[]?>(item.AffixesJson, JsonOption.DefaultOption);
          }
          catch
          {

          }
        }
      }

    }

    public override void Initialize(ItemProfile? itemProfile, int? itemLevel, int? quanty = null)
    {
      base.Initialize(itemProfile, itemLevel, quanty);
      this.Property = new CombatProperty();
      if (itemProfile is EquipProfile ep)
      {
        Quanty = 1;
        EquipType = ep.EquipType ?? EnumEquipType.Helmet;
      }

    }
    public override ItemEntity ToEntity()
    {
      var result = base.ToEntity();
      Property.SetCompatProperty(result);

      result.AffixesJson = JsonSerializer.Serialize(Affixes ?? Enumerable.Empty<AffixeComponent>(), JsonOption.DefaultOption);

      return result;
    }


    public virtual void AppliedAffixes()
    {
      if (Affixes?.Any() != true)
      {
        return;
      }
      foreach (var a in Affixes)
      {
        a.SetProperty(Property);
      }
      this.DisplayName = $"{String.Join("", Prefixes.Select(b => b.AffixeProfile?.Name))} {ItemProfile?.Name} {String.Join("", Suffixes.Select(b => b.AffixeProfile?.Name))}";
    }
    public IEnumerable<AffixeComponent> Prefixes => Affixes?.Where(b => b?.AffixeProfile?.IsPrefix == true) ?? Enumerable.Empty<AffixeComponent>();
    public IEnumerable<AffixeComponent> Suffixes => Affixes?.Where(b => b?.AffixeProfile?.IsPrefix == false) ?? Enumerable.Empty<AffixeComponent>();
    public virtual void InitialAffixes(AffixeComponent[]? affixes)
    {
      this.Affixes = affixes;
    }



    public override async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
    {
      var result = await base.OnTick(parameter);
      if (!result.Success)
      {
        return result;
      }
      if (Affixes?.Any() == true)
      {
        foreach (var a in Affixes)
        {
          await a.OnTick(parameter);
        }
      }
      return result;
    }

    public async Task<ContentResponse<bool>> AfterDealingDamage(AfterDealingDamageParameter? parameter)
    {
      await Task.CompletedTask;
      var result = new ContentResponse<bool>();
      if (AfterDealingDamageCount > 0)
      {
        return result;
      }
      if (EquipProfile == null)
      {
        return result;
      }
      AfterDealingDamageCount = EquipProfile.AfterDealingDamageCount;
      await EquipProfile.AfterDealingDamage(parameter);
      if (Affixes?.Any() == true)
      {
        foreach (var aff in Affixes)
        {
          await aff.AfterDealingDamage(parameter);
        }
      }
      result.SetSuccess(true);
      return result;
    }
  }
}
