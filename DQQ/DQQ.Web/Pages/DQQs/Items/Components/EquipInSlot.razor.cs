using DQQ.Entities;
using DQQ.Enums;
using DQQ.TickLogs;
using Microsoft.AspNetCore.Components;
using System.Collections.Concurrent;

namespace DQQ.Web.Pages.DQQs.Items.Components
{
	public partial class EquipInSlot
	{
		[Parameter]
		public ConcurrentDictionary<EnumEquipSlot, ItemEntity?>? Equips { get; set; }

		[Parameter]
		public EnumEquipSlot? Slots { get; set; }

		public bool WithEquips => (Equips == null || Slots == null) ? false : Equips?.ContainsKey(Slots.Value) == true ? Equips![Slots.Value] != null : false;

		[Parameter]
		public Func<Task>? OnDismiss { get; set; }

		public Task ThisOnDismiss()
		{
			if (OnDismiss == null)
			{
				return Task.CompletedTask;
			}
			return OnDismiss();
		}
	}
}