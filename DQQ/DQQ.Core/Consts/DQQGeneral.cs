using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Helper;
using System.Numerics;

namespace DQQ.Consts
{
	public static class DQQGeneral
	{
		public const int TickPerSecond = 30;
		public const int MapMinute = 15;
		public const int DelayTime = 10;
		public const decimal MinCooldown = 0.15m;
		public const int MaxDamageCount = 5;
		public const int MinimunMapLevel = 50;
		public const int LevelPerTier = 3;
		public const int LevelPerSubTier = 1;
		public const decimal MobLevelIncreased = 0.05m;
		public const int DurationIntervalTick = 15;

		public const decimal RareRate = 0.10m;
		public const decimal MagicRate = 0.25m;

		public const decimal DuelwieldAttackSpeed = 1.10m;
		public static EnumTargetLevel[] GuardianLevel = { EnumTargetLevel.Guardian };
		public static EnumTargetLevel[] EliteLevel = { EnumTargetLevel.Magic, EnumTargetLevel.Elite, EnumTargetLevel.Champion };
		public static EnumTargetLevel[] TrashLevel = { EnumTargetLevel.NotSpecified, EnumTargetLevel.Normal };
		public static EnumDamageType[] ElementDamage = { EnumDamageType.Fire, EnumDamageType.Cold, EnumDamageType.Lightning };


		public const decimal MaxHitChance = 0.95m;
		public const decimal MinHitChance = 0.05m;
		public const decimal SameLevelHitChance = 0.90m;
		public const decimal HitChanceModifyByLevel = 0.05m;
		public const decimal AttributeImpact = 0.001m;

		public const decimal BlockRecoveryTime = 0.35m;
		public static Int64 MobStatusCalculate(int mobLevel, Int64 basicValue, EnumMobRarity? rarity = EnumMobRarity.Normal, bool isBoss = false)
		{
			var multiple = 1;
			if (isBoss != true)
			{
				switch (rarity)
				{
					case EnumMobRarity.Magic:
						multiple = 2;
						break;
					case EnumMobRarity.Elite:
						multiple = 3;
						break;
					case EnumMobRarity.Champion:
						multiple = 5;
						break;
				}
			}
			else
			{
				multiple = 2;
			}
			if (mobLevel <= 1)
			{
				return basicValue;
			}
			var value = (mobLevel - 1) * MobLevelIncreased;
			return (basicValue + basicValue.Percentage(value)) * multiple;
		}
		public static int TotalTick(this IMap? map) => 60 * ((map?.limitMinute ?? MapMinute) * TickPerSecond);

		public const int WeaponDamageRange = 95;
		public const int MaxLevel = 70;
		public const double InitialWeaponDPS = 6;
		public const double MaxWeaponDPS = 200.0;

		public const double ArmorReduceConst = 5.67;
		public static double ArmorDamageReduction(int attackLevel, long armor)
		{
			return armor / (armor + ArmorReduceConst * attackLevel);
		}

		public const int BasicLevelUpExp = 150;
		public const double LevelUpExpIncreased = 1.25;

		public const int BasicMobExp = 8;
		public const double LevelMobExpIncreased = 0.05;

	}
}
