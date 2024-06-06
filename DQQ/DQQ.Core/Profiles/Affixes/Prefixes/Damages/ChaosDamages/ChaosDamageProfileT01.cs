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
	public class ChaosDamageProfileT01 : ChaosDamageProfile
	{
		public override int AffixeLevel => 10;
		public override int TierLevel => 1;

		public override string? Name => "腐烂";

		protected override EnumAffixeNumber number => EnumAffixeNumber.ChaosDamageProfileT01;

		protected override int min => 6;
		protected override int max => 12;
	}
}
