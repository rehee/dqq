using DQQ.Entities;
using DQQ.Enums;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Items.Components
{
	public partial class EquipCompare
	{
		[Parameter]
		public Dictionary<EnumEquipSlot, ItemEntity?>? EquipedItems { get; set; }

		[Parameter]
		public ItemEntity? CurrentItem { get; set; }

		[Parameter]
		public EnumEquipSlot? Slot { get; set; }

	}
}