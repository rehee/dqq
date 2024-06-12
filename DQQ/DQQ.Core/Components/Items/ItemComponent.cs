using DQQ.Entities;
using DQQ.Enums;
using DQQ.Helper;
using DQQ.Pools;
using DQQ.Profiles.Items;

namespace DQQ.Components.Items
{
	public class ItemComponent : DQQComponent, IItem, IWithRarity
	{
		public virtual bool Avaliable => true;
		public EnumRarity Rarity { get; set; }
		public static ItemComponent New() => new ItemComponent();
		public static T New<T>() where T : ItemComponent, new() => new T();
		public DateTime CreateTime { get; protected set; } = DateTime.UtcNow;
		public Guid? OwnerId { get; set; }
		public int? Quanty { get; set; }
		public int? ItemLevel { get; set; }
		public EnumItem? ItemNumber { get; set; }
		public ItemProfile? ItemProfile => Profile is ItemProfile ? Profile as ItemProfile : null;
		public override void Initialize(IDQQEntity entity, DQQComponent? parent)
		{
			base.Initialize(entity, parent);
			if (entity is ItemEntity ie)
			{
				ItemNumber = ie.ItemNumber;
				var profile = DQQPool.TryGet<ItemProfile, EnumItem?>(ItemNumber);
				Initialize(profile, ie.ItemLevel, ie.Quantity);
			}
		}
		public virtual void Initialize(ItemProfile? itemProfile, int? itemLevel, int? quanty = null)
		{
			if (!DisplayId.HasValue)
			{
				DisplayId = Guid.NewGuid();
			}
			ItemNumber = itemProfile?.ProfileNumber;
			Profile = itemProfile;
			DisplayName = itemProfile?.Name;
			if (ItemProfile?.IsStack != true)
			{
				Quanty = 1;
				ItemLevel = itemLevel.DefaultValue(1);
			}
			else
			{
				ItemLevel = null;
				Quanty = quanty.DefaultValue(1);
			}

		}
		public virtual Task Use()
		{
			return Task.CompletedTask;
		}

		public virtual ItemEntity ToEntity()
		{
			var entity = new ItemEntity();
			entity.Id = this.DisplayId ?? Guid.NewGuid();
			entity.Name = this.DisplayName;
			entity.ActorId = OwnerId;
			entity.ItemNumber = ItemNumber;
			entity.Quantity = ItemProfile?.IsStack == true ? Quanty : 1;
			entity.ItemLevel = ItemProfile?.IsStack == true ? null : ItemLevel.DefaultValue(1);

			return entity;
		}
	}
}
