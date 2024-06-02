using DQQ.Enums;

namespace DQQ.Profiles.Items.Equipments.TwoHandWeapons
{
  public abstract class AbTwoHandWeapon : WeopnProfile
  {
    public override EnumEquipType? EquipType => EnumEquipType.TwoHandWeapon;
    public override decimal DamageMultiple => 1.75m;

  }
}
