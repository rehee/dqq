
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Items
{
	public class ActorInventoryPage : DQQPageBase
	{
		[Inject]
		[NotNull]
		public IItemService? itemService { get; set; }
		public IEnumerable<ItemEntity>? Items { get; set; }

		[Parameter]
		public Guid? ActorId { get; set; }

		[Parameter]
		public Character? SelectedCharacter { get; set; }

		public async Task UnEquipSlog(EnumEquipSlot? slot)
		{
			if (slot == null)
			{
				return;
			}
			await EquipItem(null, slot);
		}

		protected override void OnParametersSet()
		{
			base.OnParametersSet();
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await base.OnAfterRenderAsync(firstRender);

		}

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			await Refresh();
		}

		public async Task EquipItem(Guid? id, EnumEquipSlot? slot)
		{
			if (id == null)
			{
				var result = await itemService.UnEquipItem(ActorId, slot!.Value);

			}
			else
			{
				var result = await itemService.EquipItem(ActorId, id, slot);

			}
			await Refresh();
			if (ParentRefreshEvent != null)
			{
				ParentRefreshEvent?.InvokeEvent(this, EventArgs.Empty);
			}

		}

		public async Task Refresh()
		{
			Items = await itemService.ActorInventory(ActorId);
			SelectedCharacter = await characterService.GetCharacter(ActorId);
			StateHasChanged();
		}
	}
}