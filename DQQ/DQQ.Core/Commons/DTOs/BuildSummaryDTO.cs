using DQQ.Entities;
using DQQ.Enums;

namespace DQQ.Commons.DTOs
{
	public class BuildSummaryDTO : BuildDTO
	{
		public EnumTargetPriority? TargetPriority { get; set; }
		public Dictionary<EnumEquipSlot, ItemEntity?>? Equips { get; set; }
		public Dictionary<EnumSkillSlot, SkillDTO>? Skills { get; set; }

	}
}
