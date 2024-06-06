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
	public class ChaosDamageProfileT07 : ChaosDamageProfile
	{
		public override int AffixeLevel => 70;
		public override int TierLevel => 7;
		public override string? Name => "绝望";
		protected override EnumAffixeNumber number => EnumAffixeNumber.ChaosDamageProfileT07;
		protected override int min => 67;
		protected override int max => 75;
	}
}
