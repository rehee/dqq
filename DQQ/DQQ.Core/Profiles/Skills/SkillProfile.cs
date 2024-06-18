using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using ReheeCmf.Responses;
using ReheeCmf.Helpers;
using DQQ.Components.Parameters;
using DQQ.Combats;

namespace DQQ.Profiles.Skills
{
	public abstract class SkillProfile : DQQProfile<EnumSkillNumber>, IWIthAttackTypeAndArea, ISetAttackTypeAndArea
	{
		public virtual int CharacterLevelRequired => 0;
		public virtual EnumSkillTag[]? OriginalTag => [];
		public virtual EnumSkillTag[]? SupportableTag => [];
		public virtual EnumSkillTag[]? ExtureTagAdded { get; set; }

		public virtual EnumDamageHand DamageHand => EnumDamageHand.Any;
		public virtual EnumSkillBindingType BindingType => EnumSkillBindingType.Active;
		public abstract bool NoPlayerSkill { get; }
		public virtual EnumSkillCategory Category => EnumSkillCategory.NotSpecified;
		public virtual bool SelfTarget { get; }
		public virtual bool SelfIfNoTarget { get; }
		public abstract decimal CastTime { get; }
		public abstract decimal CoolDown { get; }
		public abstract decimal DamageRate { get; }
		public abstract bool CastWithWeaponSpeed { get; }
		public EnumSkillNumber SkillNumber => ProfileNumber;
		public virtual EnumSkillType SkillType => EnumSkillType.Damage;
		public string? SkillName => Name;

		public virtual EnumAreaLevel AreaLevel { get; set; } = EnumAreaLevel.Single;
		public virtual EnumAttackType AttackTypes { get; set; }
		public int ExtraAttackNumber { get; set; }

		public virtual bool IsAvaliableTarget(ITarget? target)
		{
			return target?.Alive == true;
		}

		public bool IsSupportAvaliable(IEnumerable<EnumSkillTag>? targetTags)
		{
			if (BindingType != EnumSkillBindingType.Support)
			{
				return false;
			}
			if (SupportableTag?.Any() != true)
			{
				return true;
			}
			return SupportableTag.Intersect(targetTags ?? []).Any();
		}

		public virtual DamageDeal[] SupportDamageCalculate(DamageDeal[] damage, ComponentTickParameter? parameter)
		{
			return damage;
		}

		public virtual DamageDeal[] CalculateDamage(ComponentTickParameter? parameter)
		{
			var result = DamageHelper
				.SkillDamage(this, parameter?.From!, parameter?.Map, parameter)
				.SkillMordifier(parameter?.From)
				.SupportSkillDamage(parameter?.SupportSkills?.Select(b => b.SkillProfile), parameter);

			return result;
		}

		protected virtual async Task DealingDamage(ComponentTickParameter? parameter, DamageDeal[] damageDeals, IMap? map)
		{
			var damageWithDeal = damageDeals.Where(b => b.DamagePoint > 0).ToArray();
			DamageTaken? damageTaken = null;
			if (parameter?.SelectedTarget != null)
			{
				damageTaken = parameter!.SelectedTarget.TakeDamage(ComponentTickParameter.New(parameter, this, damageWithDeal));
			}
			parameter?.From?.DamageHandCheck(DamageHand);


			if (damageTaken?.HitCheck == EnumHitCheck.Hit)
			{
				await AfterDealingDamage(ComponentTickParameter.New(parameter, damageTaken));
			}
		}
		protected virtual async Task AfterDealingDamage(ComponentTickParameter parameter)
		{
			await parameter.AfterDealingDamage();

			if (parameter?.TriggerSkills?.Any() == true)
			{
				foreach (var trigger in parameter.TriggerSkills)
				{
					await trigger.AfterDealingDamage(parameter);
				}
			}
		}
		public virtual async Task<ContentResponse<bool>> CastSkill(ComponentTickParameter? parameter)
		{
			TargetOverride(parameter);
			await Task.CompletedTask;
			var response = new ContentResponse<bool>();
			if (BindingType == EnumSkillBindingType.Support)
			{
				response.SetSuccess(true);
				return response;
			}
			var selectedTarget = parameter?.SelectedTarget;
			if (IsAvaliableTarget(selectedTarget) != true)
			{
				return response;
			}
			parameter?.SetAttackAndAreaType(this, parameter?.SupportSkills?.Select(b => b.SkillProfile));

			parameter?.Map!.AddMapLogSpillCast(true, parameter?.From, parameter?.SelectedTarget, this);
			response.SetSuccess(true);


			if (SkillType == EnumSkillType.Damage || SkillType == EnumSkillType.Hybrid)
			{
				var damage = CalculateDamage(parameter);
				var targets = parameter!.SkillEffectTarget();
				if (targets?.Count() > 1)
				{
					foreach (var target in targets)
					{
						await DealingDamage(ComponentTickParameter.New(parameter, target), damage, parameter?.Map);
					}
				}
				else
				{
					await DealingDamage(parameter, damage, parameter?.Map);
				}

			}

			if (SkillType == EnumSkillType.Healing || SkillType == EnumSkillType.Hybrid)
			{
				var healing = CalculateHealing(parameter);
				DealingHealing(ComponentTickParameter.New(parameter, healing));
			}
			return response;
		}

		public virtual SkillHitCheck? CheckHit(ComponentTickParameter parameter)
		{
			return null;
		}

		protected virtual HealingDeal[] CalculateHealing(ComponentTickParameter? parameter)
		{
			return [];
		}
		protected virtual void DealingHealing(ComponentTickParameter? parameter)
		{
			parameter?.To?.TakeHealing(parameter);
			//if (parameter?.Healings?.Any() != true)
			//{
			//	return;
			//}
			//parameter.To?.TakeHealing(from, h.Points, map, this);
		}

		public virtual void SetAttackTypeAndArea(IWIthAttackTypeAndArea input)
		{

		}


		public virtual int GetDurationSeconds()
		{
			return 0;
		}
		public virtual long GetDurationPower(ComponentTickParameter? parameter = null)
		{
			return 0;
		}
		public virtual ITarget? GetTarget(ComponentTickParameter? parameter = null)
		{
			if (SelfTarget)
			{
				return parameter?.From;
			}
			if (SelfIfNoTarget)
			{
				return parameter?.SelfCastTarget;
			}
			return parameter?.To;
		}


		public virtual EnumTarget? TargetForce => null;

		public virtual void TargetOverride(ComponentTickParameter? parameter)
		{
			if (parameter == null || TargetForce == null)
			{
				return;
			}
			switch (TargetForce)
			{
				case EnumTarget.Target:
					if (parameter?.To == null || parameter?.FriendlyTargets?.Contains(parameter?.To) == true)
					{
						parameter.SecondaryTarget = parameter?.From?.Target;
					}
					break;
				case EnumTarget.Self:
					parameter.SecondaryTarget = parameter.From;
					break;
				case EnumTarget.Friendly:
					if (parameter?.To == null || parameter?.EnemyTargets?.Contains(parameter?.To) == true)
					{
						parameter.SecondaryTarget = parameter?.From;
					}
					break;
			}

		}
	}
}
