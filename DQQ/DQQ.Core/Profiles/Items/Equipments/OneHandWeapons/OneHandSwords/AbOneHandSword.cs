using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.OneHandWeapons.OneHandSwords
{
  public abstract class AbOneHandSword : AbOneHandWeapon
  {
    public override EnumItemType? ItemType => EnumItemType.Sword;
  }
}
