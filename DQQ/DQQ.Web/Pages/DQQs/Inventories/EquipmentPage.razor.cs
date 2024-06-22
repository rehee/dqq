using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services.ItemServices;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Inventories
{
	public class EquipmentPagePage : DQQPageBase
	{
		[Parameter]
		public EnumEquipSlot? SelectedSlot { get; set; }
		

		[Parameter]
		public EventCallback<EnumEquipSlot?> SelectedSlotClicked { get; set; }

		public Task EquipSlotClicked(EnumEquipSlot? slot)
		{
			if (SelectedSlotClicked.HasDelegate)
			{
				SelectedSlotClicked.InvokeAsync(slot);
			}
			return Task.CompletedTask;
		}

		public string SlotActiveClass(EnumEquipSlot? slot)
		{
			if (slot ==null||slot!= SelectedSlot)
			{
				return "col-4";
			}
			return "col-4 select_slot";
		}

	}
}