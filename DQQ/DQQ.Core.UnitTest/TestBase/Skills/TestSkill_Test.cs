using DQQ.Components.Parameters;
using DQQ.Components.Stages.Actors;
using DQQ.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.UnitTest.TestBase.Skills
{
	public class TestSkill_Test : BaseTest
	{
		[TestCase(1, 1.2, 0)]
		[TestCase(2, 1.2, 1)]
		[TestCase(3, 1.2, 2)]
		[TestCase(4, 1.2, 3)]
		public async Task CastTimeCount(int totalTimeSecond, decimal castTime, int countExpected)
		{
			var skill = new TestSkill();
			skill.CastTime = castTime;
			var actor = new Actor();
			actor.CurrentHP = 1000;
			actor.Alive = true;
			actor.Target = actor;
			var parameter = ComponentTickParameter.New(123);
			parameter.From = actor;
			for (var i = 0; i < totalTimeSecond * DQQGeneral.TickPerSecond; i++)
			{

				await skill.OnTick(ComponentTickParameter.New(parameter));
			}
			Assert.That(skill.CastTimeCount, Is.EqualTo(countExpected));
		}
		[TestCase(1, 1.2, 1)]
		[TestCase(2, 1.2, 2)]
		[TestCase(3, 1.2, 3)]
		[TestCase(4, 1.2, 4)]
		public async Task CastTimeCount_CD(int totalTimeSecond, decimal cd, int countExpected)
		{
			var skill = new TestSkill();
			skill.Cooldown = cd;
			skill.CastTime = 0.001m;
			var actor = new Actor();
			actor.CurrentHP = 1000;
			actor.Alive = true;
			actor.Target = actor;
			var parameter = ComponentTickParameter.New(123);
			parameter.From = actor;
			for (var i = 0; i < totalTimeSecond * DQQGeneral.TickPerSecond; i++)
			{
				await skill.OnTick(parameter);
			}
			Assert.That(countExpected, Is.EqualTo(skill.CastTimeCount));
		}
	}
}
