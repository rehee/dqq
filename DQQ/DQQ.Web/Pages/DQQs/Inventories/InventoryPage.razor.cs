using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using DQQ.Web.Services.ItemServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Inventories
{
	public class InventoryPagePage: DQQPageBase
	{
		public EnumEquipSlot? SelectedSlot { get; set; }
		public ItemEntity? ItemSelected { get; set; }


		public Task OnItemClicked(ItemEntity item)
		{
			ItemSelected = item;
			return Task.CompletedTask;
		}

		public Task OnSelectedSlotChange(EnumEquipSlot? selectedSlot)
		{
			SelectedSlot = selectedSlot;
			ItemSelected = null;
			return Task.CompletedTask;
		}

		[Inject]
		[NotNull]
		public IItemService? ItemService { get; set; }

		public ItemEntity[]? Items {  get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			await RefreshInventory();
		}
		public async Task RefreshInventory()
		{
			Items = (await ItemService.ActorInventory(SelectedCharacter?.DisplayId))?.ToArray();
		}

		
	}
}