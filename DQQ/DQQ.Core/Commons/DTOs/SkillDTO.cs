using DQQ.Components.Parameters;
using DQQ.Components.Skills;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using DQQ.Strategies.SkillStrategies;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DQQ.Commons.DTOs
{
	public class SkillDTO
	{
		public static SkillDTO New(SkillComponent component)
		{
			return new SkillDTO
			{
				SkillNumber = component?.SkillProfile?.SkillNumber ?? EnumSkillNumber.NotSpecified,
				SkillStrategies = component?.SkillStrategies?.OrderBy(b => b.Property).ToList() ?? [],
				SupportSkills = component?.SupportSkills?.ToList(),
				AvaliableForUser = component?.AvaliableForUser == true
			};
		}
		public static SkillDTO New(EnumSkillNumber skill)
		{
			return new SkillDTO
			{
				SkillNumber = skill
			};
		}
		public EnumSkillNumber SkillNumber { get; set; }
		[JsonIgnore]
		public SkillProfile? Profile => DQQPool.TryGet<SkillProfile, EnumSkillNumber>(SkillNumber);
		public string? SkillName => Profile?.SkillName;

		public decimal CastTime => Profile?.CastTime ?? 0;

		public bool CastWithWeaponSpeed => Profile?.CastWithWeaponSpeed ?? false;

		public decimal CoolDown => Profile?.CoolDown ?? 0;

		public decimal DamageRate => Profile?.DamageRate ?? 0;

		public string? Discription => Profile?.Discription;

		public bool NoPlayerSkill => Profile?.IsPlayerAvaliableSkill() != true;
		public List<SkillStrategy>? SkillStrategies { get; set; }

		public List<SkillDTO>? SupportSkills { get; set; }
		public bool AvaliableForUser { get; set; }

		public bool AbleToAddSupport(EnumSkillSlot? slot)
		{
			if (Profile == null) return false;
			var maxNumber = slot.MaxSkillNumber();
			return maxNumber > SupportSkills?.Count(b => b.Profile != null);
		}
	}
}
