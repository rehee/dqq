using DQQ.Combats;
using DQQ.Commons;
using DQQ.Components.Skills;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Profiles;

namespace DQQ.Components.Parameters
{
	public class ComponentTickParameter : ComponentBaseParameter, IWIthAttackTypeAndArea
	{
		private ComponentTickParameter()
		{
			// 私有构造函数
		}
		public static ComponentTickParameter New(int randomSeed)
		{
			return new ComponentTickParameter
			{
				Random = new Random(randomSeed)
			};
		}

		public static ComponentTickParameter New(ComponentTickParameter? parameter)
		{
			return new ComponentTickParameter
			{
				Random = parameter?.Random,
				From = parameter?.From,
				FriendlyTargets = parameter?.FriendlyTargets,
				EnemyTargets = parameter?.EnemyTargets,
				Map = parameter?.Map,
				SecondaryTarget = parameter?.SecondaryTarget,
				Damage = parameter?.Damage,
				TriggerSkills = parameter?.TriggerSkills,
				SubAttackSkills = parameter?.SubAttackSkills,
				SupportSkills = parameter?.SupportSkills,
				Trigger = parameter?.Trigger,
				Source = parameter?.Source,
				Damages = parameter?.Damages,
				ExtraAttackNumber = parameter?.ExtraAttackNumber ?? 0,
				AreaLevel = parameter?.AreaLevel ?? EnumAreaLevel.Single,
				AttackTypes = parameter?.AttackTypes ?? EnumAttackType.NotSpecified
			};
		}
		public static ComponentTickParameter New(ComponentTickParameter? parameter, HealingDeal[]? healings,DQQProfile? source)
		{
			var result = New(parameter);
			result.Healings = healings;
			result.Source= source;
			return result;
		}
		public static ComponentTickParameter New(ComponentTickParameter? parameter, HealingDeal[]? healings, DQQProfile? source, ITarget? from, ITarget? to, IMap? map)
		{
			var result = New(parameter);
			result.Healings = healings;
			result.From = from;
			result.SecondaryTarget = to;
			result.Map = map;
			result.Source = source;
			return result;
		}
		public static ComponentTickParameter New(ComponentTickParameter? parameter, ITarget? from, IEnumerable<ITarget>? friendlyTargets, IEnumerable<ITarget>? enemyTargets, IMap? map)
		{
			var result = New(parameter);
			result.From = from;
			result.FriendlyTargets = friendlyTargets;
			result.EnemyTargets = enemyTargets;
			result.Map = map;
			return result;

		}
		public static ComponentTickParameter New(ComponentTickParameter? parameter, ITarget? secondaryTarget)
		{
			var result = New(parameter);
			result.SecondaryTarget = secondaryTarget;
			return result;
		}
		public static ComponentTickParameter New(ComponentTickParameter? parameter, DamageTaken? damage)
		{
			var result = New(parameter);
			result.Damage = damage;
			return result;
		}
		public static ComponentTickParameter New(ComponentTickParameter? parameter, DQQProfile? source, params DamageDeal[]? rawDamages)
		{
			var result = New(parameter);
			result.Source = source;
			result.Damages = rawDamages;
			return result;
		}
		public static ComponentTickParameter New(ComponentTickParameter? parameter, ITarget? from, ITarget? to, IMap? map, DQQProfile? source, params DamageDeal[]? damages)
		{
			var result = New(parameter);
			result.From = from;
			result.SecondaryTarget = to;
			result.Map = map;
			result.Source = source;
			result.Damages = damages?.ToArray();
			return result;

		}
		public ITarget? From { get; set; }
		public ITarget? SecondaryTarget { get; set; }
		public IEnumerable<ITarget>? FriendlyTargets { get; set; }
		public IEnumerable<ITarget>? EnemyTargets { get; set; }
		public IMap? Map { get; set; }
		public ITarget? SelectedTarget => SecondaryTarget ?? From?.Target;
		public ITarget? To => SelectedTarget;
		public ITarget? SelfCastTarget => SecondaryTarget ?? From;
		public virtual void SetSupportSkill(
			SkillComponent? trigger,
			IEnumerable<SkillComponent>? supportSkills, IEnumerable<SkillComponent>? triggerSkills, IEnumerable<SkillComponent>? subAttackSkills)
		{
			Trigger = trigger;
			SupportSkills = supportSkills?.Where(b => b.SkillProfile?.BindingType == EnumSkillBindingType.Support).Distinct();
			TriggerSkills = triggerSkills?.Where(b => b.SkillProfile?.BindingType == EnumSkillBindingType.Trigger).Distinct();
			SubAttackSkills = subAttackSkills?.Where(b => b.SkillProfile?.BindingType == EnumSkillBindingType.Active).Distinct();
		}
		public IEnumerable<SkillComponent>? SupportSkills { get; set; }
		public IEnumerable<SkillComponent>? TriggerSkills { get; set; }
		public IEnumerable<SkillComponent>? SubAttackSkills { get; set; }

		public SkillComponent? Trigger { get; protected set; }
		public DamageTaken? Damage { get; set; }
		public DQQProfile? Source { get; set; }
		public DamageDeal[]? Damages { get; set; }
		public EnumAreaLevel AreaLevel { get; set; }
		public EnumAttackType AttackTypes { get; set; }
		public int ExtraAttackNumber { get; set; }
		public HealingDeal[]? Healings { get; set; }

	}

}
