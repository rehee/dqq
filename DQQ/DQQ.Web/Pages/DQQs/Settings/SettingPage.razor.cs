using BootstrapBlazor.Components;
using DQQ.Consts;
using DQQ.Enums;

namespace DQQ.Web.Pages.DQQs.Settings
{
	public class SettingPagePage : DQQPageBase
	{
		public EnumCombatPlayType CombatPlayType { get; set; } = EnumCombatPlayType.Detail;


		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

			var status = await GameStatusService.GetOrCreateGameStatus();
			if (status?.Success == true)
			{
				if(!(status?.Content?.CombatPlayType == null || status?.Content?.CombatPlayType == EnumCombatPlayType.NotSpecified))
				{
					CombatPlayType = status!.Content!.CombatPlayType;
				}
			}
			


		}

		public async Task SelectedChanged(SelectedItem item)
		{
			await Task.CompletedTask;

			var status = await GameStatusService.GetOrCreateGameStatus();
			if (status?.Success == true)
			{
				status!.Content!.CombatPlayType = CombatPlayType;
			}
			StateHasChanged();
		}

	}
}