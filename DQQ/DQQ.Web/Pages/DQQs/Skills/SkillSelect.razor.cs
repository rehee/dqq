using DQQ.Commons.DTOs;
using DQQ.Enums;
using DQQ.Profiles.Skills;
using DQQ.Services.SkillServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Skills
{
	public class SkillSelectPage : DQQPageBase
	{

		[Inject]
		[NotNull]
		public ISkillService? skillServices { get; set; }

		[Parameter]
		public EnumSkill? SelectSkillNumber { get; set; }
		[Parameter]
		public int? Slot { get; set; }
		[Parameter]
		public Guid? ActorId { get; set; }

		public IEnumerable<SkillDTO>? Skills { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			Skills = await skillServices.GetAllSkills();
			StateHasChanged();
		}

		public async Task PickSkill(EnumSkill? skillNumber)
		{
			await Task.CompletedTask;
			SelectSkillNumber = skillNumber;
			StateHasChanged();
		}

		public override async Task<bool> SaveFunction()
		{
			if (ActorId == null || SelectSkillNumber == null || Slot == null)
			{
				return false;
			}
			var result = await skillServices.PickSkill(new Commons.DTOs.PickSkillDTO
			{
				ActorId = ActorId!.Value,
				SkillNumber = SelectSkillNumber!.Value,
				Slot = (EnumSkillSlot)Slot
			});
			return result.Success;
		}
	}
}