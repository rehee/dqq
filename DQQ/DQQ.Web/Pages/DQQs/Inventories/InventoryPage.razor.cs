using BootstrapBlazor.Components;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using DQQ.Web.Pages.DQQs.Characters;
using DQQ.Web.Services.ItemServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Inventories
{
	public class InventoryPagePage: DQQPageBase
	{
		public EnumEquipSlot? SelectedSlot { get; set; }
		public ItemEntity? ItemSelected { get; set; }
		public bool IsMultiSelect { get; set; }

		public EnumInventotyType CurrentInventoryType {  get; set; }
		public  Task OnTabChange(EnumInventotyType tabItem)
		{
			CurrentInventoryType = tabItem;
			StateHasChanged();
			return Task.CompletedTask;
		}

		public Task IsMultiSelectChange(bool isMultiSelect)
		{
			IsMultiSelect=isMultiSelect;
			StateHasChanged();
			return Task.CompletedTask;
		}

		public Task OnItemClicked(ItemEntity item)
		{
			ItemSelected = item;
			return Task.CompletedTask;
		}

		public Task OnSelectedSlotChange(EnumEquipSlot? selectedSlot)
		{
			SelectedSlot = selectedSlot;
			if (ItemSelected != null)
			{
				if (ItemSelected?.GetAvaliableSlots()?.Any(b => b == selectedSlot) != true)
				{
					ItemSelected = null;
				}
			}
			StateHasChanged();
			return Task.CompletedTask;
		}

		[Inject]
		[NotNull]
		public IItemService? ItemService { get; set; }

		[Parameter]
		public ItemEntity[]? Items {  get; set; }
		
		public ItemEntity[]? PickupItems { get; set; }


		public ItemEntity[]? CombineItemSource => CurrentInventoryType == EnumInventotyType.Backpack ? Items : PickupItems;

		[Parameter]

		public EventCallback InventoryQueryRequest {  get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			await RefreshInventory(false);
			PickupItems = (await ItemService.PickableItems(SelectedCharacter?.DisplayId))?.ToArray();
		}
		public async Task RefreshInventory(bool forceRefresh)
		{
			if (Items == null|| forceRefresh==true)
			{
				if (InventoryQueryRequest.HasDelegate)
				{
					InventoryQueryRequest.InvokeAsync();
				}
			}
			PickupItems = (await ItemService.PickableItems(SelectedCharacter?.DisplayId))?.ToArray();
			ItemSelected = null;
		}
		
	
		public bool IsOpen { get; set; }
		public bool ShowDrawer => BreakPoint == BreakPoint.ExtraExtraSmall || BreakPoint == BreakPoint.ExtraSmall || BreakPoint == BreakPoint.Small;
		
		public async Task OpenOrClose(bool isOpen)
		{
			await Task.CompletedTask;
			if (ShowDrawer)
			{
				IsOpen = isOpen;
			}
			else
			{
				IsOpen = false;
			}
			
			StateHasChanged();
		}
		
		
		


	}
}