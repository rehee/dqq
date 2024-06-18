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

			var combatPlayType = LocalStorageService.GetItem<EnumCombatPlayType?>(WebConsts.CombatStyleTypeKey);
			if (combatPlayType == null || combatPlayType == EnumCombatPlayType.NotSpecified)
			{
				LocalStorageService.SetItem<EnumCombatPlayType?>(WebConsts.CombatStyleTypeKey, CombatPlayType);
			}
			else
			{
				CombatPlayType = combatPlayType.Value;
			}

		}

		public async Task SelectedChanged(SelectedItem item)
		{
			await Task.CompletedTask;
			LocalStorageService.SetItem<EnumCombatPlayType?>(WebConsts.CombatStyleTypeKey, CombatPlayType);
			StateHasChanged();
		}

	}
}