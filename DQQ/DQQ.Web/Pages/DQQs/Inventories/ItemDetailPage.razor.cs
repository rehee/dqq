using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Inventories
{
	public class ItemDetailPagePage: DQQPageBase
	{
		[Parameter]
		public EnumEquipSlot? SelectedSlot { get; set; }

		[Parameter]
		public ItemEntity? ItemSelected { get; set; }

		public ItemEntity? SelectedItem => SelectedCharacter?.EquipItems?.Where(b => b.Key == SelectedSlot).Select(b => b.Value).FirstOrDefault();
		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();
		
		}
		[Parameter]
		public EventCallback EquipChange { get; set; }

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
			if (EquipChange.HasDelegate)
			{
				await EquipChange.InvokeAsync();
			}
			StateHasChanged();
		}

		public async Task Equip()
		{
			if (SelectedSlot == null || ItemSelected==null)
			{
				return;
			}
			await ItemService.EquipItem(SelectedCharacter?.DisplayId, ItemSelected?.Id,SelectedSlot);
			ParentRefreshEvent.InvokeEvent(this, new EventArgs());
			if (EquipChange.HasDelegate)
			{
				await EquipChange.InvokeAsync();
			}
			StateHasChanged();
		}
	}
}