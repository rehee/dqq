using DQQ.Components.Stages.Actors;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Web.Pages.DQQs.Items;
using DQQ.Web.Pages.DQQs.Strategies;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Builds
{
	public class BuildPagePage : DQQPageBase
	{
		[Parameter]
		public Character? SelectedCharacter { get; set; }

		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();
			StateHasChanged();
		}

		public async Task ShowManagement()
		{
			await dialogService.ShowComponent<BuildManage>(
				new Dictionary<string, object?>
				{
					["ActorId"] = SelectedCharacter?.DisplayId,
					["ParentRefreshEvent"] = ParentRefreshEvent,
					["SelectedCharacter"] = SelectedCharacter,
				}

				, "");
		}

		public async Task ChangeTarget()
		{
			await dialogService.ShowComponent<TargetPriority>(
				 new Dictionary<string, object?>
				 {
					 ["TargetPriority"] = SelectedCharacter?.TargetPriority,
					 ["ActorId"] = SelectedCharacter?.DisplayId
				 }, "", true, async save =>
				 {
					 await Task.CompletedTask;
					 ParentRefreshEvent?.InvokeEvent(this, new EventArgs());
				 });
		}

		public async Task ShowInventory()
		{
			await dialogService.ShowComponent<ActorInventory>(
				new Dictionary<string, object?>
				{
					["ActorId"] = SelectedCharacter?.DisplayId,
					["ParentRefreshEvent"] = ParentRefreshEvent,
					["SelectedCharacter"] = SelectedCharacter,
					["ParentGuid"] = ParentGuid,

				}, "");
		}
	}
}