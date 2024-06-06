using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Suffixes.Lifes
{
	public abstract class IncreasedLifeSuffixe : SuffixProfile
	{
		public override EnumAffixeGroup AffixeGroup => EnumAffixeGroup.IncreasedLife;
		public override string? Discription => "增加生命";
	}
}
