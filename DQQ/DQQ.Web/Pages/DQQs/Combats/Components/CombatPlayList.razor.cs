
using DQQ.Consts;
using DQQ.Enums;

namespace DQQ.Web.Pages.DQQs.Combats.Components
{
	public class CombatPlayListPage : CombatPage
	{
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			//var combatPlayType = LocalStorageService.GetItem<EnumCombatPlayType?>(WebConsts.CombatStyleTypeKey);
			//if (combatPlayType == null || combatPlayType == EnumCombatPlayType.NotSpecified)
			//{
			//	combatPlayType = EnumCombatPlayType.Detail;
			//}
			//PlayType = combatPlayType!.Value;

			PlayType = EnumCombatPlayType.Detail;
			StateHasChanged();
		}
	}
}