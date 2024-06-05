using DQQ.Enums;
using DQQ.Profiles.Items.Equipments.Armors.ArmorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Armors.BodyArmor
{
  public abstract class AbHelmet<T> : AbArmor<T> where T : class, IArmorType, new()
  {
    public override EnumItemType? ItemType => EnumItemType.Helmet;
    public override EnumEquipType? EquipType => EnumEquipType.Helmet;
    public override decimal PowerPercentage => 1m;
  }
}
