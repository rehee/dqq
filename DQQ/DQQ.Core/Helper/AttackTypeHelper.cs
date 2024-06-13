using DQQ.Combats;
using DQQ.Components.Parameters;
using DQQ.Components.Stages;
using DQQ.Consts;
using DQQ.Enums;

namespace DQQ.Helper
{
	public static class AttackTypeHelper
	{
		public static void AttackTypeModify(this IWIthAttackTypeAndArea input, EnumAttackType attackType)
		{
			input.AttackTypes = attackType;
		}
		public static ITarget[] SkillEffectTarget(this ComponentTickParameter parameter)
		{
			switch (parameter.AttackTypes)
			{
				case EnumAttackType.Chain:
					return GetChainTargets(parameter.To,
						parameter?.EnemyTargets?.Where(b => b.Alive).ToArray() ?? [], Math.Min(DQQGeneral.MaxDamageCount, parameter?.ExtraAttackNumber ?? 0))
						.ToArray();
				case EnumAttackType.Cleave:
					return GetCleaveTargets(parameter.To,
						parameter?.EnemyTargets?.Where(b => b.Alive).ToArray() ?? [], parameter?.AreaLevel ?? EnumAreaLevel.Single)
						.ToArray();
				case EnumAttackType.Piercing:
					return GetPiercingTargets(parameter.To,
						parameter?.EnemyTargets?.Where(b => b.Alive).ToArray() ?? [], parameter?.AreaLevel ?? EnumAreaLevel.Single)
						.ToArray();
				case EnumAttackType.MultiAttack:
					return GetMultiAttackTargets(parameter.To,
						parameter?.EnemyTargets?.Where(b => b.Alive).ToArray() ?? [], parameter?.AreaLevel ?? EnumAreaLevel.Single, Math.Min(DQQGeneral.MaxDamageCount, parameter?.ExtraAttackNumber ?? 0))
						.ToArray();
				case EnumAttackType.Area:
					return GetAreaTargets(parameter.To,
						parameter?.EnemyTargets?.Where(b => b.Alive).ToArray() ?? [], parameter?.AreaLevel ?? EnumAreaLevel.Single)
						.ToArray();

			}
			return [parameter.To!];
		}
		public static IEnumerable<ITarget> GetChainTargets(ITarget? target, ITarget[] allTargets, int remain = 0, IEnumerable<ITarget>? targetPool = null)
		{
			if (target == null)
			{
				return [];
			}
			var avaliableTargets = allTargets.Where(b => b.DisplayId != target.DisplayId);
			if (avaliableTargets?.Any() != true || remain <= 1)
			{
				return targetPool ?? [target];
			}
			var randomon = RandomHelper.NewRandom();
			var randomTarget = avaliableTargets.Select(b => (randomon.Next(), b)).OrderBy(b => b.Item1).Select(b => b.b).FirstOrDefault();
			return GetChainTargets(randomTarget, allTargets, remain - 1, (targetPool ?? [target!]).Concat([randomTarget!]).ToArray());
		}
		public static IEnumerable<ITarget> GetCleaveTargets(ITarget? target, ITarget[] allTargets, EnumAreaLevel aoeLevel)
		{
			if (target == null)
			{
				return [];
			}
			var selfList = new ITarget[] { target! };
			if (aoeLevel == EnumAreaLevel.Self || aoeLevel == EnumAreaLevel.Single || allTargets.Length <= 1)
			{
				return selfList;
			}
			var aoeNumber = 0;
			switch (aoeLevel)
			{
				case EnumAreaLevel.TargetWithRadius1:
					aoeNumber = 1;
					break;
				case EnumAreaLevel.TargetWithRadius2:
					aoeNumber = 2;
					break;
				case EnumAreaLevel.TargetWithRadiusMax:
					aoeNumber = allTargets.Length / 2;
					break;
			}
			var randomon = RandomHelper.NewRandom();


			return selfList.Concat(allTargets.Where(b => b.DisplayId != target.DisplayId).GetNumberOfRandom(aoeNumber)).ToArray();
		}
		public static IEnumerable<ITarget> GetPiercingTargets(ITarget? target, ITarget[] allTargets, EnumAreaLevel aoeLevel)
		{
			if (target == null)
			{
				return [];
			}
			var selfList = new ITarget[] { target! };

			if (aoeLevel == EnumAreaLevel.Self || aoeLevel == EnumAreaLevel.Single || allTargets.Length <= 1)
			{
				return selfList;
			}
			var aoeNumber = 1;
			switch (aoeLevel)
			{
				case EnumAreaLevel.TargetWithRadius1:
					aoeNumber = 2;
					break;
				case EnumAreaLevel.TargetWithRadius2:
					aoeNumber = 4;
					break;
				case EnumAreaLevel.TargetWithRadiusMax:
					aoeNumber = allTargets.Length;
					break;
			}
			var currentIndex = Array.IndexOf(allTargets.Select(b => b.DisplayId).ToArray(), target.DisplayId);
			return allTargets.Where((b, i) => i >= currentIndex).Take(aoeNumber + 1).ToArray();

		}
		public static IEnumerable<ITarget> GetMultiAttackTargets(ITarget? target, ITarget[] allTargets, EnumAreaLevel aoeLevel, int attackTimes = 1)
		{
			var targetPool = GetAreaTargets(target, allTargets, aoeLevel).ToArray();
			if (targetPool.Length <= 0)
			{
				return targetPool;
			}
			var result = new List<ITarget>() { targetPool[0] };
			for (var i = 1; i < attackTimes; i++)
			{
				result.Add(targetPool.GetRamdom());
			}
			return result.ToArray();
		}
		public static IEnumerable<ITarget> GetAreaTargets(ITarget? target, ITarget[] allTargets, EnumAreaLevel aoeLevel)
		{
			ITarget[] targetPool = [];
			if (target == null)
			{
				return targetPool;
			}
			targetPool = [target!];
			if (aoeLevel == EnumAreaLevel.Self || aoeLevel == EnumAreaLevel.Single || allTargets.Length <= 1)
			{
				return targetPool;
			}
			var index = Array.IndexOf(allTargets.Select(b => b.DisplayId).ToArray(), target.DisplayId);
			int targetRange = 0;
			switch (aoeLevel)
			{
				case EnumAreaLevel.TargetWithRadius1:
					targetRange = 1;
					break;
				case EnumAreaLevel.TargetWithRadius2:
					targetRange = 2;
					break;
				case EnumAreaLevel.TargetWithRadiusMax:
					targetRange = allTargets.Length;
					break;
			}
			var minRange = index - targetRange;
			var maxRange = index + targetRange;
			var avaliable = allTargets.Where((b, i) => i != index && i >= minRange && i <= maxRange);
			if (!avaliable.Any())
			{
				return targetPool;
			}
			return targetPool.Concat(avaliable).ToArray();
		}
	}
}