using DQQ.Combats;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles.Affixes;
using DQQ.Profiles.Items;
using DQQ.Profiles.Items.Equipments;
using System.Numerics;

namespace DQQ.Components.Items.Equips
{
  public class EquipComponent : ItemComponent, IEquptment
  {
    public override bool Avaliable => EquipType != null;
    public EnumRarity Rarity { get; set; }
    public EnumEquipType? EquipType { get; set; }
    public EnumItemType EnumItemType { get; set; }

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


      return result;
    }
  }
}
