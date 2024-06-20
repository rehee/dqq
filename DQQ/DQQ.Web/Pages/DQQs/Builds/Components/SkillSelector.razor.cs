using BootstrapBlazor.Components;
using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using DQQ.Services.SkillServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		public int? SupportSkillIndex { get; set; }

		public int MaxSlotIndex => Slot.MaxSkillNumber() - 1;

		public int SelectedIndex => SupportSkillIndex == null ? 0 : SupportSkillIndex > MaxSlotIndex ? MaxSlotIndex : SupportSkillIndex < 0 ? 0 : SupportSkillIndex.Value;

		[Inject]
		[NotNull]
		public ISkillService? SkillService { get; set; }

		public SkillDTO? SelectedSkillDTO
		{
			get
			{
				if (IsActiveSkill)
				{
					return SelectedCharacter?.GetSelectedSkillDTO(Slot);
				}
				if (SelectedCharacter?.GetSelectedSkillDTO(Slot)?.SupportSkills == null)
				{
					SelectedCharacter!.GetSelectedSkillDTO(Slot)!.SupportSkills = new List<SkillDTO>();
				}
				var length = SelectedCharacter!.GetSelectedSkillDTO(Slot)!.SupportSkills!.Count();
				if (SelectedIndex >= length)
				{
					for (var i = 0; i <= (SelectedIndex - length); i++)
					{
						SelectedCharacter!.GetSelectedSkillDTO(Slot)!.SupportSkills!.Add(new SkillDTO { SkillNumber = EnumSkillNumber.NotSpecified });
					}
				}

				return SelectedCharacter!.GetSelectedSkillDTO(Slot)!.SupportSkills?[SelectedIndex];
			}
		}

		public bool IsActiveSkill => SupportSkillIndex == null;


		[NotNull]
		public List<SkillProfile>? SkillProfiles { get; set; }
		public Task<QueryData<SkillProfile>> OnQueryAsync(QueryPageOptions options)
		{
			var items = SkillProfiles.Where(b => b.BindingType == EnumSkillBindingType.Active).Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems);
			return Task.FromResult(new QueryData<SkillProfile>()
			{
				Items = items,
				TotalCount = SkillProfiles.Count()
			});
		}
		public Task<QueryData<SkillProfile>> OnQueryAsync2(QueryPageOptions options)
		{
			var items = SkillProfiles.Where(b => b.BindingType == EnumSkillBindingType.Trigger).Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems);
			return Task.FromResult(new QueryData<SkillProfile>()
			{
				Items = items,
				TotalCount = SkillProfiles.Count()
			});
		}
		public Task<QueryData<SkillProfile>> OnQueryAsync3(QueryPageOptions options)
		{
			var items = SkillProfiles.Where(b => b.BindingType == EnumSkillBindingType.Support).Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems);
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
				SelectedSkillDTO.SkillNumber = profile?.SkillNumber ?? EnumSkillNumber.NotSpecified;
			}
			StateHasChanged();
		}

		public async Task SkillUnselect()
		{
			await Task.CompletedTask;
			if (SelectedSkillDTO != null)
			{
				SelectedSkillDTO.SkillNumber = EnumSkillNumber.NotSpecified;
			}
		}
		public async Task SaveSkill()
		{
			var dto = SelectedCharacter?.GetSelectedSkillDTO(Slot);

			var result = await SkillService.PickSkill(
				PickSkillDTO.New(dto, SelectedCharacter?.DisplayId, Slot));
			ParentRefreshEvent?.InvokeEvent(this, new EventArgs());
			await CloseDynamicDialog();
		}
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			SkillProfiles = DQQPool.SkillPool.Select(b => b.Value)
				.PlayerAvaliableSkill(SelectedCharacter?.Level)
				.ToList();
		}
	}
}