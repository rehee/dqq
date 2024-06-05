using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.OneHandWeapons.Scepters
{
  public abstract class AbWand : AbOneHandWeapon
  {
    public override EnumItemType? ItemType => EnumItemType.Wand;
    public override EnumEquipType? EquipType => EnumEquipType.MainHandWeapon;
  }
}
