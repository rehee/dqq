using DQQ.Commons;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Mobs;
using DQQ.Profiles.Skills;
using DQQ.Strategies;
using DQQ.Strategies.SkillStrategies;
using DQQ.Tags;
using ReheeCmf.Commons.Jsons.Options;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System.Text.Json;

namespace DQQ.Components.Skills
{
	public class SkillComponent : DQQComponent
	{
		public static SkillComponent New(EnumSkill skill, EnumSkillSlot skillSlot = EnumSkillSlot.NotSpecified)
		{
			var skillComponent = new SkillComponent();
			var skillProfile = DQQPool.TryGet<SkillProfile, EnumSkill?>(skill);
			skillComponent.InitSkillProfile(skillProfile, skillSlot);
			return skillComponent;
		}
		public SkillStrategy[]? SkillStrategies { get; set; }
		public static SkillComponent New(MobSkill skill, EnumSkillSlot skillSlot = EnumSkillSlot.NotSpecified)
		{
			var skillComponent = new SkillComponent();
			var skillProfile = DQQPool.TryGet<SkillProfile, EnumSkill?>(skill.SkillNumber);
			skillComponent.InitSkillProfile(skillProfile, skillSlot);
			skillComponent.SkillStrategies = skill.Strategies;
			return skillComponent;
		}
		public EnumSkillSlot Slot { get; set; }
		public decimal CastTime { get; set; }
		public decimal Cooldown { get; set; }
		public bool CastWithWeaponSpeed { get; set; }
		public decimal DamageRate { get; set; }

		public int CastTick => Convert.ToInt32(CastTime * DQQGeneral.TickPerSecond);
		public int CDTick => Convert.ToInt32(Cooldown * DQQGeneral.TickPerSecond);
		public int CastTickCount { get; set; }
		public int CDTickCount { get; set; }
		public IEnumerable<ITag>? Tags { get; set; }

		public SkillProfile? SkillProfile { get; protected set; }

		public int TotalCount { get; set; } = 0;
		public int WaveCount { get; set; } = 0;

		public override void Initialize(IDQQEntity entity)
		{
			base.Initialize(entity);
			if (entity is SkillEntity sp)
			{
				try
				{
					var skillProfile = DQQPool.TryGet<SkillProfile, EnumSkill?>(sp.SkillNumber);
					InitSkillProfile(skillProfile, sp.Slot);
					SkillStrategies = String.IsNullOrEmpty(sp.SkillStrategy) ? null :
					JsonSerializer.Deserialize<SkillStrategy[]?>(sp.SkillStrategy ?? "", JsonOption.DefaultOption);
				}
				catch
				{

				}

			}
		}

		public void InitSkillProfile(SkillProfile? profile, EnumSkillSlot skillSlot = EnumSkillSlot.NotSpecified)
		{
			SkillProfile = profile;
			CastTime = profile?.CastTime ?? 0;
			Cooldown = profile?.CoolDown ?? 0;
			DamageRate = profile?.DamageRate ?? 0;
			CastWithWeaponSpeed = profile?.CastWithWeaponSpeed ?? false;
			this.Slot = skillSlot;
		}

		public int CastWithWeaponSpeedTick(ITarget? caster)
		{
			if (CastWithWeaponSpeed != true || caster?.CombatPanel?.DynamicPanel.AttackPerSecond == null || caster?.CombatPanel?.DynamicPanel.AttackPerSecond == 0)
			{
				return CastTick;
			}

			return (int)((1 / caster?.CombatPanel?.DynamicPanel.AttackPerSecond!) * DQQGeneral.TickPerSecond);

		}

		public override async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
		{
			var result = await base.OnTick(parameter);
			if (!result.Success)
			{
				return result;
			}
			result.SetError();
			var target = parameter.From?.Target;
			if (CDTickCount > 0)
			{
				CDTickCount--;
				return result;
			}
			if (CastTickCount < CastWithWeaponSpeedTick(parameter.From))
			{
				CastTickCount++;
				return result;
			}
			var matchCondition = StrategeCheckResult.New(false, null);
			if (SkillStrategies?.Any() == true)
			{
				matchCondition = StrategyHelper.MatchSkillStrategy(SkillStrategies, parameter, this);
			}
			else
			{
				matchCondition = StrategeCheckResult.New(true, null);
			}

			if (!matchCondition.Matched)
			{
				return result;
			}
			var skillParameter = ComponentTickParameter.New(parameter, matchCondition.MatchedTarget);
			if (SkillProfile != null)
			{
				result = await SkillProfile.CastSkill(skillParameter);
			}
			else
			{
				result.SetSuccess(true);
			}
			TotalCount++;
			WaveCount++;
			CastTickCount = 0;
			if (CastWithWeaponSpeedTick(skillParameter.From) <= 0 && CDTick <= 0)
			{
				CDTickCount = (int)(DQQGeneral.MinCooldown * DQQGeneral.TickPerSecond);
			}
			else
			{
				CDTickCount = CDTick;
			}

			return result;
		}



		public virtual SkillEntity ToSkillEntity(Guid? actorId = null)
		{
			var result = new SkillEntity();
			result.Id = DisplayId ?? Guid.NewGuid();
			result.Name = DisplayName;
			result.SkillNumber = SkillProfile?.SkillNumber;
			result.ActorId = actorId;
			return result;
		}
	}
}
