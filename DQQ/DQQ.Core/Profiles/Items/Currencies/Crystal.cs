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
  public class Crystal : AbCurrency
	{
    public override EnumItem ProfileNumber => EnumItem.Crystal;
    public override string? Name => "水晶";
    public override string? Discription => "";
    public override decimal Rarity => 1m;

		
	}
}
