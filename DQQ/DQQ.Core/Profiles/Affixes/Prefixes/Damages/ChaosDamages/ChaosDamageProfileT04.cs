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
	public class ChaosDamageProfileT04 : ChaosDamageProfile
	{
		public override int AffixeLevel => 40;
		public override int TierLevel => 4;
		public override string? Name => "瘟疫";
		protected override EnumAffixeNumber number => EnumAffixeNumber.ChaosDamageProfileT04;
		protected override int min => 31;
		protected override int max => 41;
	}
}
