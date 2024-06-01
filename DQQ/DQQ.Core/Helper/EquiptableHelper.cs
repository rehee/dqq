using DQQ.Combats;
using DQQ.Components.Items.Equips;
using DQQ.Enums;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;

namespace DQQ.Helper
{
  public static class EquiptableHelper
  {
    public static void TotalEquipProperty(this IEquippableCharacter eChar)
    {
      if (eChar.CombatPanel != null)
      {
        eChar.CombatPanel.StaticPanel.CombatPropertySummary(eChar.Equips.Where(b => b.Value != null).Select(b => new CombatPropertySum { Slot = b.Key, Property = b.Value }));

      }
    }
    public static ContentResponse<bool> Equip(this IEquippableCharacter eChar, EnumEquipSlot slot, IEquptment? equipComponent)
    {
      var result = new ContentResponse<bool>();
      if (equipComponent == null)
      {
        eChar.Equips.AddOrUpdate(slot, default(IEquptment?), (b, c) => default(IEquptment?));
        eChar.TotalEquipProperty();
        result.SetSuccess(true);
        return result;
      }
      switch (slot)
      {
        case EnumEquipSlot.MainHand:
          if (!(equipComponent.EquipType == EnumEquipType.TwoHandWeapon || equipComponent.EquipType == EnumEquipType.OneHandWeapon || equipComponent.EquipType == EnumEquipType.MainHandWeapon))
          {
            return result;
          }
          if (equipComponent.EquipType == EnumEquipType.TwoHandWeapon)
          {
            eChar.Equips.AddOrUpdate(EnumEquipSlot.OffHand, default(IEquptment?), (b, c) => default(IEquptment?));
          }
          eChar.Equips.AddOrUpdate(slot, equipComponent, (b, c) => equipComponent);
          break;
        case EnumEquipSlot.OffHand:
          if (!(equipComponent.EquipType == EnumEquipType.Offhand || equipComponent.EquipType == EnumEquipType.OneHandWeapon))
          {
            return result;
          }
          if (eChar.Equips.TryGetValue(EnumEquipSlot.MainHand, out var mh))
          {
            if (mh != null && mh!.EquipType == EnumEquipType.TwoHandWeapon)
            {
              eChar.Equips.AddOrUpdate(EnumEquipSlot.MainHand, default(IEquptment?), (b, c) => default(IEquptment?));
            }
          }
          eChar.Equips.AddOrUpdate(slot, equipComponent, (b, c) => equipComponent);
          break;
        default:
          if (!(slot == equipComponent.EquipSlot || slot == equipComponent.SecondEquipSlot))
          {
            return result;
          }
          eChar.Equips.AddOrUpdate(slot, equipComponent, (b, c) => equipComponent);
          break;
      }
      result.SetSuccess(true);
      eChar.TotalEquipProperty();
      return result;
    }

    public static EnumEquipSlot? GetMainSlot(this EnumEquipType type)
    {
      switch (type)
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
    public static EnumEquipSlot? GetSecondSlot(this EnumEquipType type)
    {
      switch (type)
      {
        case EnumEquipType.Ring:
          return EnumEquipSlot.LeftRing;
        case EnumEquipType.OneHandWeapon:
          return EnumEquipSlot.OffHand;
        default:
          return null;
      }
    }

    public static IEnumerable<EnumEquipSlot>? GetAvaliableSlots(this EnumEquipType? type)
    {
      if (type == null)
      {
        return null;
      }
      var result = new EnumEquipSlot?[]
      {
        type.Value.GetMainSlot(),
        type.Value.GetSecondSlot()
      };
      return result.Where(b => b != null).Select(b => b.Value).ToArray();
    }

    public static void SetCompatProperty(this ICombatProperty? from, ICombatProperty? to)
    {
      if (from == null || to == null)
      {
        return;
      }
      to.MaximunLife = from.MaximunLife;
      to.Armor = from.Armor;
      to.Damage = from.Damage;
      to.AttackPerSecond = from.AttackPerSecond;
      to.ArmorPercentage = from.ArmorPercentage;
      to.Resistance = from.Resistance;
      to.MainHand = from.MainHand;
      to.OffHand = from.OffHand;
      to.AttackRating = from.AttackRating;
    }
  }
}
