using DQQ.Components.Stages.Actors;
using DQQ.Enums;
using System.ComponentModel.DataAnnotations;

namespace DQQ.Entities
{
	public class ActorEntity : DQQEntityBase<Actor>
	{
		public int Level { get; set; }
		public string? CurrentXP { get; set; }
		
		public string? OwnerId { get; set; }
		public Int64? MaxHP { get; set; }
		public Int64? BasicDamage { get; set; }
		public EnumTargetPriority? TargetPriority { get; set; }

		public virtual List<SkillEntity>? Skills { get; set; }
		public virtual List<ItemEntity>? Items { get; set; }
		public virtual List<ActorEquipmentEntity>? Equips { get; set; }

		[Timestamp]
		public byte[]? Version { get; set; }
	}


}
