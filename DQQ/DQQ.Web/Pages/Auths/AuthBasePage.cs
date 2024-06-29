using DQQ.Web.Enums;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.Auths
{
	public abstract class AuthBasePage: ComponentBase
	{
		[Parameter]
		public EventCallback<EnumPlayMode> OnPlayModeChange { get; set; }
		public abstract EnumPlayMode PlayMode { get; }
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			if (OnPlayModeChange.HasDelegate)
			{
				await OnPlayModeChange.InvokeAsync(PlayMode);
				StateHasChanged();
			}
		}
	}
}
