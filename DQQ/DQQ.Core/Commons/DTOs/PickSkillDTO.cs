using DQQ.Enums;
using DQQ.Strategies.SkillStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Commons.DTOs
{
	public class PickSkillDTO
	{
		public static PickSkillDTO New(SkillDTO? skillDTO, Guid? actorId, EnumSkillSlot? slot)
		{
			if (skillDTO == null)
			{
				return new PickSkillDTO();
			}

			return new PickSkillDTO
			{
				ActorId = actorId,
				SkillNumber = skillDTO.SkillNumber,
				Slot = slot,
				Strategies = skillDTO?.SkillStrategies?.ToArray(),
				SupportSkill = skillDTO?.SupportSkills?.Select(b => b.SkillNumber).Where(b => b != EnumSkill.NotSpecified).ToArray()
			};
		}
		public Guid? ActorId { get; set; }
		public EnumSkill? SkillNumber { get; set; }
		public EnumSkillSlot? Slot { get; set; }
		public SkillStrategy[]? Strategies { get; set; }
		public EnumSkill[]? SupportSkill { get; set; }
	}
}
