using BootstrapBlazor.Components;
using DQQ.Enums;
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
		public EventCallback<Guid> OnRemove { get; set; }

		[Parameter]
		public TextFloatParameter? Damage { get; set; }

		public bool Display { get; set; } = true;
		int count = 0;

		public string CssClass
		{
			get
			{
				switch (Damage?.AnimationType)
				{
					case EnumAnimationType.Slash:
						return "animation_base animated-slash";
					case EnumAnimationType.Slash2:
						return "animation_base animated-slash2";
				}
				return $"{(Damage?.Color == Color.Warning ? "floating-hold-div" : "floating-div")} {(Damage?.Color == Color.Warning ? "text-warning" : Damage?.Color == Color.Success ? "text-success" : "text-danger")}";
			}
		}

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			Task.Run(async () =>
			{
				await Task.Delay(1000);

				Display = false;
				StateHasChanged();
				OnRemove.InvokeAsync(Damage!.Value.Id);

			});
		}
	}
}