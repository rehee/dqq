using DQQ.Components.Items.Equips;
using DQQ.Enums;
using System.Numerics;

namespace DQQ.Profiles.Items.Equipments
{
  public abstract class WeopnProfile : EquipProfile
  {
    public abstract decimal AttackPerSecond { get; }
    public abstract Int64 DamagePerSecond { get; }

    public override EquipComponent GenerateEquipComponent(int? itemLevel, EnumRarity rarity = EnumRarity.Normal)
    {
      var equip = base.GenerateEquipComponent(itemLevel, rarity);
      equip.AttackPerSecond = AttackPerSecond;

      var attackPerHit = (int)Math.Round(DamagePerSecond / AttackPerSecond, 0);
      if (attackPerHit <= 0)
      {
        attackPerHit = 1;
      }
      switch (EquipType)
      {
        case EnumEquipType.OneHandWeapon:
          equip.MainHand = attackPerHit;
          equip.OffHand = attackPerHit;
          break;
        case EnumEquipType.TwoHandWeapon:
        case EnumEquipType.MainHandWeapon:
          equip.MainHand = attackPerHit;
          break;
      }

      return equip;
    }
  }
}
