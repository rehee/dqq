using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.TwoHandWeapons.Bows
{
  public abstract class AbTwoHandMace : AbTwoHandWeapon
  {
    public override EnumItemType? ItemType => EnumItemType.Mace;
  }
}
