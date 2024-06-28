using DQQ.Attributes;
using DQQ.Commons;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DQQ.Profiles.Skills.Attacks
{
	[Pooled]
	public class SwingAttack : GeneralSkill
	{
		public override EnumChapter? UnLockedChapter => EnumChapter.C_1_7;
		public override EnumSkillCategory Category => EnumSkillCategory.Core;
		public override EnumDamageHand DamageHand => EnumDamageHand.BothHand;
		public override decimal CastTime => 0m;
		public override decimal CoolDown => 6m;
		public override decimal DamageRate => 1m;
		public override string? Discription => "迅捷的一击. 同时使用主副手的武器对敌人发动攻击. 造成 100% 主手的伤害 与 副手150%的伤害";
		public override string? Name => "迅猛攻击";
		public override bool CastWithWeaponSpeed => true;

		public override EnumSkillNumber ProfileNumber => EnumSkillNumber.SwingAttack;


		public override long? GetBaseDamage(ComponentTickParameter? parameter)
		{
			if (parameter?.From?.CombatPanel.IsDuelWield == true)
			{
				return parameter?.From?.CombatPanel.DynamicPanel.MainHand + (long)((parameter?.From?.CombatPanel.DynamicPanel.OffHand??0) *1.5);
			}
			return parameter?.From?.CombatPanel.DynamicPanel.MainHand;
		}

		


	}
}