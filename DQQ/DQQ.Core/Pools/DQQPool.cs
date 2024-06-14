using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles;
using DQQ.Profiles.Affixes;
using DQQ.Profiles.Durations;
using DQQ.Profiles.Items;
using DQQ.Profiles.Mobs;
using DQQ.Profiles.Skills;
using ReheeCmf.Helpers;
using ReheeCmf.Utility.CmfRegisters;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Pools
{
	public static partial class DQQPool
	{
		public static void InitPool()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			Assembly[] assemblies = currentDomain.GetAssemblies();
			var pooledType = typeof(PooledAttribute);
			foreach (var assembly in assemblies)
			{
				foreach (var type in assembly.GetTypes())
				{
					if (type.CustomAttributes.Any(b => b.AttributeType == pooledType))
					{
						var instance = Activator.CreateInstance(type);

						if (instance is SkillProfile sp)
						{
							DQQPool.SkillPool.TryAdd(sp.SkillNumber, sp);
						}
						if (instance is AbMobProfile mp)
						{
							DQQPool.MobPool.TryAdd(mp.ProfileNumber, mp);
						}
						if (instance is ItemProfile ip)
						{
							DQQPool.ItemPool.TryAdd(ip.ProfileNumber, ip);
						}
						if (instance is DurationProfile dp)
						{
							DQQPool.DurationPool.TryAdd(dp.ProfileNumber, dp);
						}
						if (instance is AffixeProfile ap)
						{
							DQQPool.AffixePool.TryAdd(ap.ProfileNumber, ap);
						}
					}

				}
			}
		}

		public static T? TryGet<T, K>(K key) where T : DQQProfile
		{
			if (key == null)
			{
				return default(T);
			}
			var type = typeof(T);

			if (key is EnumSkillNumber skillNumber)
			{
				SkillPool.TryGetValue(skillNumber, out var skill);
				return skill as T;
			}
			if (key is EnumMob mobNumber)
			{
				MobPool.TryGetValue(mobNumber, out var mob);
				return mob as T;
			}
			if (key is EnumItem itemNumber)
			{
				ItemPool.TryGetValue(itemNumber, out var item);
				return item as T;
			}
			if (key is EnumDurationNumber durationNumber)
			{
				DurationPool.TryGetValue(durationNumber, out var duration);
				return duration as T;
			}
			if (key is EnumAffixeNumber affixeNumber)
			{
				AffixePool.TryGetValue(affixeNumber, out var affixe);
				return affixe as T;
			}
			return default(T);
		}
	}
}
