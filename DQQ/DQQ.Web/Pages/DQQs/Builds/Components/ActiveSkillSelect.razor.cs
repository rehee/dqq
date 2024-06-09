using BootstrapBlazor.Components;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DQQ.Web.Pages.DQQs.Builds.Components
{
	public class ActiveSkillSelectPage : DQQPageBase
	{

		[Parameter]
		public EnumSkillSlot? Slot { get; set; }
		[Parameter]
		public Character? SelectedCharacter { get; set; }
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			
		}

		
	}


}