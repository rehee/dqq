using DQQ.Entities;
using DQQ.Enums;

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
	}
}