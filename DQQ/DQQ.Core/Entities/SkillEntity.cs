using DQQ.Components.Skills;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using System.ComponentModel.DataAnnotations.Schema;

namespace DQQ.Entities
{
	public class SkillEntity : DQQEntityBase<SkillComponent>
	{
		public EnumSkillSlot Slot { get; set; }
		[ForeignKey(nameof(ActorEntity))]
		public Guid? ActorId { get; set; }
		public virtual ActorEntity? Actor { get; set; }
		public EnumSkillNumber? SkillNumber { get; set; }
		public SkillProfile? Profile => DQQPool.TryGet<SkillProfile, EnumSkillNumber>(SkillNumber ?? EnumSkillNumber.NotSpecified);
		public string? SkillStrategy { get; set; }
		public string? SupportSkills { get; set; }
	}
}
