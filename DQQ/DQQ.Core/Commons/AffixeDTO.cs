using DQQ.Components.Affixes;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Affixes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DQQ.Commons
{
	public class AffixeDTO
	{

		public static AffixeDTO New(AffixeComponent component)
		{
			return new AffixeDTO
			{
				AffixeNumber = component.AffixeNumber,
				AffixeGroup = component.AffixeProfile?.AffixeGroup ?? EnumAffixeGroup.NotSpecified,
			};
		}
		public EnumAffixeNumber AffixeNumber { get; set; }
		public EnumAffixeGroup AffixeGroup { get; set; }

		[JsonIgnore]
		public int AffixeLevel => Profile?.AffixeLevel ?? 0;
		[JsonIgnore]
		public int TierLevel => Profile?.TierLevel ?? 0;
		[JsonIgnore]
		public bool IsPrefix => Profile?.IsPrefix ?? true;
		[JsonIgnore]
		public string? Name => Profile?.Name;
		[JsonIgnore]
		public AffixeProfile? Profile => DQQPool.TryGet<AffixeProfile, EnumAffixeNumber>(AffixeNumber);
	}
}
