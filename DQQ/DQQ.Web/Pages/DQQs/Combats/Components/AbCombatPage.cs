﻿

using DQQ.Commons.DTOs;
using DQQ.Enums;
using DQQ.Services.CombatServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace DQQ.Web.Pages.DQQs.Combats.Components
{
	public abstract class AbCombatPage : DQQPageBase
	{

		[Parameter]
		public Guid? ActorId { get; set; }

		[Inject]
		[NotNull]
		public ICombatService? combatService { get; set; }

		protected CombatRequestDTO? RequestDTO { get; set; }
		public CombatResultDTO? Result { get; set; }
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

		}

		public EnumCombatPlayStatus Status { get; set; }

		public bool StatusSelectDisabled => !(Status == EnumCombatPlayStatus.NotSpecified || Status == EnumCombatPlayStatus.FinishPlay);
		public bool StartCombatDisabled => PlayType == EnumCombatPlayType.NotSpecified || Status != EnumCombatPlayStatus.NotSpecified;
		public EnumCombatPlayType PlayType { get; set; }


		public virtual async Task StopCombat()
		{
			await Task.CompletedTask;
			Status = EnumCombatPlayStatus.NotSpecified;
			StateHasChanged();
		}
		protected Guid? CurrentGuid { get; set; }
		public virtual async Task StartCombat()
		{
			if (IsDispose)
			{
				return;
			}
			CurrentGuid = Guid.NewGuid();
			Status = EnumCombatPlayStatus.Waiting;
			StateHasChanged();
			await Task.Delay(1000);
			Status = EnumCombatPlayStatus.Searcing;
			StateHasChanged();
			await Task.Delay(1000);
			Status = EnumCombatPlayStatus.CreateMap;
			StateHasChanged();
			await Task.Delay(1000);

			Status = EnumCombatPlayStatus.Playing;
			await Task.Delay(1000);
			RequestDTO = new CombatRequestDTO
			{
				ActorId = ActorId,
				RandomGuid = CurrentGuid
			};
			var result = await combatService.RequestCombatRandom(RequestDTO);
			Result = result?.Content;
			await Task.Delay(1000);
			StateHasChanged();
			if (result?.Success == true)
			{
				if (PlayType == EnumCombatPlayType.Summary)
				{
					await FinishProcess();
				}

			}
			else
			{
				Status = EnumCombatPlayStatus.Failed;
				await Task.Delay(1000);
				Retry();
			}


			StateHasChanged();
		}
		public int RetryTime = 0;
		public async Task Retry(int retryTime = 30)
		{
			RetryTime = retryTime;
			while (RetryTime > 1)
			{
				if (IsDispose)
				{
					return;
				}
				await Task.Delay(1000);
				RetryTime--;
				StateHasChanged();
			}
			StartCombat();
		}

		protected override async Task OnDisposeAsync()
		{
			await base.OnDisposeAsync();
		}

		protected async Task FinishProcess()
		{
			Status = EnumCombatPlayStatus.FinishPlay;
			StateHasChanged();
			await Task.Delay(1000);
			if (Result?.Success == true)
			{
				var pushRequest = await combatService.PushCombatRandom(RequestDTO);
				if (pushRequest.Success != true)
				{
					Status = EnumCombatPlayStatus.Failed;
					StateHasChanged();
					Retry();
					return;
				}
				ParentRefreshEvent.InvokeEvent(this, null);
			}
			StateHasChanged();
			await Task.Delay(1000);
		}

		public async Task OnCombatPlayFinished()
		{
			if (IsDispose)
			{
				return;
			}
			if (PlayType != EnumCombatPlayType.Summary)
			{
				await FinishProcess();
			}
			await StartCombat();
		}
	}
}
