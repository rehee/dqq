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
	public class ChaosDamageProfileT06 : ChaosDamageProfile
	{
		public override int AffixeLevel => 60;
		public override int TierLevel => 6;
		public override string? Name => "混乱";
		protected override EnumAffixeNumber number => EnumAffixeNumber.ChaosDamageProfileT06;
		protected override int min => 61;
		protected override int max => 66;
	}
}
