using DQQ.Attributes;
using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.ChaosDamages
{
	[Pooled]
	public class ChaosDamageProfileT03 : ChaosDamageProfile
	{
		public override int AffixeLevel => 30;
		public override int TierLevel => 3;

		public override string? Name => "剧毒";

		protected override EnumAffixeNumber number => EnumAffixeNumber.ChaosDamageProfileT03;

		protected override int min => 21;
		protected override int max => 31;
	}
}
