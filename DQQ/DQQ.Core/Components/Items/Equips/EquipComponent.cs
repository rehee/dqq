using DQQ.Combats;
using DQQ.Enums;
using DQQ.Profiles.Items;
using DQQ.Profiles.Items.Equipments;
using System.Numerics;

namespace DQQ.Components.Items.Equips
{
  public class EquipComponent : ItemComponent, IEquptment
  {
    public EnumEquipType EquipType { get; set; }

    public EnumEquipSlot? EquipSlot
    {
      get
      {
        switch (EquipType)
        {
          case EnumEquipType.Glove:
            return EnumEquipSlot.Gloves;
          case EnumEquipType.Boots:
            return EnumEquipSlot.Boots;
          case EnumEquipType.Helmet:
            return EnumEquipSlot.Head;
          case EnumEquipType.Amulet:
            return EnumEquipSlot.Amulet;
          case EnumEquipType.BodyArmor:
            return EnumEquipSlot.Body;
          case EnumEquipType.Ring:
            return EnumEquipSlot.RightRing;
          case EnumEquipType.Belt:
            return EnumEquipSlot.Belt;
          case EnumEquipType.TwoHandWeapon:
            return EnumEquipSlot.MainHand;
          case EnumEquipType.MainHandWeapon:
            return EnumEquipSlot.MainHand;
          case EnumEquipType.Offhand:
            return EnumEquipSlot.OffHand;
          case EnumEquipType.OneHandWeapon:
            return EnumEquipSlot.MainHand;
          default:
            return null;
        }
      }
    }
    public EnumEquipSlot? SecondEquipSlot
    {
      get
      {
        switch (EquipType)
        {
          case EnumEquipType.Ring:
            return EnumEquipSlot.LeftRing;
          case EnumEquipType.OneHandWeapon:
            return EnumEquipSlot.OffHand;
          default:
            return null;
        }
      }
    }

    public BigInteger? MaximunLife { get; set; }
    public BigInteger? Armor { get; set; }
    public BigInteger? Damage { get; set; }
    public decimal? AttackPerSecond { get; set; }
    public decimal? ArmorPercentage { get; set; }
    public decimal? Resistance { get; set; }
    public BigInteger? MainHand { get; set; }
    public BigInteger? OffHand { get; set; }
    public bool? PrevioursMainHand { get; set; }
    public override void Initialize(ItemProfile itemProfile, int? itemLevel, int? quanty = null)
    {
      base.Initialize(itemProfile, itemLevel, quanty);
      if (itemProfile is EquipProfile ep)
      {
        Quanty = 1;
        EquipType = ep.EquipType;

      }
    }

  }
}
