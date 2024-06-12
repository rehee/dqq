using DQQ.Components.Skills;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;

namespace DQQ.Components.Parameters
{
	public class ComponentTickParameter
	{
		public static ComponentTickParameter New(ITarget? from, IEnumerable<ITarget>? friendlyTargets, IEnumerable<ITarget>? enemyTargets, IMap? map)
		{
			return new ComponentTickParameter
			{
				From = from,
				FriendlyTargets = friendlyTargets,
				EnemyTargets = enemyTargets,
				Map = map
			};
		}
		public static ComponentTickParameter New(ComponentTickParameter? parameter, ITarget? secondaryTarget)
		{
			return new ComponentTickParameter
			{
				From = parameter?.From,
				FriendlyTargets = parameter?.FriendlyTargets,
				EnemyTargets = parameter?.EnemyTargets,
				Map = parameter?.Map,
				SecondaryTarget = secondaryTarget
			};
		}
		public ITarget? From { get; set; }
		public ITarget? SecondaryTarget { get; set; }
		public IEnumerable<ITarget>? FriendlyTargets { get; set; }
		public IEnumerable<ITarget>? EnemyTargets { get; set; }
		public IMap? Map { get; set; }
		public ITarget? SelectedTarget => SecondaryTarget ?? From?.Target;

		public virtual void SetSupportSkill(
			SkillComponent? trigger,
			IEnumerable<SkillComponent>? supportSkills, IEnumerable<SkillComponent>? triggerSkills, IEnumerable<SkillComponent>? subAttackSkills)
		{
			Trigger = trigger;
			SupportSkills = supportSkills?.Where(b => b.SkillProfile?.BindingType == EnumSkillBindingType.Support).Distinct();
			TriggerSkills = triggerSkills?.Where(b => b.SkillProfile?.BindingType == EnumSkillBindingType.Trigger).Distinct();
			SubAttackSkills = subAttackSkills?.Where(b => b.SkillProfile?.BindingType == EnumSkillBindingType.Active).Distinct();
		}
		public IEnumerable<SkillComponent>? SupportSkills { get; protected set; }
		public IEnumerable<SkillComponent>? TriggerSkills { get; protected set; }
		public IEnumerable<SkillComponent>? SubAttackSkills { get; protected set; }

		public SkillComponent? Trigger { get; protected set; }
	}

}
