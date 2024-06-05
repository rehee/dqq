using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.Armors.ArmorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Armors.BodyArmor
{
  public abstract class AbBodyArmor<T> : AbArmor<T> where T : class, IArmorType, new()
  {
    public override EnumItemType? ItemType => EnumItemType.BodyArmor;
    public override EnumEquipType? EquipType => EnumEquipType.BodyArmor;
    public override decimal PowerPercentage => 1m;
  }
}
