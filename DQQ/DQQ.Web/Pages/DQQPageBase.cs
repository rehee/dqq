using BootstrapBlazor.Components;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Services;
using DQQ.Services.ActorServices;
using DQQ.Web.Enums;
using DQQ.Web.Services.RenderServices;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using ReheeCmf.Requests;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DQQ.Web.Pages
{
	public class DQQPageBase : ComponentBase, IAsyncDisposable
	{
		public EnumPlayMode PlayMode { get; set; }
		public Task OnPlayModeChange(EnumPlayMode playMode)
		{
			PlayMode= playMode;
			StateHasChanged();
			return Task.CompletedTask;
		}


		[Inject]
		[NotNull]
		public ICharacterService? characterService { get; set; }
		[Inject]
		[NotNull]
		public NavigationManager? nav { get; set; }

		[Inject]
		[NotNull]
		public RequestClient<DQQGetHttpClient>? client { get; set; }

		[Inject]
		[NotNull]
		public IRenderService? RenderService { get; set; }

		[Inject]
		public DialogService? dialogService { get; set; }

		[Inject]
		[NotNull]
		public IStringLocalizer? Localizer { get; set; }

		[Inject]
		[NotNull]
		public IGameStatusService? GameStatusService { get; set; }

		public virtual async Task CloseDynamicDialog()
		{
			if (OnSave?.DynamicDialogOption != null)
			{
				await OnSave!.DynamicDialogOption!.CloseDialogAsync();
			}
			return;
		}

		[Parameter]
		public OnSaveDTO? OnSave { get; set; }

		[Parameter]
		public Func<Task>? ParentRefresh { get; set; }

		[Parameter]
		public EventParameter? ParentRefreshEvent { get; set; }

		public EventParameter? RefreshEvent { get; set; }
		[Parameter]
		public Guid? ActorId { get; set; }

		[Parameter]
		public Character? SelectedCharacter { get; set; }

		[Parameter]
		public Guid? ParentGuid { get; set; }

		protected bool IsDispose { get; set; }

		protected virtual Task OnDisposeAsync()
		{
			return Task.CompletedTask;
		}
		public virtual async ValueTask DisposeAsync()
		{
			await Task.CompletedTask;
			if (IsDispose)
			{
				return;
			}
			IsDispose = true;
			if (RefreshEvent != null)
			{
				RefreshEvent.Dispose();
				RefreshEvent = null;
			}
			await OnDisposeAsync();

		}

		public virtual Task<bool> SaveFunction()
		{
			return Task.FromResult(true);
		}
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

			CultureInfo.CurrentCulture = new CultureInfo("zh");
			if (OnSave != null)
			{
				OnSave.OnSaveFunc = SaveFunction;
			}

		}
		public BreakPoint BreakPoint { get; set; }
		public bool IsSmall => BreakPoint == BreakPoint.Small || BreakPoint == BreakPoint.ExtraSmall;
		public async Task BreakPointChanged(BreakPoint bK)
		{
			await Task.CompletedTask;
			BreakPoint = bK;
			StateHasChanged();
		}
	}
}
