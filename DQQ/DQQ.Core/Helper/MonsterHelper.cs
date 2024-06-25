using DQQ.Combats;
using DQQ.Consts;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class MonsterHelper
	{

		public static double GetMobRarityRate(this EnumMobRarity enumMobRarity, bool? isBoss)
		{
			switch (enumMobRarity)
			{
				case EnumMobRarity.Magic:
					return 2;
				case EnumMobRarity.Elite:
					return 3;
				case EnumMobRarity.Champion:
					return 4;
			}
			return isBoss == true ? 5 : 1;
		}

		public static void SetMonsterCombatPanel(this MobProfile profile, int? level, ICombatProperty property, EnumMobRarity enumMobRarity = EnumMobRarity.Normal)
		{

			var rarityRate = enumMobRarity.GetMobRarityRate(profile?.IsBoss);
			var hp = MonsterBasicProfile.CalculateValue(level, MonsterBasicProfile.BaseHP * profile?.HPPercentage ?? 1);
			var damage = MonsterBasicProfile.CalculateValue(level, MonsterBasicProfile.BasicDPS * profile?.DamagePercentage ?? 1);
			property.MaximunLife = Convert.ToInt64(hp);
			property.Damage = Convert.ToInt64(damage);
			property.AttackPerSecond = MonsterBasicProfile.AttackPerSecond * (1 + (profile?.AttackSpeedModify ?? 0));
			property.AttackRating = (level ?? 1) * 90;
		}
	}
}
