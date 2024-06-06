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
	public class ChaosDamageProfileT05 : ChaosDamageProfile
	{
		public override int AffixeLevel => 50;
		public override int TierLevel => 5;
		public override string? Name => "黑暗";
		protected override EnumAffixeNumber number => EnumAffixeNumber.ChaosDamageProfileT05;
		protected override int min => 41;
		protected override int max => 51;
	}
}
