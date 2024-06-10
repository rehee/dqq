using DQQ.Commons;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using ReheeCmf.Responses;
using ReheeCmf.Helpers;
using DQQ.Components.Parameters;
using DQQ.Components.Stages.Actors;
using DQQ.Combats;
using System.Reflection.Metadata;

namespace DQQ.Profiles.Skills
{
	public abstract class SkillProfile : DQQProfile<EnumSkill>
	{
		public virtual EnumDamageHand DamageHand => EnumDamageHand.Any;
		public virtual EnumSkillBindingType BindingType => EnumSkillBindingType.Active;
		public abstract bool NoPlayerSkill { get; }
		public virtual EnumSkillCategory Category => EnumSkillCategory.NotSpecified;
		protected virtual bool SelfTarget { get; }
		public abstract decimal CastTime { get; }
		public abstract decimal CoolDown { get; }
		public abstract decimal DamageRate { get; }
		public abstract bool CastWithWeaponSpeed { get; }
		public EnumSkill SkillNumber => ProfileNumber;
		public virtual EnumSkillType SkillType => EnumSkillType.Damage;
		public string? SkillName => Name;



		public virtual DamageDeal[] CalculateDamage(ITarget? caster, IMap? map)
		{
			return DamageHelper.SkillDamage(this, caster!, map).SkillMordifier(caster);
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
		protected virtual async Task AfterDealingDamage(AfterDealingDamageParameter? parameter)
		{
			await parameter.AfterDealingDamage();
		}
		public virtual async Task<ContentResponse<bool>> CastSkill(ComponentTickParameter? parameter)
		{
			await Task.CompletedTask;
			var response = new ContentResponse<bool>();
			var selectedTarget = parameter?.SelectedTarget;
			if (selectedTarget?.Alive != true)
			{
				return response;
			}
			parameter?.Map!.AddMapLogSpillCast(true, parameter?.From, parameter?.SelectedTarget, this);
			response.SetSuccess(true);
			if (SkillType == EnumSkillType.Damage || SkillType == EnumSkillType.hybrid)
			{
				var damage = CalculateDamage(parameter?.From, parameter?.Map);
				await DealingDamage(parameter, damage, parameter?.Map);
			}

			if (SkillType == EnumSkillType.Healing || SkillType == EnumSkillType.hybrid)
			{
				var healing = CalculateHealing(parameter?.From, parameter?.Map);
				DealingHealing(parameter?.From, healing, parameter?.Map);
			}
			return response;
		}

		public virtual SkillHitCheck? CheckHit(BeforeDamageTakenParameter parameter)
		{
			return null;
		}

		protected virtual HealingDeal[] CalculateHealing(ITarget? caster, IMap? map)
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
