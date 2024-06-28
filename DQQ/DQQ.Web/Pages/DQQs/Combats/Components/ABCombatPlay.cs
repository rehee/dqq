using BootstrapBlazor.Components;
using DQQ.Commons.DTOs;
using DQQ.Consts;
using DQQ.Enums;
using DQQ.TickLogs;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Combats.Components
{
	public class CombatPlay : DQQPageBase
	{

		[Parameter]
		public EnumCombatPlayType PlayType { get; set; }

		[Parameter]
		public TickLogItem[]? CombatLog { get; set; }

		public int? MaxTick => CombatResult?.CombatTimeLimitationTick;
		public int CurrentActionTick {  get; set; }
		public double CurrentPlayProgress => CombatResult?.CombatTick > 0 ? (CurrentActionTick) * 100 / (double)CombatResult.CombatTick : 0;

		public DateTime? MapLimitation => CombatResult?.MapLimitation();


		[Parameter]
		public CombatResultDTO? CombatResult { get; set; }

		[Parameter]
		public Func<Task>? AfterCombatPlay { get; set; }

		public int Tick { get; set; }
		public List<TickLogActor>? Players { get; set; }
		public List<TickLogActor>? Enemies { get; set; }

		protected bool boolPageIsLoad { get; set; }

		public List<ConsoleMessageItem> MessageItems { get; set; } = new List<ConsoleMessageItem>();
		protected async override void OnAfterRender(bool firstRender)
		{
			base.OnAfterRender(firstRender);
			if (firstRender)
			{
				boolPageIsLoad = true;
			}
		}

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			switch (PlayType)
			{
				case EnumCombatPlayType.Summary:
					RushSummary();
					break;
				case EnumCombatPlayType.Simple:
				case EnumCombatPlayType.Detail:
				case EnumCombatPlayType.Talbe:
					KeepRefresh();
					break;
			}

		}


		public int TotalSecond { get; set; }
		public async Task RushSummary()
		{
			if (IsDispose)
			{
				return;
			}
			if (CombatLog?.Any() != true || boolPageIsLoad != true)
			{
				await Task.Delay(30);
				RushSummary();
				return;
			}
			TotalSecond = Convert.ToInt32((CombatResult.CombatTime - new DateTime()).TotalSeconds) + 10;

			while (TotalSecond >= 0)
			{
				await Task.Delay(1000);
				TotalSecond--;
				StateHasChanged();
				if (IsDispose)
				{
					return;
				}
			}

			if (AfterCombatPlay != null)
			{
				AfterCombatPlay!();
			}

		}

		public TickLogItem? CurrentLog { get; set; }
		public async Task KeepRefresh()
		{
			if (IsDispose)
			{
				return;
			}
			if (CombatResult== null || boolPageIsLoad != true)
			{
				await Task.Delay(30);
				KeepRefresh();
				return;
			}
			var lastTick = CombatResult?.Timelines?.OrderByDescending(b=>b.ActionTick).Select(b=>b.ActionTick).FirstOrDefault() ??
				CombatLog.OrderByDescending(b => b.ActionTick).Select(b => b.ActionTick).FirstOrDefault();
			if (lastTick == null)
			{
				return;
			}
			MessageItems.Clear();
			var basicDelayTime = 1000 / DQQGeneral.TickPerSecond;
			for (var i = 0; i <= lastTick; i++)
			{
				if (IsDispose)
				{
					return;
				}
				CurrentActionTick = i;
				
				
				
				
				var logs = CombatLog.Where(b => b.ActionTick == i).ToArray();
				var timeLineStart = CombatResult?.Timelines?.Where(b => b.ActionTick == i && b.IsStart).FirstOrDefault();
				var timeLineEnd = CombatResult?.Timelines?.Where(b => b.ActionTick == i && !b.IsStart).FirstOrDefault();
				
				var totaDisplayPerTick = Math.Max(1, (logs?.Count() ?? 0) + (timeLineStart!=null?1:0) + (timeLineEnd != null ? 1 : 0));
				var thisTickDelay = totaDisplayPerTick<=0 ? basicDelayTime : basicDelayTime / totaDisplayPerTick;
				if (timeLineStart != null)
				{
					await Task.Delay(thisTickDelay);
					Players = timeLineStart?.Players?.ToList();
					Enemies = timeLineStart?.Enemies?.ToList();
					//StateHasChanged();
					
				}
				if (logs?.Any() == true)
				{
					foreach (var log in logs)
					{
						await Task.Delay(thisTickDelay);
						CurrentLog = log;

						//await Task.Delay(1000 / DQQGeneral.TickPerSecond / 2);

						if (log.WaveNumber < 0)
						{
							continue;
						}
						Tick = log.WaveNumber;
						MessageItems.Add(log.GetConsoleMessage());

						if (log.LogType == Enums.EnumLogType.WaveChange)
						{
							//if (timeLineStart == null)
							//{
							//	//Players = log.Players?.ToList();
							//	//Enemies = log.Enemies?.ToList();
							//	StateHasChanged();
							//}
							await Task.Delay(3000);
							StateHasChanged();
							
						}
						else
						{
							if (Enemies?.All(b => b.PercentageHP <= 0) == true || Players?.All(b => b.PercentageHP <= 0) == true)
							{
								await Task.Delay(3000);
								StateHasChanged();
							}
							else
							{
								StateHasChanged();
								//await Task.Delay(1000 / 30);
							}

						}


					}
				}
				
			
				if (timeLineEnd != null)
				{
					await Task.Delay(thisTickDelay);
					Players = timeLineEnd?.Players?.ToList();
					Enemies = timeLineEnd?.Enemies?.ToList();
					StateHasChanged();
					
				}
				//if (Enemies?.All(b => b.PercentageHP <= 0) == true || Players?.All(b => b.PercentageHP <= 0) == true)
				//{
				//	await Task.Delay(1500);
				//}
				continue;
			}
			if (AfterCombatPlay != null)
			{
				AfterCombatPlay!();
			}
		}

		protected override async Task OnDisposeAsync()
		{
			await base.OnDisposeAsync();
			AfterCombatPlay = null;
		}
	}
}
