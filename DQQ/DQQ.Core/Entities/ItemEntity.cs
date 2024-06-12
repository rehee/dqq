using DQQ.Combats;
using DQQ.Components.Affixes;
using DQQ.Components.Items;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Items;
using DQQ.Profiles.Items.Equipments;
using Microsoft.Linq.Translations;
using ReheeCmf.Commons.Jsons.Options;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DQQ.Entities
{
	public class ItemEntity : DQQEntityBase<ItemComponent>, ICombatProperty, IWithRarity
	{
		public EnumItem? ItemNumber { get; set; }
		[NotMapped]
		public ItemProfile? Profile => DQQPool.TryGet<ItemProfile, EnumItem?>(ItemNumber);
		[ForeignKey(nameof(ActorEntity))]
		public Guid? ActorId { get; set; }
		[JsonIgnore]
		public virtual ActorEntity? Actor { get; set; }

		public int? Quantity { get; set; }
		public int? ItemLevel { get; set; }

		public EnumRarity Rarity { get; set; }

		[NotMapped]
		public EnumItemType? ItemType => Profile?.ItemType;

		[NotMapped]
		public EquipProfile? EquipProfile => Profile is EquipProfile ? Profile as EquipProfile : null;



		[NotMapped]
		public EnumEquipType? EquipType => EquipProfile?.EquipType;
		public bool IsEquipped => IsEquippedExp.Evaluate(this);
		public static CompiledExpression<ItemEntity, bool> IsEquippedExp =
			DefaultTranslationOf<ItemEntity>.Property(e => e.IsEquipped)
				.Is(e => e.ActorEquipments != null && e.ActorEquipments.Any() == true);
		public virtual List<ActorEquipmentEntity>? ActorEquipments { get; set; }

		[NotMapped]
		public string? ComponentString { get; protected set; }
		public void SetComponentString(string? componentString)
		{
			ComponentString = componentString;
		}
		[NotMapped]
		public Dictionary<EnumEquipSlot, string>? CompareComponentString { get; protected set; }
		public void SetCompareString(Dictionary<EnumEquipSlot, string>? componentString)
		{
			CompareComponentString = componentString;
		}

		[NotMapped]
		public string? CompareComponentStringAll { get; protected set; }
		public void SetCompareStringAll(string? componentString)
		{
			CompareComponentStringAll = componentString;
		}

		public decimal? AttackPerSecond { get; set; }
		public decimal? ArmorPercentage { get; set; }
		public decimal? Resistance { get; set; }
		public long? MaximunLife { get; set; }
		public long? Armor { get; set; }
		public long? Damage { get; set; }
		public long? MainHand { get; set; }
		public long? OffHand { get; set; }
		public decimal? DamageModifier { get; set; }


		public long? AttackRating { get; set; }
		public string? AffixesJson { get; set; }

		[NotMapped]
		public AffixeComponent[] Affixes
		{
			get
			{
				if (String.IsNullOrEmpty(AffixesJson))
				{
					return [];
				}
				try
				{
					return JsonSerializer.Deserialize<AffixeComponent[]>(AffixesJson, JsonOption.DefaultOption) ?? [];
				}
				catch { }
				return [];
			}
		}

		public long? Defence { get; set; }
		public decimal? DefencePercentage { get; set; }
		public decimal? BlockChance { get; set; }
		public decimal? BlockRecovery { get; set; }
		public decimal? DodgeChance { get; set; }
		public long? PhysicsResistance { get; set; }
		public long? FireResistance { get; set; }
		public long? ColdResistance { get; set; }
		public long? LightningResistance { get; set; }
		public long? ChaosResistance { get; set; }
		public decimal? PhysicsDamageModifier { get; set; }
		public decimal? FireDamageModifier { get; set; }
		public decimal? ColdDamageModifier { get; set; }
		public decimal? LightningDamageModifier { get; set; }
		public decimal? ChaosDamageModifier { get; set; }
	}
}
