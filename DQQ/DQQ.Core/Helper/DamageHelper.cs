using DQQ.Combats;
using DQQ.Commons;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class DamageHelper
	{
		public static Int64 BasicDamage(this EnumDamageHand? hand, ITarget? caster, IMap? map)
		{
			if (caster == null)
			{
				return 0;
			}
			var property = caster!.CombatPanel.DynamicPanel;
			var baseDamage = property.Damage ?? 0;
			if (caster is IActor actor)
			{
				baseDamage = baseDamage + actor.BasicDamage;
			}
			var attackSpeed = 1m;
			if (property?.AttackPerSecond <= 0 || property?.AttackPerSecond == null)
			{
				attackSpeed = 1;
			}
			else
			{
				attackSpeed = property.AttackPerSecond.Value;
			}
			baseDamage = Convert.ToInt64(baseDamage / attackSpeed);
			var mainHandDamage = (property.MainHand == null ? 0 : property.MainHand.Value) + baseDamage;
			var offHandDamage = (property.OffHand == null ? 0 : property.OffHand.Value) + baseDamage;
			if (hand == null)
			{
				return property.MainHand == null ? baseDamage : mainHandDamage;
			}
			switch (hand)
			{
				case EnumDamageHand.Any:
					return property.MainHand == null ? baseDamage : mainHandDamage;
				case EnumDamageHand.MainHand:
					return mainHandDamage == 0 ? baseDamage : mainHandDamage;
				case EnumDamageHand.OffHand:
					return offHandDamage;
				case EnumDamageHand.BothHand:
					return (mainHandDamage + offHandDamage) == 0 ? baseDamage : mainHandDamage + offHandDamage;
			}
			return baseDamage;
		}
		public static DamageDeal[] SkillDamage(this SkillProfile skill, ITarget caster, IMap? map, ComponentTickParameter? parameter)
		{
			var profile = DQQPool.TryGet<SkillProfile, EnumSkillNumber?>(skill.SkillNumber);
			if (profile == null || skill.DamageRate == 0)
			{
				return Enumerable.Empty<DamageDeal>().ToArray();
			}
			var damageHand = profile.DamageHand;
			if (profile.DamageHand == EnumDamageHand.EachHand)
			{
				if (caster.PrevioursMainHand == false || caster.PrevioursMainHand == null)
				{
					damageHand = EnumDamageHand.MainHand;
				}
				else
				{
					damageHand = EnumDamageHand.OffHand;
				}
			}
			var basicDamage = DamageHelper.BasicDamage(damageHand, caster, map);
			if (basicDamage == 0)
			{
				return Enumerable.Empty<DamageDeal>().ToArray();
			}
			var supportSpellRate = profile.DamageRate + (parameter?.SupportSkills?.Sum(b => b.DamageRate) ?? 0);
			var skillDamage = basicDamage.Percentage(supportSpellRate);
			return [DamageDeal.New(skillDamage)];
		}

		public static Int64 SkillMordifier(this long baseDamage, ITarget? caster)
		{
			var modifier = caster?.CombatPanel.DynamicPanel.DamageModifier ?? 0;
			if (modifier == 0)
			{
				return baseDamage;
			}
			var p = baseDamage.Percentage(modifier);
			return baseDamage + p;
		}
		public static DamageDeal[] SkillMordifier(this DamageDeal[] damageDeals, ITarget? caster)
		{
			return damageDeals.Select(b => b.SkillMordifier(caster)).ToArray();
		}
		public static DamageDeal[] SupportSkillDamage(this DamageDeal[] damageDeals, IEnumerable<SkillProfile?>? profiles, ComponentTickParameter? parameter)
		{
			if (profiles?.Any() != true)
			{
				return damageDeals;
			}
			var result = damageDeals;
			foreach (var support in profiles)
			{
				if (support != null)
				{
					result = support.SupportDamageCalculate(result, parameter);
				}
			}
			return result;
		}
		public static DamageDeal SkillMordifier(this DamageDeal damageDeals, ITarget? caster)
		{
			//TODO need Implement Another Type Of damage
			var modifier = caster?.CombatPanel.DynamicPanel.DamageModifier ?? 0;
			var damagePoint = damageDeals.DamagePoint.Percentage(modifier + 1);
			return DamageDeal.New(damagePoint, damageDeals.DamageType);
		}
		public static Int64 Percentage(this Int64? input, decimal percentage)
		{
			if (input == null)
			{
				return 0;
			}
			return input.Value.Percentage(percentage);
		}
		public static Int64 Percentage(this Int64 input, decimal percentage, int times = 100)
		{

			var multiple = (int)(percentage * times);
			return input * multiple / times;
		}

		public static void ArmorDamageReduction(this ComponentTickParameter? parameter)
		{
			if (parameter == null)
			{
				return;
			}
			var totalArmor = (parameter?.To?.CombatPanel?.DynamicPanel.Armor).DefaultValue() * (1 + (parameter?.To?.CombatPanel?.DynamicPanel.ArmorPercentage).DefaultValue());
			var attackerLevel = (parameter?.From?.Level).DefaultValue(1);
			foreach (var d in parameter.Damages?.ToArray() ?? [])
			{
				d.DamagePoint = (long)(d.DamagePoint * (1 - DQQGeneral.ArmorDamageReduction(attackerLevel, (long)totalArmor)));
			}
		}
	}
}
