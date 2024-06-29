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
	public class GeneralMaterial: AbCurrency
	{
		public override EnumItem ProfileNumber => EnumItem.GeneralMaterial;
		public override string? Name => "耗材";
		public override string? Discription => "";
		public override decimal Rarity => 1m;
	}
}
