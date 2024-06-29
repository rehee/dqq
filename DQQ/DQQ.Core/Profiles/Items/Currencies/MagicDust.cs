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
	public class MagicDust: AbCurrency
	{
		public override EnumItem ProfileNumber => EnumItem.MagicDust;
		public override string? Name => "魔尘";
		public override string? Discription => "";
		public override decimal Rarity => 1m;
	}
}
