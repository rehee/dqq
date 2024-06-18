using DQQ.Commons;
using DQQ.Components.Parameters;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Drops;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles;
using DQQ.Profiles.Mobs;
using DQQ.TickLogs;
using DQQ.XPs;
using System.Numerics;
using System.Text.Json.Serialization;

namespace DQQ.Components.Stages.Actors.Mobs
{
	public class Monster : Actor, IDropper, IXP
	{
		public EnumMob MobNumber { get; set; }
		[JsonIgnore]
		public override DQQProfile? Profile => DQQPool.TryGet<MobProfile, EnumMob>(MobNumber);
		public EnumMobRarity Rarity { get; set; } = EnumMobRarity.Normal;
		public decimal DropRate { get; set; }
		public decimal RarityRaRate { get; set; }
		public override EnumTargetLevel PowerLevel
		{
			get
			{
				if (Profile is MobProfile mp)
				{
					if (mp.IsBoss)
					{
						return EnumTargetLevel.Guardian;
					}
				}
				switch (Rarity)
				{
					case EnumMobRarity.Normal:
						return EnumTargetLevel.Normal;
					case EnumMobRarity.Magic:
						return EnumTargetLevel.Magic;
					case EnumMobRarity.Elite:
						return EnumTargetLevel.Elite;
					case EnumMobRarity.Champion:
						return EnumTargetLevel.Champion;
				}
				return base.PowerLevel;
			}
		}
		public Int64 XP { get; set; }
		public static Monster Create(MobProfile profile, int level, EnumMobRarity? rarity = null)
		{
			var mob = new Monster();

			mob.MobNumber = profile.ProfileNumber;
			var namePrefix = "";
			var dropTimes = 1;
			if (profile.IsBoss != true)
			{
				switch (rarity)
				{
					case EnumMobRarity.Magic:
						namePrefix = "";
						dropTimes = 2;
						break;
					case EnumMobRarity.Elite:
						namePrefix = "";
						dropTimes = 4;
						break;
					case EnumMobRarity.Champion:
						dropTimes = 8;
						namePrefix = "";
						break;
				}
			}


			mob.MobNumber = profile.ProfileNumber;
			mob.Alive = true;
			mob.Targetable = true;
			mob.Rarity = rarity ?? EnumMobRarity.Normal;
			mob.DisplayName = $"{profile.Name}";

			profile.SetMonsterCombatPanel(level, mob.CombatPanel.StaticPanel, mob.Rarity);

			mob.XP = XPHelper.GetMobKilledExp(level);
			mob.Level = level;
			mob.CurrentHP = mob.CombatPanel.StaticPanel.MaximunLife ?? 0;
			mob.DropRate = profile.DropRate * dropTimes;
			mob.RarityRaRate = profile.RarityRaRate * dropTimes;

			if (profile?.Skills?.Any() == true)
			{
				var list = new List<SkillComponent>();
				foreach (var skill in profile.Skills)
				{
					list.Add(SkillComponent.New(skill, EnumSkillSlot.MainSlot));
				}
				mob.Skills = [.. list];
			}
			mob.DisplayId = Guid.NewGuid();
			return mob;
		}

		protected override void SelfAfterTakeDamage(ComponentTickParameter parameter)
		{
			base.SelfAfterTakeDamage(parameter);
			if (parameter.Damage?.DamageTakenSuccess != true)
			{
				return;
			}
			if (parameter.Damage?.IsKilled != true)
			{
				return;
			}
			parameter.Damage.Drops = DropHelper.Drop(this, parameter.Map);
			parameter.Damage.XP = this.XP;
		}
	}
}
