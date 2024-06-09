using BootstrapBlazor.Components;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.TickLogs;
using Microsoft.AspNetCore.Components;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using DQQ.Helper;
using DQQ.Components.Items;
namespace DQQ.Web.Pages.DQQs.Items.Components
{
	public partial class EquipInSlot
	{
		[Parameter]
		public ConcurrentDictionary<EnumEquipSlot, ItemEntity?>? Equips { get; set; }

		

		public Color ThisColor => Item.GetColor();

		[Parameter]
		public EnumEquipSlot? Slots { get; set; }

		public bool WithEquips => (Equips == null || Slots == null) ? false : Equips?.ContainsKey(Slots.Value) == true ? Equips![Slots.Value] != null : false;

		[Parameter]
		public Func<Task>? OnDismiss { get; set; }

		[Inject]
		[NotNull]
		public IComponentHtmlRenderer? Renderer { get; set; }

		public ItemEntity? Item { get; set; }

		public string? ComponentString {  get; set; }
		protected override async Task OnParametersSetAsync()
		{
			if (!WithEquips)
			{
				Item = null;
				return;
			}
			Item = Equips![Slots!.Value];
			await Item.SetComponent(Renderer);
			ComponentString = Item?.ComponentString;
		}
		public bool ShowsDismiss => OnDismiss != null;
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