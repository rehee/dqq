using DQQ.Commons.DTOs;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;

namespace DQQ.UnitTest.TestBase.Stages.Maps
{
	public class MapTests : BaseTest
	{

		[Test]
		public async Task MapPlayTest_Happy_Path()
		{
			var map = new Map();
			map.Playable = true;
			await map.Play();

			Assert.That(map.Playable, Is.False);
			Assert.That(map.Playing, Is.False);
			Assert.That(map.PlayTime, Is.Not.Null);
		}

		[Test]
		public async Task Map_Init_Happy_Path()
		{
			var map = new Map();
			var creator = new Actor();

			creator.Alive = true;
			creator.DisplayName = "player 1";
			creator.CurrentHP = 10000;
			creator.CombatPanel.DynamicPanel.MaximunLife = 10000;
			creator.BasicDamage = 100000;
			creator.CombatPanel.DynamicPanel.Damage = 100000;
			creator.CombatPanel.DynamicPanel.AttackPerSecond = 1;
			creator.Skills = new SkillComponent[]
			{
				SkillComponent.New(EnumSkillNumber.NormalAttack, EnumSkillSlot.MainSlot)
			};
			var dto = new CombatRequestDTO
			{
				Creator = creator,
				ActorId = creator.DisplayId,
				SubMapLevel = 0,
				RandomGuid = Guid.NewGuid(),
				MapLevel = 0,
				MapNumber = EnumMapNumber.Map_1_1
			};
			await map.Initialize(dto);
			await map.Play();
			var log = map.Logs;
			Assert.That(map.PlayMins, Is.LessThan(30));
			Assert.That(map.MobPool!.All(m => m.All(b => !b.Alive)), Is.True);
		}
	}
}
