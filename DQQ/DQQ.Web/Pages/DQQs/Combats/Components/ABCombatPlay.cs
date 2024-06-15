using BootstrapBlazor.Components;
using DQQ.Commons.DTOs;
using DQQ.Enums;
using DQQ.TickLogs;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Combats.Components
{
	public class ABCombatPlay : DQQPageBase
	{

		[Parameter]
		public EnumCombatPlayType PlayType { get; set; }

		[Parameter]
		public TickLogItem[]? CombatLog { get; set; }

		[Parameter]
		public CombatResultDTO? CombatResult { get; set; }

		[Parameter]
		public Func<Task>? AfterCombatPlay { get; set; }

		public int Tick { get; set; }
		public IEnumerable<TickLogActor>? Players { get; set; }
		public IEnumerable<TickLogActor>? Enemies { get; set; }

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

		public async Task KeepRefresh()
		{
			if (IsDispose)
			{
				return;
			}
			if (CombatLog?.Any() != true || boolPageIsLoad != true)
			{
				await Task.Delay(30);
				KeepRefresh();
				return;
			}
			var lastTick = CombatLog.OrderByDescending(b => b.ActionTick).Select(b => b.ActionTick).FirstOrDefault();
			if (lastTick == null)
			{
				return;
			}
			MessageItems.Clear();
			for (var i = 0; i <= lastTick; i++)
			{
				if (IsDispose)
				{
					return;
				}
				await Task.Delay(1000 / 30);

				var logs = CombatLog.Where(b => b.ActionTick == i).ToArray();
				foreach (var log in logs)
				{
					if (log.WaveNumber < 0)
					{
						continue;
					}
					Tick = log.WaveNumber;
					MessageItems.Add(log.GetConsoleMessage());
					if (log.LogType == Enums.EnumLogType.WaveChange)
					{

						Players = log.Players?.ToArray();
						Enemies = log.Enemies?.ToArray();
						StateHasChanged();
						await Task.Delay(1000);

					}
					else
					{
						Players = log.Players?.ToArray();
						Enemies = log.Enemies?.ToArray();
						StateHasChanged();
						if (Enemies?.All(b => b.PercentageHP <= 0) == true || Players?.All(b => b.PercentageHP <= 0) == true)
						{
							await Task.Delay(3000);
						}
						else
						{
							await Task.Delay(1000 / 30);
						}

					}


				}

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
