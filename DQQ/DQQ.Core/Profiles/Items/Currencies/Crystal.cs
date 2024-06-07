using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Items.Currencies
{
  [Pooled]
  public class Crystal : ItemProfile
  {
    public override EnumItem ProfileNumber => EnumItem.Crystal;
    public override string? Name => "水晶";
    public override string? Discription => "";
    public override bool IsStack => true;
    public override int DropQuantity => 1;

    public override decimal Rarity => 1m;

		public override EnumItemType? ItemType => EnumItemType.Currency;
	}
}
