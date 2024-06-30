using DQQ.Attributes;
using DQQ.Enums;
using DQQ.Profiles;
using DQQ.Profiles.Affixes;
using DQQ.Profiles.Chapters;
using DQQ.Profiles.Durations;
using DQQ.Profiles.Items;
using DQQ.Profiles.Maps;
using DQQ.Profiles.Mobs;
using DQQ.Profiles.PresetStrategies;
using DQQ.Profiles.Skills;
using DQQ.Profiles.ZProgress;
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
		public static void InitPool(params Action<Type>[]? additionalMapping)
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
						if (instance is MobProfile mp)
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
						if (instance is ProgressProfile pgp)
						{
							DQQPool.ProgressPool.TryAdd(pgp.ProfileNumber, pgp);
						}
						if (instance is ChapterProfile cpf)
						{
							DQQPool.ChapterPools.TryAdd(cpf.ProfileNumber, cpf);
						}
						if (instance is MapProfile mpf)
						{
							DQQPool.MapPools.TryAdd(mpf.ProfileNumber, mpf);
						}
						if (instance is PresetStrategyProfile psp)
						{
							DQQPool.PresetStrategyPool.TryAdd(psp.ProfileNumber, psp);
						}
					}


					#region additional mapping
					if (additionalMapping?.Any() == true)
					{
						foreach (var additional in additionalMapping)
						{
							additional(type);
						}
					}
					#endregion
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
			if (key is EnumProgress progressNumber)
			{
				ProgressPool.TryGetValue(progressNumber, out var progress);
				return progress as T;
			}
			if (key is EnumChapter chapterNumber)
			{
				ChapterPools.TryGetValue(chapterNumber, out var cpt);
				return cpt as T;
			}
			if (key is EnumMapNumber mapNumber)
			{
				MapPools.TryGetValue(mapNumber, out var mapc);
				return mapc as T;
			}
			if (key is EnumPresetSkillStrategy psskey)
			{
				PresetStrategyPool.TryGetValue(psskey, out var pssvalue);
				return pssvalue as T;
			}
			return default(T);
		}
	}
}