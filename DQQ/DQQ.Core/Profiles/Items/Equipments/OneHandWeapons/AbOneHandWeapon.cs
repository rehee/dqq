using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.OneHandWeapons
{
  public abstract class AbOneHandWeapon : WeopnProfile
  {
    public override EnumEquipType? EquipType => EnumEquipType.OneHandWeapon;
    public override long DamagePerSecond => 3;
  }
}
