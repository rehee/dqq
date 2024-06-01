using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments.OneHandWeapons.Claws
{
  public abstract class AbClaw : AbOneHandWeapon
  {
    public override EnumItemType? ItemType => EnumItemType.Claw;
  }
}
