using DQQ.Entities;
using DQQ.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Commons.DTOs
{
	public class BuildSummaryDTO : BuildDTO
	{
		public EnumTargetPriority? TargetPriority { get; set; }
		public Dictionary<EnumEquipSlot, ItemEntity?>? Equips { get; set; }
		public Dictionary<EnumSkillSlot, SkillDTO>? Skills { get; set; }

	}
}
