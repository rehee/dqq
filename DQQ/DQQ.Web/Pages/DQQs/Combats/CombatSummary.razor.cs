
using BootstrapBlazor.Components;
using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors;
using DQQ.Services.ActorServices;
using DQQ.Services.CombatServices;
using DQQ.Web.Pages.DQQs.Combats.Components;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Responses;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Combats
{
	public class CombatSummaryPage : DQQPageBase
	{

		public CombatResultDTO? Result { get; set; }
		public bool IsCombat = false;

		[Inject]
		[NotNull]
		public ICombatService? combatService { get; set; }
		[Inject]
		[NotNull]
		private SwalService? SwalService { get; set; }
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
		}
		[Parameter]
		public Guid? ActorId { get; set; }
		public async Task CombatRequest()
		{
			//await dialogService.ShowComponent<CombatRequest>(new Dictionary<string, object?>
			//{
			//  ["ActorId"] = ActorId
			//}, null, true, async (f) =>
			//{
			//  if (f.ResultValue is ContentResponse<CombatResultDTO?> rv)
			//  {
			//    Result = rv.Content;
			//  }
			//  StateHasChanged();
			//});
			if (Clicked == true)
			{
				return;
			}
			Clicked = true;
			Task.Run(async () =>
			{
				await Task.Delay(3000);
				Clicked = false;
				StateHasChanged();
			});
			var result = await combatService.RequestCombat(new Commons.DTOs.CombatRequestDTO
			{
				ActorId = ActorId,
				MapLevel = 0,
				SubMapLevel = 0
			});
			if (!result.Success)
			{
				var op = new SwalOption()
				{
					Category = SwalCategory.Error,
					Title = "进入战斗失败",
					Content = "进入战斗失败. 请稍等片刻继续尝试",
					ShowClose = true
				};
				await SwalService.Show(op);
				return;
			}
			ParentRefreshEvent.InvokeEvent(this, null);
			Result = result.Content;
			StateHasChanged();
			await dialogService.ShowComponent<CombatPlayRush>(
				new Dictionary<string, object?>
				{
					["CombatLog"] = Result?.Logs,
					["CombatResult"] = Result
				}

				, null, false, async (f) =>
				{
					if (f.ResultValue is ContentResponse<CombatResultDTO?> rv)
					{
						Result = rv.Content;
					}
					StateHasChanged();
				});
		}
		public bool Clicked { get; set; }
		public async Task CombatRequest2()
		{
			if (Clicked == true)
			{
				return;
			}
			Clicked = true;
			Task.Run(async () =>
			{
				await Task.Delay(3000);
				Clicked = false;
				StateHasChanged();
			});
			var result = await combatService.RequestCombat(new Commons.DTOs.CombatRequestDTO
			{
				ActorId = ActorId,
				MapLevel = 0,
				SubMapLevel = 0
			});
			if (!result.Success)
			{
				var op = new SwalOption()
				{
					Category = SwalCategory.Error,
					Title = "进入战斗失败",
					Content = "进入战斗失败. 请稍等片刻继续尝试",
					ShowClose = true
				};
				await SwalService.Show(op);
				return;
			}
			ParentRefreshEvent.InvokeEvent(this, null);
			Result = result.Content;
			StateHasChanged();
			await dialogService.ShowComponent<CombatPlay>(
				new Dictionary<string, object?>
				{
					["CombatLog"] = Result?.Logs
				}

				, null, false, async (f) =>
			{
				if (f.ResultValue is ContentResponse<CombatResultDTO?> rv)
				{
					Result = rv.Content;
				}
				StateHasChanged();
			});
		}
		public async Task CombatRequest3()
		{
			if (Clicked == true)
			{
				return;
			}
			Clicked = true;
			Task.Run(async () =>
			{
				await Task.Delay(3000);
				Clicked = false;
				StateHasChanged();
			});
			var result = await combatService.RequestCombat(new Commons.DTOs.CombatRequestDTO
			{
				ActorId = ActorId,
				MapLevel = 0,
				SubMapLevel = 0
			});
			if (!result.Success)
			{
				var op = new SwalOption()
				{
					Category = SwalCategory.Error,
					Title = "进入战斗失败",
					Content = "进入战斗失败. 请稍等片刻继续尝试",
					ShowClose = true
				};
				await SwalService.Show(op);
				return;
			}
			ParentRefreshEvent.InvokeEvent(this, null);
			Result = result.Content;
			StateHasChanged();
			await dialogService.ShowComponent<CombatPlay2>(
				new Dictionary<string, object?>
				{
					["CombatLog"] = Result?.Logs
				}

				, null, false, async (f) =>
				{
					if (f.ResultValue is ContentResponse<CombatResultDTO?> rv)
					{
						Result = rv.Content;
					}
					StateHasChanged();
				});
		}
		public async Task CombatRequest4()
		{
			if (Clicked == true)
			{
				return;
			}
			Clicked = true;
			Task.Run(async () =>
			{
				await Task.Delay(3000);
				Clicked = false;
				StateHasChanged();
			});
			var result = await combatService.RequestCombat(new Commons.DTOs.CombatRequestDTO
			{
				ActorId = ActorId,
				MapLevel = 0,
				SubMapLevel = 0
			});
			if (!result.Success)
			{
				var op = new SwalOption()
				{
					Category = SwalCategory.Error,
					Title = "进入战斗失败",
					Content = "进入战斗失败. 请稍等片刻继续尝试",
					ShowClose = true
				};
				await SwalService.Show(op);
				return;
			}
			ParentRefreshEvent.InvokeEvent(this, null);
			Result = result.Content;
			StateHasChanged();
			await dialogService.ShowComponent<CombatCoCos>(
				new Dictionary<string, object?>
				{
					["CombatResult"] = Result
				}

				, null, false, async (f) =>
				{
					if (f.ResultValue is ContentResponse<CombatResultDTO?> rv)
					{
						Result = rv.Content;
					}
					StateHasChanged();
				});
		}
		public async Task CombatRequest5()
		{
			if (Clicked == true)
			{
				return;
			}
			Clicked = true;
			Task.Run(async () =>
			{
				await Task.Delay(3000);
				Clicked = false;
				StateHasChanged();
			});
			ParentRefreshEvent.InvokeEvent(this, null);
			StateHasChanged();
			await dialogService.ShowComponent<CombatPlayList>(
				new Dictionary<string, object?>
				{
					["ActorId"] = ActorId,
					["ParentRefreshEvent"] = ParentRefreshEvent,

				}
				, null, false, size: Size.ExtraExtraLarge);
		}
		public async Task CombatLog()
		{
			await dialogService.ShowComponent<CombatLog>(
				new Dictionary<string, object?>
				{
					["CombatLog"] = Result?.Logs
				}
				, null, false);
		}
	}
}