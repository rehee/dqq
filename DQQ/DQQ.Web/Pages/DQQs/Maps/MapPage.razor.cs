using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Profiles.Maps;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Maps
{
	public class MapPagePage : DQQPageBase
	{
		public MapProfile[]? UnlockedMaps => SelectedCharacter?.GetUnlockedMaps();
		[Parameter]
		public EnumMapNumber? SelectedMap { get; set; }
		[Parameter]
		public EventCallback<EnumMapNumber?> OnMapSelected { get; set; }
		public MapProfile? SelectedMapProfile => UnlockedMaps?.FirstOrDefault(b => b.ProfileNumber == SelectedMap);

		

		public Task SelectThisMap(EnumMapNumber? mapId)
		{
			if (OnMapSelected.HasDelegate)
			{
				OnMapSelected.InvokeAsync(mapId);
			}
			return Task.CompletedTask;
		}
	}
}