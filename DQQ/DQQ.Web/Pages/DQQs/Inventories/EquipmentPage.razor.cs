using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Inventories
{
	public class EquipmentPagePage : DQQPageBase
	{
		public EnumEquipSlot? SelectedSlot { get; set; }

		public ItemEntity? SelectedItem => SelectedCharacter?.EquipItems?.Where(b => b.Key == SelectedSlot).Select(b => b.Value).FirstOrDefault();


		public async Task EquipSlotClicked(EnumEquipSlot? slot)
		{
			await Task.CompletedTask;
			SelectedSlot = slot;
			StateHasChanged();

		}

		[Inject]
		[NotNull]
		public IItemService? ItemService { get; set; }
		public async Task UnEquip()
		{
			if (SelectedSlot == null)
			{
				return;
			}
			await ItemService.UnEquipItem(SelectedCharacter?.DisplayId, SelectedSlot.Value);
			ParentRefreshEvent.InvokeEvent(this, new EventArgs());
			StateHasChanged();
		}
	}
}