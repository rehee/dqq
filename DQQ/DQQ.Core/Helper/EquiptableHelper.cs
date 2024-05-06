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
      eChar.CombatPropertySummary(eChar.Equips.Where(b => b.Value != null).Select(b => b.Value));
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
  }
}
