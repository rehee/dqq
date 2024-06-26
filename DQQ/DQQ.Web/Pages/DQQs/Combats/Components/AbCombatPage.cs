

using DQQ.Commons.DTOs;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Services.CombatServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace DQQ.Web.Pages.DQQs.Combats.Components
{
	public abstract class AbCombatPage : DQQPageBase
	{

		[Inject]
		[NotNull]
		public ICombatService? combatService { get; set; }

		[Parameter]
		public EnumMapNumber? MapNumber { get; set; }

		[Parameter]
		public bool StartCombatWhenInit {  get; set; }
		[Parameter]
		public bool ContinueCombat { get; set; }

		protected CombatRequestDTO? RequestDTO { get; set; }
		public CombatResultDTO? Result { get; set; }
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			if (StartCombatWhenInit)
			{
				if (ContinueCombat)
				{
					StartCombat(true);
				}
				else
				{
					StartCombat(false);
				}
			}
		}

		public EnumCombatPlayStatus Status { get; set; }

		public bool StatusSelectDisabled => !(Status == EnumCombatPlayStatus.NotSpecified || Status == EnumCombatPlayStatus.FinishPlay);
		public bool StartCombatDisabled => PlayType == EnumCombatPlayType.NotSpecified || Status != EnumCombatPlayStatus.NotSpecified;
		public bool StartSingleCombatDisabled => !(Status == EnumCombatPlayStatus.NotSpecified || (Status == EnumCombatPlayStatus.FinishPlay && !KeepCombat)) || KeepCombat;

		public bool SingleCombatEnabled => (Status == EnumCombatPlayStatus.NotSpecified || Status == EnumCombatPlayStatus.FinishPlay) && !KeepCombat;


		public EnumCombatPlayType PlayType { get; set; }


		public virtual async Task StopCombat()
		{
			await Task.CompletedTask;
			Status = EnumCombatPlayStatus.NotSpecified;
			KeepCombat = false;
			StateHasChanged();
		}
		protected Guid? CurrentGuid { get; set; }
		protected bool KeepCombat { get; set; }

		public virtual Task BackToMap()
		{
			nav.NavigateTo($"?{nameof(Home.WebPage)}={EnumWebPage.Map}");
			return Task.CompletedTask;

		}

		public virtual async Task StartCombat(bool keepCombate = false)
		{
			if (IsDispose)
			{
				return;
			}
			if (MapNumber == EnumMapNumber.None)
			{
				return;
			}
			KeepCombat = keepCombate;
			Result = null;
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
				RandomGuid = CurrentGuid,
				MapNumber = MapNumber ?? EnumMapNumber.None,
			};
			var result = await combatService.RequestCombatRandom(RequestDTO);
			Result = result?.Content;
			await Task.Delay(1000);
			StateHasChanged();
			if (result?.Success == true)
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
				StateHasChanged();

			}
			else
			{
				Status = EnumCombatPlayStatus.Failed;
				await Task.Delay(1000);

				if (KeepCombat)
				{
					Retry();
				}

			}
			StateHasChanged();


		}
		public int RetryTime = 0;
		public async Task Retry(int retryTime = 30, bool countOnly = false)
		{
			RetryTime = retryTime;
			while (RetryTime > 0)
			{
				if (IsDispose)
				{
					return;
				}
				await Task.Delay(1000);
				RetryTime--;
				StateHasChanged();
			}
			if (countOnly)
			{
				return;
			}
			if (KeepCombat)
			{
				StartCombat(KeepCombat);
			}

		}

		protected override async Task OnDisposeAsync()
		{
			await base.OnDisposeAsync();
			KeepCombat = false;
		}



		public async Task OnCombatPlayFinished()
		{
			if (IsDispose)
			{
				return;
			}
			Status = EnumCombatPlayStatus.FinishPlay;
			StateHasChanged();
			if (KeepCombat)
			{
				await Retry(10, true);
				StartCombat(KeepCombat);
			}

		}

		
	}
}
