﻿using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
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
		public EnumSkill SkillNumber { get; set; }
		[JsonIgnore]
		public SkillProfile? Profile => DQQPool.TryGet<SkillProfile, EnumSkill>(SkillNumber);
		public string? SkillName => Profile?.SkillName;

		public decimal CastTime => Profile?.CastTime ?? 0;

		public bool CastWithWeaponSpeed => Profile?.CastWithWeaponSpeed ?? false;

		public decimal CoolDown => Profile?.CoolDown ?? 0;

		public decimal DamageRate => Profile?.DamageRate ?? 0;

		public string? Discription => Profile?.Discription;

		public bool NoPlayerSkill => Profile?.NoPlayerSkill ?? false;
		public List<SkillStrategy>? SkillStrategies { get; set; }

	}
}
