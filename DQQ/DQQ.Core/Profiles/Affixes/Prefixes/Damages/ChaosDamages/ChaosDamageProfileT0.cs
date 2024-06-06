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
	public class ChaosDamageProfileT0 : ChaosDamageProfile
	{
		public override int AffixeLevel => 0;
		public override int TierLevel => 0;

		public override string? Name => "腐败";

		protected override EnumAffixeNumber number => EnumAffixeNumber.ChaosDamageProfileT0;

		protected override int min => 1;
		protected override int max => 5;
	}
}
