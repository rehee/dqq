using DQQ.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Supports
{
	public abstract class AbSupportSkillProfile : GeneralSkill
	{
		public override EnumSkillBindingType BindingType => EnumSkillBindingType.Support;
	}
}
