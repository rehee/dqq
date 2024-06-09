using BootstrapBlazor.Components;
using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Builds.Components
{
	public class SkillSelectorPage : DQQPageBase
	{
		[Parameter]
		public string? CardTitle { get; set; }

		[Parameter]
		public EnumSkillBindingType[]? BindingTypes { get; set; }

		[Parameter]
		public EnumSkillSlot? Slot { get; set; }

		[Parameter]
		public Character? SelectedCharacter { get; set; }

		public SkillDTO? SelectedSkillDTO => SelectedCharacter?.GetSelectedSkillDTO(Slot);

		

		[NotNull]
		public List<SkillProfile>? SkillProfiles { get; set; }
		public Task<QueryData<SkillProfile>> OnQueryAsync(QueryPageOptions options)
		{
			var items = SkillProfiles.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems);
			return Task.FromResult(new QueryData<SkillProfile>()
			{
				Items = items,
				TotalCount = SkillProfiles.Count()
			});
		}
		public bool CollapsedGroupCallback(object? groupKey)
		{
			if (SelectedSkillDTO?.Profile != null)
			{
				return groupKey?.ToString()?.EndsWith(SelectedSkillDTO?.Profile?.Category.GetEnumString() ?? "") != true;
			}
			return groupKey?.ToString()?.EndsWith(EnumSkillCategory.NotSpecified.GetEnumString() ?? "") != true;
		}
		public async Task SkillSelected(SkillProfile? profile)
		{
			await Task.CompletedTask;
			if (SelectedSkillDTO != null)
			{
				SelectedSkillDTO.SkillNumber = profile?.SkillNumber ?? EnumSkill.NotSpecified;
			}
		}
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			SkillProfiles = DQQPool.SkillPool.Select(b => b.Value)
				.Where(b => b.NoPlayerSkill != true)
				.Where(b => BindingTypes == null ? true : BindingTypes.Contains(b.BindingType))
				.ToList();
		}
	}
}