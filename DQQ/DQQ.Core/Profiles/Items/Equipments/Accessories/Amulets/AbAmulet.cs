using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.Accessories.Amulets
{
  public abstract class AbAmulet : EquipProfile
  {
    public override EnumItemType? ItemType => EnumItemType.Amulet;
    public override EnumEquipType? EquipType => EnumEquipType.Amulet;
  }
}
