

using DQQ.Commons.DTOs;
using DQQ.Enums;
using DQQ.Services.CombatServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Combats.Components
{
	public abstract class AbCombatPage : DQQPageBase
	{

		[Parameter]
		public Guid? ActorId { get; set; }

		[Inject]
		[NotNull]
		public ICombatService? combatService { get; set; }

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
			var request = new CombatRequestDTO
			{
				ActorId = ActorId,
				RandomGuid = CurrentGuid
			};
			var result = await combatService.RequestCombatRandom(request);
			Result = result?.Content;
			await Task.Delay(1000);
			StateHasChanged();
			if (result?.Content?.Success == true)
			{
				var pushRequest = await combatService.PushCombatRandom(request);
				if (pushRequest.Success != true)
				{
					Status = EnumCombatPlayStatus.Failed;
					return;
				}
				ParentRefreshEvent.InvokeEvent(this, null);
			}
			else
			{
				Status = EnumCombatPlayStatus.Failed;
				await Task.Delay(1000);
			}
			StateHasChanged();
		}
		protected override async Task OnDisposeAsync()
		{
			await base.OnDisposeAsync();
		}

		public async Task OnCombatPlayFinished()
		{
			if (IsDispose)
			{
				return;
			}
			Status = EnumCombatPlayStatus.FinishPlay;

			await StartCombat();
		}
	}
}
