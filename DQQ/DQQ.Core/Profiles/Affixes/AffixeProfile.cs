using DQQ.Combats;
using DQQ.Components;
using DQQ.Components.Affixes;
using DQQ.Components.Items;
using DQQ.Components.Items.Equips;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using ReheeCmf.Responses;
using static System.Net.Mime.MediaTypeNames;

namespace DQQ.Profiles.Affixes
{
  public abstract class AffixeProfile : DQQProfile<EnumAffixeNumber>
  {
    public abstract EnumAffixeGroup AffixeGroup { get; }
    public virtual int AffixeLevel => 0;
    public abstract int TierLevel { get; }
    public abstract bool IsPrefix { get; }
    public virtual EnumRarity[]? RarityLimits => null;
    public virtual EnumItemType[]? ItemTypeLimites => null;
    public virtual EnumEquipType[]? EquipTypeLimites => null;
    public abstract AffixeRange[] Ranges { get; }

    public virtual AffixeComponent GenerateAffixe(int level)
    {
      var result = new AffixeComponent();
      result.Powers = Ranges.Select(b => b.NewPower(level)).ToArray();
      result.AffixeNumber = ProfileNumber;
      return result;
    }

    public bool MatchRarityLimits(EnumRarity rarity)
    {
      if (RarityLimits?.Any() != true)
      {
        return true;
      }
      return RarityLimits.Any(b => b == rarity);
    }
    public bool MatchItemTypeLimites(EnumItemType itemType)
    {
      if (ItemTypeLimites?.Any() != true)
      {
        return true;
      }
      return ItemTypeLimites.Any(b => b == itemType);
    }
    public bool MatchEquipTypeLimites(EnumEquipType equipType)
    {
      if (EquipTypeLimites?.Any() != true)
      {
        return true;
      }
      return EquipTypeLimites.Any(b => b == equipType);
    }
  }


}
