using DQQ.Components.Items.Equips;
using System.Numerics;

namespace DQQ.Profiles.Items.Equipments
{
  public abstract class WeopnProfile : EquipProfile
  {
    public abstract decimal AttackPerSecond { get; }
    public abstract Int64 BaseDamage { get; }

    public override EquipComponent GenerateEquipComponent(int? itemLevel)
    {
      var equip = base.GenerateEquipComponent(itemLevel);
      equip.AttackPerSecond = AttackPerSecond;

      switch (EquipType)
      {
        case Enums.EnumEquipType.OneHandWeapon:
          equip.MainHand = BaseDamage;
          equip.OffHand = BaseDamage;
          break;
        case Enums.EnumEquipType.TwoHandWeapon:
        case Enums.EnumEquipType.MainHandWeapon:
          equip.MainHand = BaseDamage;
          break;
      }

      return equip;
    }
  }
}
