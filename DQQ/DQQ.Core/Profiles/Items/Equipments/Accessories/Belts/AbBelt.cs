using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Accessories.Belts
{
  public abstract class AbBelt : EquipProfile
  {
    public override EnumItemType? ItemType => EnumItemType.Belt;
    public override EnumEquipType? EquipType => EnumEquipType.Belt;
  }
}
