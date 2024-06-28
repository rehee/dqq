using Blazor.Serialization.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Combats.Components
{
	public class CombatCoCos2Page : CombatPlay
	{

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			RunnintCocosGame();
		}
		public bool DisplayCocos { get; set; }
		public async Task RunnintCocosGame()
		{
			if (IsDispose)
			{
				return;
			}
			if (CombatLog?.Any() != true || boolPageIsLoad != true)
			{
				await Task.Delay(30);
				RunnintCocosGame();
				return;
			}
			DisplayCocos = true;
			StateHasChanged();
			CheckRunning();

			while (DisplayCocos)
			{
				if (IsDispose)
				{
					return;
				}
				await Task.Delay(1000);
				try
				{
					DisplayCocos = !(await jsRunningTime.InvokeAsync<bool>("IsCoCosFinish"));
				}
				catch
				{

				}
				
			}
			DisplayCocos = false;
			TotalSecond = 10;
			StateHasChanged();
			while (TotalSecond > 0)
			{
				await Task.Delay(1000);
				TotalSecond--;
				StateHasChanged();
			}
			if (AfterCombatPlay != null)
			{
				AfterCombatPlay!();
			}

		}

		[Inject]
		[NotNull]
		public IJSRuntime? jsRunningTime { get; set; }
		private async Task SendDataToGame()
		{
			var json = CombatResult?.ToJson();
			await jsRunningTime.InvokeVoidAsync("receiveDataFromBlazor", json ?? "");
		}

		protected override void OnAfterRender(bool firstRender)
		{
			base.OnAfterRender(firstRender);
			if (firstRender)
			{
				CheckRunning();
			}
		}
		bool IsRunning = false;
		private async Task CheckRunning()
		{
			if (IsRunning)
			{
				return;
			}
			IsRunning = true;
			var count = 100;
			for (var i = 0; i < count; i++)
			{
				try
				{
					var result = await jsRunningTime.InvokeAsync<bool>("IsCoCosRunning");
					if (result)
					{
						await SendDataToGame();
						return;
					}
				}
				catch
				{

				}

				await Task.Delay(1000);
			}

		}
	}
}
