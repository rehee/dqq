using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Strategies.SkillStrategies
{
	public class SkillTargetDTO
	{
		public EnumTarget? SkillTarget {  get; set; }	
		public EnumTargetPriority? TargetPriority { get; set; }
	}
}
