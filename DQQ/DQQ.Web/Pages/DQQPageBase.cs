using BootstrapBlazor.Components;
using DQQ.Services.ActorServices;
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
		public DialogService? dialogService { get; set; }

		[Inject]
		[NotNull]
		public IStringLocalizer? Localizer { get; set; }

		[Inject]
		[NotNull]
		public ILocalStorageService? LocalStorageService { get; set; }

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
			//CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("zh");
			if (OnSave != null)
			{
				OnSave.OnSaveFunc = SaveFunction;
			}

		}

	}
}
