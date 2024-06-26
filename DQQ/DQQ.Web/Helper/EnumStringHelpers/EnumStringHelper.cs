﻿using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Items;
using DQQ.Profiles.Items.Equipments.Armors.BodyArmor;
using DQQ.Profiles.Items.Equipments.OneHandWeapons;
using DQQ.Profiles.Items.Equipments.OneHandWeapons.Claws;
using DQQ.Profiles.Items.Equipments.TwoHandWeapons;
using System;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace DQQ.Helper
{
	public static partial class EnumStringHelper
	{
		public static string? GetEnumString<T>(this T? input)
		{
			if (input is EnumStrategyCondition esc)
			{
				return GetString(esc);
			}
			if (input is EnumTarget et)
			{
				return GetString(et);
			}
			if (input is EnumTargetPriority etp)
			{
				return GetString(etp);
			}
			if (input is EnumPropertyCompare epc)
			{
				return GetString(epc);
			}
			if (input is EnumCompare ec)
			{
				return GetString(ec);
			}
			if (input is EnumStrategyParty esp)
			{
				return GetString(esp);
			}
			if (input is EnumStrategyWave esw)
			{
				return GetString(esw);
			}
			if (input is EnumRarity er)
			{
				return GetString(er);
			}
			if (input is EnumItemType eit)
			{
				return GetString(eit);
			}
			if (input is EnumEquipSlot ees)
			{
				return GetString(ees);
			}
			if (input is EnumEquipType eet)
			{
				return GetString(eet);
			}
			if (input is EnumItem ei)
			{
				return GetString(ei);
			}
			if (input is EnumSkillSlot ess)
			{
				return GetString(ess);
			}
			if (input is EnumSkillCategory escc)
			{
				return GetString(escc);
			}
			if (input is EnumSkillBindingType esbt)
			{
				return GetString(esbt);
			}
			if (input is EnumAttackType eact)
			{
				return GetString(eact);
			}
			if (input is EnumSkillAndStrategy esas)
			{
				return GetString(esas);
			}
			if (input is EnumCombatPlayType ecpt)
			{
				return GetString(ecpt);
			}
			if (input is EnumInventotyType eity)
			{
				return GetString(eity);
			}
			if (input is EnumPresetSkillStrategy epss)
			{
				return GetString(epss);
			}
			return $"{input}";
		}

		public static string? GetString(this EnumStrategyCondition? input)
		{
			switch (input)
			{
				case EnumStrategyCondition.Target:
					return "单一目标";
				case EnumStrategyCondition.Enemies:
					return "敌方目标";
				case EnumStrategyCondition.Players:
					return "友方目标";
				case EnumStrategyCondition.Combat:
					return "当前战斗";
				case EnumStrategyCondition.Wave:
					return "当前波次";
				default:
					return "无策略";
			}
		}
		public static string? GetString(this EnumTarget? input)
		{
			switch (input)
			{
				case EnumTarget.Target:
					return "敌人";
				case EnumTarget.Self:
					return "自身";
				case EnumTarget.Friendly:
					return "友方";
				default:
					return "无策略";
			}
		}
		public static string? GetString(this EnumTargetPriority? input)
		{
			switch (input)
			{
				case EnumTargetPriority.AnyTarget:
					return "任意目标";
				case EnumTargetPriority.Strongest:
					return "最强目标";
				case EnumTargetPriority.Weakest:
					return "最弱目标";
				case EnumTargetPriority.Front:
					return "队列前方";
				case EnumTargetPriority.Back:
					return "队列后方";
				default:
					return "无策略";
			}
		}
		public static string? GetString(this EnumPropertyCompare? input)
		{
			switch (input)
			{
				case EnumPropertyCompare.HealthPercentage:
					return "生命百分比";
				case EnumPropertyCompare.HealthAmount:
					return "生命绝对值";
				default:
					return "无策略";
			}
		}
		public static string? GetString(this EnumCompare? input)
		{
			switch (input)
			{
				case EnumCompare.MoreThan: return ">";
				case EnumCompare.MoreOrEqual: return ">=";
				case EnumCompare.LessThan: return "<";
				case EnumCompare.LessOrEqual: return "<=";
				case EnumCompare.Equal: return "==";
				case EnumCompare.NotEqual: return "!=";
				default:
					return "无";
			}
		}
		public static string? GetString(this EnumStrategyParty? input)
		{
			switch (input)
			{
				case EnumStrategyParty.AliveNumber: return "存活数量";
				case EnumStrategyParty.Contain: return "包含条件";

				default:
					return "无策略";
			}
		}
		public static string? GetString(this EnumStrategyWave? input)
		{
			switch (input)
			{
				case EnumStrategyWave.OnlyBeginning: return "仅在开始时";
				case EnumStrategyWave.Period: return "周期释放";
				case EnumStrategyWave.BossFight: return "仅首领战";
				case EnumStrategyWave.EliteFight: return "仅精英战";
				case EnumStrategyWave.TrashMob: return "仅小怪战";

				default:
					return "无策略";
			}
		}
		public static string? GetString(this EnumRarity? input)
		{
			switch (input)
			{
				case EnumRarity.Normal: return "普通";
				case EnumRarity.Magic: return "魔法";
				case EnumRarity.Rare: return "稀有";
				default:
					return "";
			}
		}
		public static string? GetString(this EnumItemType? input)
		{
			switch (input)
			{
				case EnumItemType.Claw: return "爪";

				case EnumItemType.Dagger: return "匕首";
				case EnumItemType.Wand: return "魔杖";
				case EnumItemType.Sword: return "剑";
				case EnumItemType.Axe: return "斧";
				case EnumItemType.Mace: return "锤";
				case EnumItemType.Sceptre: return "短杖";
				case EnumItemType.Bows: return "弓";
				case EnumItemType.Stave: return "长杖";
				case EnumItemType.Spear: return "矛";

				case EnumItemType.Quiver: return "箭袋";
				case EnumItemType.Shield: return "盾牌";
				case EnumItemType.Glove: return "护手";
				case EnumItemType.Boots: return "靴子";
				case EnumItemType.BodyArmor: return "盔甲";
				case EnumItemType.Helmet: return "头盔";
				case EnumItemType.Amulet: return "护符";
				case EnumItemType.Ring: return "戒指";
				case EnumItemType.Belt: return "腰带";
				case EnumItemType.Currency: return "通货";
			}
			return "";
		}
		public static string? GetString(this EnumEquipType? input)
		{
			switch (input)
			{
				case EnumEquipType.OneHandWeapon: return "单手武器";
				case EnumEquipType.TwoHandWeapon: return "双手武器";
				case EnumEquipType.MainHandWeapon: return "主手武器";
				case EnumEquipType.Offhand: return "副手";
				case EnumEquipType.Helmet: return "头盔";
				case EnumEquipType.BodyArmor: return "盔甲";
				case EnumEquipType.Glove: return "护手";
				case EnumEquipType.Boots: return "靴子";
				case EnumEquipType.Belt: return "腰带";
				case EnumEquipType.Amulet: return "护符";
				case EnumEquipType.Ring: return "戒指";
			}
			return "";
		}
		public static string? GetString(this EnumItem? item)
		{
			if (item == null) return "";
			return DQQPool.TryGet<ItemProfile, EnumItem>(item.Value)?.Name;
		}
		public static string? GetString(this EnumEquipSlot? input)
		{
			switch (input)
			{
				case EnumEquipSlot.Head: return "头盔";
				case EnumEquipSlot.Body: return "盔甲";
				case EnumEquipSlot.Gloves: return "护手";
				case EnumEquipSlot.Boots: return "靴子";
				case EnumEquipSlot.MainHand: return "主手";
				case EnumEquipSlot.OffHand: return "副手";
				case EnumEquipSlot.Amulet: return "护符";
				case EnumEquipSlot.Belt: return "腰带";
				case EnumEquipSlot.LeftRing: return "左戒指";
				case EnumEquipSlot.RightRing: return "右戒指";
				default:
					return "";
			}
		}

		public static string? GetString(this EnumSkillSlot? input)
		{
			switch (input)
			{
				case EnumSkillSlot.MainSlot: return "主技能槽";
				case EnumSkillSlot.WeaponSlotTH: return "双手武器技能槽";
				case EnumSkillSlot.WeaponSlot1: return "单手武器能槽 1";
				case EnumSkillSlot.WeaponSlot2: return "单手武器能槽 2";
				case EnumSkillSlot.GeneralSlot1: return "通用技能槽 1";
				case EnumSkillSlot.GeneralSlot2: return "通用技能槽 2";
				case EnumSkillSlot.GeneralSlot3: return "通用技能槽 3";
				default:
					return "";
			}
		}

		public static string? GetString(this EnumSkillCategory? input)
		{
			switch (input)
			{
				case EnumSkillCategory.NotSpecified: return "未分类";
				case EnumSkillCategory.Primary: return "基础";
				case EnumSkillCategory.Core: return "核心";
				case EnumSkillCategory.Secondary: return "次级";
				case EnumSkillCategory.Defence: return "防御";
				case EnumSkillCategory.Strategy: return "战术";
				case EnumSkillCategory.Mastery: return "精通";
				case EnumSkillCategory.Ultimate: return "大招";
				case EnumSkillCategory.Enhancement: return "强化";

				default:
					return "";
			}
		}
		public static string? GetString(this EnumSkillBindingType input)
		{
			switch (input)
			{
				case EnumSkillBindingType.Active: return "主动技能";
				case EnumSkillBindingType.Trigger: return "触发技能";
				case EnumSkillBindingType.Support: return "辅助技能";

				default:
					return "";
			}
		}
		public static string? GetString(this EnumAttackType input)
		{
			switch (input)
			{
				case EnumAttackType.Chain: return "连锁";
				case EnumAttackType.Cleave: return "顺劈";
				case EnumAttackType.Piercing: return "穿刺";
				case EnumAttackType.MultiAttack: return "多重";
				case EnumAttackType.Area: return "范围";

				default:
					return "普通";
			}
		}
		public static string? GetString(this EnumSkillAndStrategy input)
		{
			switch (input)
			{
				case EnumSkillAndStrategy.All: return "技能与策略";
				case EnumSkillAndStrategy.SkillAndSupport: return "技能与辅助";
				case EnumSkillAndStrategy.SkillOnly: return "仅技能";
				case EnumSkillAndStrategy.SupportOnly: return "仅辅助";
				case EnumSkillAndStrategy.StrategyOnly: return "仅策略";

				default:
					return "普通";
			}
		}
		public static string? GetString(this EnumCombatPlayType input)
		{
			switch (input)
			{
				case EnumCombatPlayType.Summary: return "汇总";
				case EnumCombatPlayType.Simple: return "简要";
				case EnumCombatPlayType.Detail: return "详细";
				case EnumCombatPlayType.Talbe: return "列表";
				case EnumCombatPlayType.Cocos: return "灵魂";
				default:
					return "";
			}
		}
		public static string? GetString(this EnumInventotyType input)
		{
			switch (input)
			{
				case EnumInventotyType.Backpack: return "背包";
				case EnumInventotyType.Pickup: return "战利品";
				default:
					return "";
			}
		}

		public static string? GetString(this EnumPresetSkillStrategy input)
		{
			switch (input)
			{
				case EnumPresetSkillStrategy.Target_Enemy_LowLife: return "目标:敌方 条件:低血";
				case EnumPresetSkillStrategy.Target_Self_LowLife: return "目标:自己 条件:低血";
				case EnumPresetSkillStrategy.Target_Friend_LowLife: return "目标:友方 条件:低血";
				case EnumPresetSkillStrategy.Target_Friend_HalfLife: return "目标:友方 条件:半血";
				case EnumPresetSkillStrategy.Target_Friend_InjuriedLife: return "目标:友方 条件:受伤";
				case EnumPresetSkillStrategy.Target_Friend_NotFullLife: return "目标:友方 条件:不满血";
			}
			return "未指定";

		}
	}
}
