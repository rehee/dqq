using DQQ.Combats;
using DQQ.Commons;
using DQQ.Commons.DTOs;
using DQQ.Components.Items.Equips;
using DQQ.Components.Parameters;
using DQQ.Components.Skills;
using DQQ.Components.Stages.Maps;
using DQQ.Consts;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Profiles.Skills;
using DQQ.TickLogs;
using ReheeCmf.Responses;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace DQQ.Components.Stages.Actors.Characters
{
	public class Character : Actor, IEquippableCharacter
	{
		public Character()
		{
			Equips = new ConcurrentDictionary<EnumEquipSlot, IEquptment?>();
			EquipItems = new ConcurrentDictionary<EnumEquipSlot, ItemEntity?>();
		}
		public override EnumTargetLevel PowerLevel => EnumTargetLevel.Elite;
		public string? OwnerId { get; set; }
		public string? CurrentXP { get; set; }
		public string? NextLevelXP { get; set; }

		public decimal? PercentageXP
		{
			get
			{
				if (String.IsNullOrEmpty(NextLevelXP))
				{
					return null;
				}
				if (String.IsNullOrEmpty(CurrentXP))
				{
					return 0;
				}
				var current = BigInteger.Parse(CurrentXP) * 10000;
				var next = BigInteger.Parse(NextLevelXP);
				var percent = (int)(current / next);
				return percent/100m;
			}

		}

		public EnumChapter Chapter { get; set; }
		public EnumMapNumber Map { get; set; }
		[NotMapped]
		[JsonIgnore]
		public ConcurrentDictionary<EnumEquipSlot, IEquptment?> Equips { get; set; }

		[NotMapped]
		public ConcurrentDictionary<EnumEquipSlot, ItemEntity?> EquipItems { get; set; }

		public Dictionary<EnumSkillSlot, SkillDTO>? SkillMap { get; set; }


		[JsonIgnore]
		public bool WithTwoHandWeapon => EquipItems == null ? false :
			EquipItems.Any(b => b.Key == EnumEquipSlot.MainHand && b.Value?.EquipType == EnumEquipType.TwoHandWeapon);

		[JsonIgnore]
		public bool WithWeapon1 => (EquipItems == null || WithTwoHandWeapon) ? false :
			EquipItems?.Any(b => b.Key == EnumEquipSlot.MainHand && b.Value != null) == true;

		[JsonIgnore]
		public bool WithWeapon2 => (EquipItems == null || WithTwoHandWeapon) ? false :
			EquipItems?.Any(b => b.Key == EnumEquipSlot.OffHand && b.Value != null) == true;

		public ActorEntity ToActorEntity()
		{
			var entity = new ActorEntity();


			return entity;
		}
		public override async Task<ContentResponse<bool>> OnTick(ComponentTickParameter parameter)
		{
			var result = await base.OnTick(parameter);
			if (!result.Success)
			{
				return result;
			}
			var equips = Equips?.Select(b => b.Value).Where(b => b != null).Select(b => b!).ToArray();
			if (equips?.Any() == true)
			{
				foreach (var e in equips)
				{
					await e.OnTick(parameter);
				}
			}
			return result;
		}
		public override void Initialize(IDQQEntity entity, DQQComponent? parent)
		{
			base.Initialize(entity, parent);




			var skills = Skills?.ToDictionary(b => b.Slot, b => SkillDTO.New(b)) ?? [];

			SkillMap = new Dictionary<EnumSkillSlot, SkillDTO>();
			CombatPanel.StaticPanel.MaximunLife = DQQGeneral.CharacterBasicHP + (DQQGeneral.HPPerLevel * (Level - 1));
			foreach (EnumSkillSlot slot in Enum.GetValues(typeof(EnumSkillSlot)))
			{
				skills.TryGetValue(slot, out var skill);
				SkillMap.TryAdd(slot, skill ?? SkillDTO.New(EnumSkillNumber.NotSpecified, slot));
			}

			if (entity is ActorEntity ae)
			{
				Chapter = ae.Chapter;
				CurrentXP = ae.CurrentXP;
				NextLevelXP = XPHelper.GetNextLevelUpExp(ae.Level).ToString();

				var equips = ae.Equips!.DistinctBy(b => b.EquipSlot).ToArray();

				foreach (var equip in equips)
				{
					if (equip?.Item == null)
					{
						continue;
					}
					var equipComponent = equip.Item.GenerateTypedComponent<EquipComponent>(this);
					if (equipComponent == null)
					{
						continue;
					}
					Equips.AddOrUpdate(equip.EquipSlot!.Value, equipComponent!, (a, b) => equipComponent);
					EquipItems.AddOrUpdate(equip.EquipSlot!.Value, equip?.Item, (a, b) => equip?.Item);
				}
			}
			foreach (var skil in Skills ?? [])
			{
				skil.CheckAndSetAvaliableForUser(this);
			}
			this.TotalEquipProperty();
			this.CurrentHP = CombatPanel.DynamicPanel.MaximunLife.DefaultValue(1);
		}


		public SkillDTO? GetSelectedSkillDTO(EnumSkillSlot? Slot)
		{
			if (Slot == null) return null;

			if (SkillMap?.TryGetValue(Slot!.Value, out var selected) == true)
			{
				if (selected != null) return selected;
			}
			var result = SkillDTO.New(EnumSkillNumber.NotSpecified);
			SkillMap?.TryAdd(Slot!.Value, result);
			return result;
		}

		protected override void SelfBeforeDamageReduction(ComponentTickParameter parameter)
		{
			var equips = Equips?.Select(b => b.Value).Where(b => b != null).Select(b => b!).ToArray();
			if (equips?.Any() == true)
			{
				foreach (var e in equips)
				{
					e.BeforeDamageReduction(parameter);
				}
			}
			base.SelfBeforeDamageReduction(parameter);
		}
		protected override void SelfDamageReduction(ComponentTickParameter parameter)
		{

			var equips = Equips?.Select(b => b.Value).Where(b => b != null).Select(b => b!).ToArray();
			if (equips?.Any() == true)
			{
				foreach (var e in equips)
				{
					e.DamageReduction(parameter);
				}
			}
			base.SelfDamageReduction(parameter);
		}


		protected override void SelfBeforeTakeDamage(ComponentTickParameter parameter)
		{
			var equips = Equips?.Select(b => b.Value).Where(b => b != null).Select(b => b!).ToArray();
			if (equips?.Any() == true)
			{
				foreach (var e in equips)
				{
					e.BeforeTakeDamage(parameter);
				}
			}
			base.SelfBeforeTakeDamage(parameter);
		}
		protected override void SelfAfterTakeDamage(ComponentTickParameter parameter)
		{
			var equips = Equips?.Select(b => b.Value).Where(b => b != null).Select(b => b!).ToArray();
			if (equips?.Any() == true)
			{
				foreach (var e in equips)
				{
					e.AfterTakeDamage(parameter);
				}
			}
			var a = this;
			base.SelfAfterTakeDamage(parameter);
		}

	}
}
