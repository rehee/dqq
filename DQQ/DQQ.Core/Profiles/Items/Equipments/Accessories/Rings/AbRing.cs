using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Accessories.Rings
{
  public abstract class AbRing : EquipProfile
  {
    public override EnumItemType? ItemType => EnumItemType.Ring;
    public override EnumEquipType? EquipType => EnumEquipType.Ring;
  }
}
