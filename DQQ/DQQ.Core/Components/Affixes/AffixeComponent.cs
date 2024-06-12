using DQQ.Combats;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Affixes;
using System.Text.Json.Serialization;

namespace DQQ.Components.Affixes
{
	public class AffixeComponent : DQQComponent
	{
		public EnumAffixeNumber AffixeNumber { get; set; }
		[JsonIgnore]
		public AffixeProfile? AffixeProfile => DQQPool.TryGet<AffixeProfile, EnumAffixeNumber>(AffixeNumber);
		public AffixPower[]? Powers { get; set; }
		public void SetProperty(ICombatProperty? property)
		{
			if (property == null || Powers?.Any() != true)
			{
				return;
			}
			foreach (var p in Powers)
			{
				p.SetProperty(property);
			}
		}
	}
}

