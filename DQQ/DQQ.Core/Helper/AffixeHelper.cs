﻿using DQQ.Commons;
using DQQ.Components.Affixes;
using DQQ.Components.Items.Equips;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Affixes;

namespace DQQ.Helper
{
  public static class AffixeHelper
  {
    public static AffixeComponent[]? GenerateAllAffieComponents(this EquipComponent? equip)
    {
      if (equip?.Avaliable != true || equip?.Rarity == EnumRarity.Normal)
      {
        return null;
      }
      var affixes = GenerateAllAffies(AffixeChange.New(equip));
      return affixes.Select(b => b.GenerateAffixe(equip?.ItemLevel ?? 1)).ToArray();
    }

    public static IEnumerable<AffixeProfile> GenerateAllAffies(AffixeChange changes)
    {
      var prefixChange = AffixeChange.New(changes);
      prefixChange.IsPrefix = true;
      if (changes.MinAffex > 0)
      {
        prefixChange.MinAffex = 1;
      }
      var sufixChange = AffixeChange.New(changes);
      sufixChange.IsPrefix = false;
      var result = GenerateAffixeChange(prefixChange).Concat(GenerateAffixeChange(sufixChange));
      if (result.Any() != true)
      {
        changes.MinAffex = changes.MinAffex + 1;
        return GenerateAllAffies(changes);
      }
      return result;
    }

    public static IEnumerable<AffixeProfile> GenerateAffixeChange(AffixeChange changes)
    {
      var prefixNumber = GetRandomAffixeNumber(changes.Rarity);
      if (prefixNumber < changes.MinAffex)
      {
        prefixNumber = changes.MinAffex;
      }
      var allAvaliables = DQQPool.AffixePool.Select(b => b.Value).Where(b => b.IsPrefix == changes.IsPrefix)
        .Where(b => changes.ItemLevel >= b.AffixeLevel)
        .Where(b => b.MatchRarityLimits(changes.Rarity))
        .Where(b => b.MatchEquipTypeLimites(changes.EquipType))
        .Where(b => b.MatchItemTypeLimites(changes.ItemType))
        .GroupBy(b => b.AffixeGroup)
        .GetNumberOfRandom(prefixNumber)
        .Select(b => b.GetRamdom());

      if (allAvaliables == null)
      {
        return Enumerable.Empty<AffixeProfile>();
      }
      return allAvaliables.ToArray();
    }

    public static int GetRandomAffixeNumber(EnumRarity rarity)
    {
      var maxNumber = 0;
      switch (rarity)
      {
        case EnumRarity.Magic:
          maxNumber = 1;
          break;
        case EnumRarity.Rare:
          maxNumber = 3;
          break;
      }
      return RandomHelper.GetRandomInt(0, maxNumber);
    }
  }
}