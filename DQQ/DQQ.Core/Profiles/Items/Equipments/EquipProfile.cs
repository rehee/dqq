using DQQ.Commons;
using DQQ.Components.Items;
using DQQ.Components.Items.Equips;
using DQQ.Enums;
using DQQ.Helper;
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
    public abstract EnumEquipType? EquipType { get; }
    public abstract EnumItemType? ItemType { get; }
    public override int DropQuantity => 1;

    public virtual EquipComponent GenerateEquipComponent(int? itemLevel, EnumRarity rarity = EnumRarity.Normal)
    {
      var result = EquipComponent.New<EquipComponent>();
      result.Initialize(this, itemLevel);

      result.Rarity = rarity;
      result.InitialAffixes(result.GenerateAllAffieComponents());
      result.AppliedAffixes();
      return result;
    }



    public override ItemComponent GenerateComponent(int? itemLevel, int? quantity, EnumRarity rarity = EnumRarity.Normal)
    {
      return GenerateEquipComponent(itemLevel, rarity);
    }
  }
}
