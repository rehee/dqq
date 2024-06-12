using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using ReheeCmf.Responses;
using ReheeCmf.Helpers;
using DQQ.Components.Parameters;

namespace DQQ.Profiles.Skills
{
	public abstract class SkillProfile : DQQProfile<EnumSkillNumber>
	{
		public virtual EnumSkillTag[]? OriginalTag { get; set; }
		public virtual EnumSkillTag[]? SupportableTag { get; set; }

		public virtual EnumSkillTag[]? ExtureTagAdded { get; set; }

		public virtual EnumDamageHand DamageHand => EnumDamageHand.Any;
		public virtual EnumSkillBindingType BindingType => EnumSkillBindingType.Active;
		public abstract bool NoPlayerSkill { get; }
		public virtual EnumSkillCategory Category => EnumSkillCategory.NotSpecified;
		protected virtual bool SelfTarget { get; }
		public abstract decimal CastTime { get; }
		public abstract decimal CoolDown { get; }
		public abstract decimal DamageRate { get; }
		public abstract bool CastWithWeaponSpeed { get; }
		public EnumSkillNumber SkillNumber => ProfileNumber;
		public virtual EnumSkillType SkillType => EnumSkillType.Damage;
		public string? SkillName => Name;

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
			var result = DamageHelper.SkillDamage(this, parameter?.From!, parameter?.Map).SkillMordifier(parameter?.From);
			if (parameter?.SupportSkills?.Any() == true)
			{
				foreach (var support in parameter.SupportSkills)
				{
					if (support?.SkillProfile != null)
					{
						result = support.SkillProfile.SupportDamageCalculate(result, parameter);
					}
				}
			}
			return result;
		}

		protected virtual async Task DealingDamage(ComponentTickParameter? parameter, DamageDeal[] damageDeals, IMap? map)
		{
			var damageWithDeal = damageDeals.Where(b => b.DamagePoint > 0).ToArray();
			DamageTaken? damageTaken = null;
			if (parameter?.SelectedTarget != null)
			{
				damageTaken = parameter!.SelectedTarget.TakeDamage(BeforeDamageTakenParameter.New(parameter, this, damageWithDeal));
			}
			if (DamageHand == EnumDamageHand.EachHand && parameter?.From?.CombatPanel.IsDuelWield == true)
			{
				if (parameter?.From.PrevioursMainHand == null)
				{
					parameter!.From.PrevioursMainHand = true;
				}
				else
				{
					parameter!.From.PrevioursMainHand = !parameter?.From.PrevioursMainHand;
				}
			}
			if (damageTaken?.HitCheck == EnumHitCheck.Hit)
			{
				await AfterDealingDamage(AfterDealingDamageParameter.New(parameter, damageTaken));
			}
		}
		protected virtual async Task AfterDealingDamage(AfterDealingDamageParameter parameter)
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
			parameter?.Map!.AddMapLogSpillCast(true, parameter?.From, parameter?.SelectedTarget, this);
			response.SetSuccess(true);
			if (SkillType == EnumSkillType.Damage || SkillType == EnumSkillType.Hybrid)
			{
				var damage = CalculateDamage(parameter);

				await DealingDamage(parameter, damage, parameter?.Map);
			}

			if (SkillType == EnumSkillType.Healing || SkillType == EnumSkillType.Hybrid)
			{
				var healing = CalculateHealing(parameter);
				DealingHealing(parameter?.From, healing, parameter?.Map);
			}
			return response;
		}

		public virtual SkillHitCheck? CheckHit(BeforeDamageTakenParameter parameter)
		{
			return null;
		}

		protected virtual HealingDeal[] CalculateHealing(ComponentTickParameter? parameter)
		{
			return [];
		}
		protected virtual void DealingHealing(ITarget? from, HealingDeal[] healings, IMap? map)
		{
			foreach (var h in healings.Where(b => b.HealingType == EnumHealingType.DirectHeal))
			{
				from?.TakeHealing(from, h.Points, map, this);
			}
		}


	}
}
