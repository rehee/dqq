using DQQ.Components.Parameters;
using DQQ.Enums;
using ReheeCmf.Helpers;
using ReheeCmf.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Profiles.Skills.Triggers
{
	public abstract class AbTriggerSkillProfile : GeneralSkill
	{
		public override EnumSkillBindingType BindingType => EnumSkillBindingType.Trigger;

		public override async Task<ContentResponse<bool>> CastSkill(ComponentTickParameter? parameter)
		{
			await Task.CompletedTask;
			var result = new ContentResponse<bool>();
			result.SetSuccess(true);
			return result;
		}
	}
}
