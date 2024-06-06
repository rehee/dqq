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
	public class ChaosDamageProfileT02 : ChaosDamageProfile
	{
		public override int AffixeLevel => 20;
		public override int TierLevel => 2;

		public override string? Name => "腐蚀";

		protected override EnumAffixeNumber number => EnumAffixeNumber.ChaosDamageProfileT02;

		protected override int min => 13;
		protected override int max => 20;
	}
}
