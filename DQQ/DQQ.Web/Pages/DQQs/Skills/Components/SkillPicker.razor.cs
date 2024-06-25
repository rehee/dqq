using BootstrapBlazor.Components;
using DQQ.Commons;
using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using DQQ.Services.SkillServices;
using DQQ.Web.Pages.DQQs.Builds;
using DQQ.Web.Pages.DQQs.Builds.Components;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Services;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace DQQ.Web.Pages.DQQs.Skills.Components
{
	public class SkillPickerPage : DQQPageBase
	{
		[Parameter]
		public EnumSkillSlot? Slot { get; set; }
		[Parameter]
		public Guid? Id { get; set; }

		public SkillDTO? SelectedDTO
		{
			get
			{
				if (SelectedCharacter?.SkillMap?.TryGetValue(Slot ?? EnumSkillSlot.NotSpecified, out var skill) == true)
				{
					return skill;
				}
				return null;
			}
		}

		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();
			if (Slot == null || SelectedCharacter == null)
			{
				return;
			}
			if (SelectedCharacter?.SkillMap?.TryGetValue(Slot.Value, out var skill) == true)
			{

			}
			else
			{
				SelectedCharacter?.SkillMap?.TryAdd(Slot.Value, new Commons.DTOs.SkillDTO
				{
					SkillNumber = EnumSkillNumber.NotSpecified,
				});
			}
		}
		public string CardClass(int index)
		{
			var selected = SelectedIndex == index;
			return $"{(selected? "skill_index_selected border-danger" : "")} position_relative";
		}
		public int? SelectedIndex = null;


		public EnumSkillNumber? ClickedSkill {  get; set; }
		public SkillProfile? ClickedSkillProfile => DQQPool.TryGet<SkillProfile, EnumSkillNumber>(ClickedSkill ?? EnumSkillNumber.NotSpecified);
		public SkillBindingTypeFilter[] BindingTypeFilters { get; set; } = [];

		public SkillProfile[] AvaliableSkills => AllAvaliableSkills.Where(b =>
		 BindingTypeFilters?.Any()==true && BindingTypeFilters.Any(bt => bt.Selected == true && b.BindingType == bt.Value)).ToArray();
		public SkillProfile[] AllAvaliableSkills { get; set; } = [];

		public Task SkillClick(int? index = null)
		{
			SelectedIndex = index;
			ClickedSkill = GetClickedSkill(index);
			StateHasChanged();
			return Task.CompletedTask;
		}
		public SkillProfile? PickedSkill { get; set; }
		public Task SkillPicked(SkillProfile? skill)
		{
			PickedSkill = skill;
			StateHasChanged();
			return Task.CompletedTask;
		}
		[Inject]
		[NotNull]
		public ISkillService? SkillService { get; set; }
		public async Task SaveChange()
		{ 
			await Task.CompletedTask;
			if (SelectedDTO == null|| SelectedIndex == null)
			{
				return;
			}
			if (SelectedIndex == 0)
			{
				SelectedDTO.SkillNumber = PickedSkill?.SkillNumber ?? EnumSkillNumber.NotSpecified;
			}
			else
			{
				var supportIndex = SelectedIndex - 1;
				if (SelectedDTO?.SupportSkills?.Count>= SelectedIndex)
				{
					SelectedDTO.SupportSkills[supportIndex ?? 0].SkillNumber = PickedSkill?.SkillNumber?? EnumSkillNumber.NotSpecified;
				}
			}
			var result = await SkillService.PickSkill(PickSkillDTO.New(SelectedDTO, SelectedCharacter?.DisplayId, Slot));
			ParentRefreshEvent.InvokeEvent(this, EventArgs.Empty);
			if (result.Success)
			{
				await SkillClick(null);
				PickedSkill = null;
			}
			StateHasChanged();
		}
		public bool GetSkillSlotAvaliable(int? index = null)
		{
			switch (index)
			{
				case 0:
					return true;

				case 1: return EnumProgress.SkillSupport1.IsUnlocked(SelectedCharacter);
				case 2: return EnumProgress.SkillSupport2.IsUnlocked(SelectedCharacter);
				case 3: return EnumProgress.SkillSupport3.IsUnlocked(SelectedCharacter);
				case 4: return EnumProgress.SkillSupport4.IsUnlocked(SelectedCharacter);
				case 5: return EnumProgress.SkillSupport5.IsUnlocked(SelectedCharacter);


			}
			return false;
		}
		public EnumSkillNumber? GetClickedSkill(int? index = null)
		{
			switch (index)
			{
				case 0:
					return SelectedDTO?.Profile?.ProfileNumber;
					
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
					return SelectedDTO?.SupportSkills?.Where((b, i) => i == (index - 1))?.Select(b => b.SkillNumber)?.FirstOrDefault();
					
			}
			return null;
		}
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			SelectedIndex = null;
			BindingTypeFilters = [
				SkillBindingTypeFilter.New<SkillBindingTypeFilter>(EnumSkillBindingType.Active,true),
				SkillBindingTypeFilter.New<SkillBindingTypeFilter>(EnumSkillBindingType.Trigger,true),
				SkillBindingTypeFilter.New<SkillBindingTypeFilter>(EnumSkillBindingType.Support,true),
				];

			AllAvaliableSkills = DQQPool.SkillPool.Select(b => b.Value).Where(b => b.IsAvaliableForCharacter(SelectedCharacter)).ToArray();
		}

		public bool Avaliable
		{
			get
			{
				if (Slot == null || SelectedCharacter == null)
				{
					return false;
				}
				switch (Slot)
				{
					case EnumSkillSlot.WeaponSlotTH: return SelectedCharacter?.WithTwoHandWeapon == true;
					case EnumSkillSlot.WeaponSlot1: return SelectedCharacter?.WithWeapon1 == true;
					case EnumSkillSlot.WeaponSlot2: return SelectedCharacter?.WithWeapon2 == true;
					default: return true;
				}
			}
		}

		public Color SkillBordColor => Avaliable ? Color.None : Color.Danger;


		public EnumSkillAndStrategy OptionSelected { get; set; }
		public async Task SelectedItemChanged(SelectedItem item)
		{
			Enum.TryParse<EnumSkillAndStrategy>(item.Value, out var option);
			await SlotSelectOpen(option);
		}

		public async Task PickSkill()
		{
			await this.dialogService.ShowComponent<SkillSelector>(
					new Dictionary<string, object?>
					{
						["ParentRefreshEvent"] = ParentRefreshEvent,
						["SelectedCharacter"] = SelectedCharacter,
						["Slot"] = Slot,
						["CardTitle"] = "主动技能选择",
						["SupportSkillIndex"] = null,
						["BindingTypes"] =  new EnumSkillBindingType[] { EnumSkillBindingType.Active, EnumSkillBindingType.Trigger } 
					}
					);
		}

		public async Task SlotSelectOpen(EnumSkillAndStrategy? options)
		{
			var op = options ?? OptionSelected;
			if (options != null)
			{
				OptionSelected = options.Value;
			}
			await dialogService.ShowComponent<SkillAndStrategyChange>(
			new Dictionary<string, object?>
			{
				["ParentRefreshEvent"] = ParentRefreshEvent,
				["SelectedCharacter"] = SelectedCharacter,
				["Option"] = op,
				["Slot"] = Slot
			}
			, "");
		}
		
	}
}