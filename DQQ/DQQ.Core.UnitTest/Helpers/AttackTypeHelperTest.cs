using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Helper;
using DQQ.UnitTest;
using DQQ.UnitTest.TestBase.Stages.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Core.UnitTest.Helpers
{
	public class AttackTypeHelperTest : BaseTest
	{

		protected Dictionary<Guid, ITarget> TargetMap { get; set; } = [];
		protected ITarget[] Targets => TargetMap.Select(t => t.Value).ToArray();
		protected ComponentTickParameter parameter { get; set; } = ComponentTickParameter.New(111);

		[SetUp]
		public override async Task Setup()
		{
			await base.Setup();
			var target1 = new TestActror();
			var target2 = new TestActror();
			var target3 = new TestActror();
			var target4 = new TestActror();
			var target5 = new TestActror();
			var target6 = new TestActror();
			var target7 = new TestActror();
			var target8 = new TestActror();
			var target9 = new TestActror();
			var target10 = new TestActror();
			TargetMap = [];
			TargetMap.Add(target1.DisplayId!.Value, target1);
			TargetMap.Add(target2.DisplayId!.Value, target2);
			TargetMap.Add(target3.DisplayId!.Value, target3);
			TargetMap.Add(target4.DisplayId!.Value, target4);
			TargetMap.Add(target5.DisplayId!.Value, target5);
			TargetMap.Add(target6.DisplayId!.Value, target6);
			TargetMap.Add(target7.DisplayId!.Value, target7);
			TargetMap.Add(target8.DisplayId!.Value, target8);
			TargetMap.Add(target9.DisplayId!.Value, target9);
			TargetMap.Add(target10.DisplayId!.Value, target10);
		}

		[Test]
		public void ChainTargetTest()
		{
			var chainTarget = AttackTypeHelper.GetChainTargets(parameter, Targets[0],
							Targets, 5);
			Assert.That(chainTarget.Count(), Is.EqualTo(5));
			chainTarget = AttackTypeHelper.GetChainTargets(parameter, Targets[0],
				Targets, 6);
			Assert.That(chainTarget.Count(), Is.EqualTo(6));
			chainTarget = AttackTypeHelper.GetChainTargets(parameter, Targets[0],
				Targets, 9);
			Assert.That(chainTarget.Count(), Is.EqualTo(9));
			chainTarget = AttackTypeHelper.GetChainTargets(parameter, Targets[0],
				Targets, 10);
			Assert.That(chainTarget.Count(), Is.EqualTo(10));
			Assert.That(chainTarget.FirstOrDefault()?.DisplayId, Is.EqualTo(Targets[0].DisplayId));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[0].DisplayId), Is.LessThanOrEqualTo(5));
			chainTarget = AttackTypeHelper.GetChainTargets(parameter, Targets[0],
				[Targets[0], Targets[1]], 10);
			Assert.That(chainTarget.Count(), Is.EqualTo(10));
			Assert.That(chainTarget.FirstOrDefault()?.DisplayId, Is.EqualTo(Targets[0].DisplayId));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[0].DisplayId), Is.LessThanOrEqualTo(5));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[0].DisplayId), Is.LessThanOrEqualTo(5));
		}
		[Test]
		public void AreaTargetTest()
		{
			var chainTarget = AttackTypeHelper.GetAreaTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.Self);
			Assert.That(chainTarget.Count(), Is.EqualTo(1));

			chainTarget = AttackTypeHelper.GetAreaTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.Single);
			Assert.That(chainTarget.Count(), Is.EqualTo(1));

			chainTarget = AttackTypeHelper.GetAreaTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.TargetWithRadius1);
			Assert.That(chainTarget.Count(), Is.EqualTo(3));

			chainTarget = AttackTypeHelper.GetAreaTargets(parameter, Targets[4],
							Targets, Enums.EnumAreaLevel.TargetWithRadius2);
			Assert.That(chainTarget.Count(), Is.EqualTo(5));
			chainTarget = AttackTypeHelper.GetAreaTargets(parameter, Targets[4],
							Targets, Enums.EnumAreaLevel.TargetWithRadiusMax);
			Assert.That(chainTarget.Count(), Is.EqualTo(10));
			chainTarget = AttackTypeHelper.GetAreaTargets(parameter, Targets[0],
							Targets, Enums.EnumAreaLevel.TargetWithRadiusMax);
			Assert.That(chainTarget.Count(), Is.EqualTo(10));
		}
		[Test]
		public void MultiAttackTargetTest()
		{
			var chainTarget = AttackTypeHelper.GetMultiAttackTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.Self, 5);
			Assert.That(chainTarget.Count(), Is.EqualTo(5));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(5));


			chainTarget = AttackTypeHelper.GetMultiAttackTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.Single, 5);
			Assert.That(chainTarget.Count(), Is.EqualTo(5));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(5));

			chainTarget = AttackTypeHelper.GetMultiAttackTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.TargetWithRadius1, 5);
			Assert.That(chainTarget.Count(), Is.EqualTo(5));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.LessThanOrEqualTo(5));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.GreaterThanOrEqualTo(1));
		}
		[Test]
		public void GetPiercingTargetsTest()
		{
			var chainTarget = AttackTypeHelper.GetPiercingTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.Self);
			Assert.That(chainTarget.Count(), Is.EqualTo(1));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(1));

			chainTarget = AttackTypeHelper.GetPiercingTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.Single);
			Assert.That(chainTarget.Count(), Is.EqualTo(1));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(1));

			chainTarget = AttackTypeHelper.GetPiercingTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.TargetWithRadius1);
			Assert.That(chainTarget.Count(), Is.EqualTo(3));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(1));

			chainTarget = AttackTypeHelper.GetPiercingTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.TargetWithRadius2);
			Assert.That(chainTarget.Count(), Is.EqualTo(5));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(1));

			chainTarget = AttackTypeHelper.GetPiercingTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.TargetWithRadiusMax);
			Assert.That(chainTarget.Count(), Is.EqualTo(5));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(1));
			chainTarget = AttackTypeHelper.GetPiercingTargets(parameter, Targets[0],
							Targets, Enums.EnumAreaLevel.TargetWithRadiusMax);
			Assert.That(chainTarget.Count(), Is.EqualTo(10));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(1));
		}
		[Test]
		public void GetGetCleaveTargetsTest()
		{
			var chainTarget = AttackTypeHelper.GetCleaveTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.Self);
			Assert.That(chainTarget.Count(), Is.EqualTo(1));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(1));

			chainTarget = AttackTypeHelper.GetCleaveTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.Single);
			Assert.That(chainTarget.Count(), Is.EqualTo(1));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(1));

			chainTarget = AttackTypeHelper.GetCleaveTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.TargetWithRadius1);
			Assert.That(chainTarget.Count(), Is.EqualTo(2));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(1));

			chainTarget = AttackTypeHelper.GetCleaveTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.TargetWithRadius2);
			Assert.That(chainTarget.Count(), Is.EqualTo(3));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(1));

			chainTarget = AttackTypeHelper.GetCleaveTargets(parameter, Targets[5],
							Targets, Enums.EnumAreaLevel.TargetWithRadiusMax);
			Assert.That(chainTarget.Count(), Is.EqualTo(6));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[5].DisplayId), Is.EqualTo(1));
			chainTarget = AttackTypeHelper.GetCleaveTargets(parameter, Targets[0],
							Targets, Enums.EnumAreaLevel.TargetWithRadiusMax);
			Assert.That(chainTarget.Count(), Is.EqualTo(6));
			Assert.That(chainTarget.Count(b => b.DisplayId == Targets[0].DisplayId), Is.EqualTo(1));
		}
	}
}
