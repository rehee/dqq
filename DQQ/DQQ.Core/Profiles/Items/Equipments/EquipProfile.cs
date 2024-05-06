using DQQ.Components.Items;
using DQQ.Components.Items.Equips;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Equipments
{
  public abstract class EquipProfile : ItemProfile
  {
    public override bool IsStack => false;
    public abstract EnumEquipType EquipType { get; }

    public virtual EquipComponent GenerateEquipComponent(int? itemLevel)
    {
      var result = EquipComponent.New<EquipComponent>();
      result.Initialize(this, itemLevel);
      return result;
    }

    public override ItemComponent GenerateComponent(int? itemLevel, int? quantity)
    {
      return GenerateEquipComponent(itemLevel);
    }
  }
}
