using BootstrapBlazor.Components;
using DQQ.Services.ActorServices;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Requests;
using System.Diagnostics.CodeAnalysis;

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

		bool isDispose { get; set; }
		public virtual async ValueTask DisposeAsync()
		{
			await Task.CompletedTask;
			if (isDispose)
			{
				return;
			}
			isDispose = true;
			if (RefreshEvent != null)
			{
				RefreshEvent.Dispose();
				RefreshEvent = null;
			}

		}

		public virtual Task<bool> SaveFunction()
		{
			return Task.FromResult(true);
		}
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			if (OnSave != null)
			{
				OnSave.OnSaveFunc = SaveFunction;
			}

		}

	}
}
