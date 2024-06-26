﻿using DQQ.Components.Parameters;
using DQQ.Components.Skills;
using DQQ.Consts;
using DQQ.UnitTest.TestBase.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.UnitTest.TestBase.Stages.Actors
{
	public class TestActror_Test : BaseTest
	{
		[TestCase(true)]
		[TestCase(false)]
		public async Task TickCountWithAlive(bool alive)
		{
			var actor = new TestActror();
			actor.Alive = alive;
			await actor.OnTick(ComponentTickParameter.New(123));
			Assert.That(actor.TickCount > 0, Is.EqualTo(actor!.TickCount > 0));
		}
		[TestCase(true, 1, 1.2, 0, 0)]
		[TestCase(true, 2, 1.2, 0, 1)]
		[TestCase(true, 3, 1.2, 0, 2)]
		[TestCase(true, 1, 0, 1.2, 1)]
		[TestCase(true, 2, 0, 1.2, 2)]
		[TestCase(true, 3, 0, 1.2, 3)]
		[TestCase(false, 1, 1.2, 0, 0)]
		[TestCase(false, 2, 1.2, 0, 0)]
		[TestCase(false, 3, 1.2, 0, 0)]
		[TestCase(false, 1, 0, 1.2, 0)]
		[TestCase(false, 2, 0, 1.2, 0)]
		[TestCase(false, 3, 0, 1.2, 0)]
		public async Task ActorCastTest(bool alive, int time, decimal cast, decimal cd, int castCount)
		{
			var actor = new TestActror();
			actor.Alive = alive;

			var target = new TestActror();
			target.Alive = alive;
			target.CurrentHP = 1000;

			actor.Target = target;
			var skill = new TestSkill();
			skill.CastTime = cast;
			skill.Cooldown = cd;

			actor.Skills = new List<SkillComponent> { skill };
			var parameter = ComponentTickParameter.New(123);
			parameter.From = actor;
			parameter.SecondaryTarget = target;
			for (var i = 0; i < time * DQQGeneral.TickPerSecond; i++)
			{
				await actor.OnTick(parameter);
			}

			Assert.That(skill.CastTimeCount, Is.EqualTo(castCount));
		}
		[TestCase(1, 1.2, 0, 1000)]
		[TestCase(2, 1.2, 0, 900)]
		[TestCase(100, 1.2, 0, 0)]
		public async Task ActorCastAttackTest(int time, decimal cast, decimal cd, int hpRemain)
		{
			var actor = new TestActror();
			actor.Alive = true;
			var target = new TestActror();
			target.Alive = true;
			target.CurrentHP = 1000;
			actor.SelectTarget(target);
			var skill = new AttackTestSkill();
			skill.Slot = Enums.EnumSkillSlot.MainSlot;
			skill.CastTime = cast;
			skill.Cooldown = cd;
			actor.CombatPanel.StaticPanel.Damage = 100;
			actor.Skills = new SkillComponent[] { skill };
			var parameters = ComponentTickParameter.New(123);
			var parameter = ComponentTickParameter.New(parameters);
			parameter.From = actor;
			parameter.SecondaryTarget = target;
			for (var i = 0; i < time * DQQGeneral.TickPerSecond; i++)
			{
				await actor.OnTick(parameter);
			}

			Assert.That(target.CurrentHP, Is.LessThanOrEqualTo((hpRemain)));
		}
	}
}
