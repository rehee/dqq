using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Affixes.Prefixes.Damages.ChaosDamages
{
	public abstract class ChaosDamageProfile : DamageProfile
	{
		protected override EnumPropertyType propertyType => EnumPropertyType.ChaosDamageModifier;
		protected override string discription => "混乱";
	}
}
