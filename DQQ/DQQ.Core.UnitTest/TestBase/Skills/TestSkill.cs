using DQQ.Commons;
using DQQ.Components;
using DQQ.Components.Parameters;
using DQQ.Components.Skills;
using DQQ.Components.Stages;
using DQQ.Components.Stages.Maps;
using DQQ.Entities;
using ReheeCmf.Responses;

namespace DQQ.UnitTest.TestBase.Skills
{
	public class TestSkill : SkillComponent
	{
		public int CastTimeCount { get; set; }
		public TestSkill()
		{
			SkillNumber = Enums.EnumSkillNumber.NormalAttack;
			Slot = Enums.EnumSkillSlot.MainSlot;
		}
		public async Task<Boolean> CastSkill(ComponentTickParameter? parameter)
		{
			await Task.CompletedTask;
			CastTimeCount++;
			return true;
		}
		public override async Task<ContentResponse<Boolean>> OnTick(ComponentTickParameter? parameter)
		{
			var result = await base.OnTick(parameter);
			if (result.Success)
			{
				await CastSkill(parameter);
			}
			return result;
		}
		public override void Initialize(IDQQEntity profile, DQQComponent? parent)
		{
			throw new NotImplementedException();
		}
	}
}
