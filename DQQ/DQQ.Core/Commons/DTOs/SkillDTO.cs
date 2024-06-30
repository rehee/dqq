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
				SkillStrategies = component?.SkillStrategies?.OrderBy(b => b.Priority).ToList() ?? [],
				SupportSkills = component?.SupportSkills?.ToList(),
				AvaliableForUser = component?.AvaliableForUser == true
			};
		}
		public static SkillDTO New(EnumSkillNumber skill, EnumSkillSlot? slot = null)
		{
			var supportSkills = new List<SkillDTO>();
			if (slot != null)
			{
				var maxSkills = slot.MaxSkillNumber();
				for (var i = 0; i < maxSkills; i++)
				{
					supportSkills.Add(SkillDTO.New(EnumSkillNumber.NotSpecified));
				}
			}
			return new SkillDTO
			{
				SkillNumber = skill,
				SupportSkills = supportSkills
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
		public List<SkillStrategyDTO>? SkillStrategies { get; set; }

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
