using DQQ.Combats;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles.Affixes;
using DQQ.Profiles.Items;
using DQQ.Profiles.Items.Equipments;
using ReheeCmf.Commons.Jsons.Options;
using ReheeCmf.Helpers;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Nodes;

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

    public AffixeComponent[]? Affixes { get; set; }

    public Int64? MaximunLife { get; set; }
    public Int64? Armor { get; set; }
    public Int64? Damage { get; set; }
    public decimal? AttackPerSecond { get; set; }
    public decimal? ArmorPercentage { get; set; }
    public decimal? Resistance { get; set; }
    public Int64? MainHand { get; set; }
    public Int64? OffHand { get; set; }
    public decimal? DamageModifier { get; set; }

    public long? AttackRating { get; set; }

    public override void Initialize(IDQQEntity entity)
    {
      base.Initialize(entity);

      if (entity is ICombatProperty cp)
      {
        cp.SetCompatProperty(this);
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
      if (itemProfile is EquipProfile ep)
      {
        Quanty = 1;
        EquipType = ep.EquipType ?? EnumEquipType.Helmet;
      }

    }
    public override ItemEntity ToEntity()
    {
      var result = base.ToEntity();
      this.SetCompatProperty(result);

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
        a.SetProperty(this);
      }
      this.DisplayName = $"{String.Join("", Prefixes.Select(b => b.AffixeProfile?.Name))} {ItemProfile?.Name} {String.Join("", Suffixes.Select(b => b.AffixeProfile?.Name))}";
    }
    public IEnumerable<AffixeComponent> Prefixes => Affixes?.Where(b => b?.AffixeProfile?.IsPrefix == true) ?? Enumerable.Empty<AffixeComponent>();
    public IEnumerable<AffixeComponent> Suffixes => Affixes?.Where(b => b?.AffixeProfile?.IsPrefix == false) ?? Enumerable.Empty<AffixeComponent>();
    public virtual void InitialAffixes(AffixeComponent[]? affixes)
    {
      this.Affixes = affixes;
    }
  }
}
