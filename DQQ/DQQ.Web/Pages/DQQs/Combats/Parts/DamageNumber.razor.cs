using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ReheeCmf.Helpers;
using System.Collections.Concurrent;
using System.Data;

namespace DQQ.Web.Pages.DQQs.Combats.Parts
{
	public class DamageNumberPage : CombatPart
	{
		public int Top = -30;
		[Parameter]
		public string? Number { get; set; }

		[Parameter]
		public Guid? Id { get; set; }

		[Parameter]
		public DateTime? Date { get; set; }

		[Parameter]
		public EventCallback<Guid> OnRemove { get; set; }

		[Parameter]
		public bool IsHealing { get; set; }

		public bool Display { get; set; } = true;
		int count = 0;

		public string CssClass => $"floating-div {(IsHealing? "text-success" : "text-danger")}";

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			Task.Run(async () =>
			{
				await Task.Delay(1000);

				Display = false;
				StateHasChanged();
				OnRemove.InvokeAsync(Id.Value);

			});
		}
	}
}