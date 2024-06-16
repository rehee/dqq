using BootstrapBlazor.Components;
using DQQ.Web.Services.DQQAuthServices;
using DQQ.Web.Services.LocalizationServices;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Requests;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Layout
{
	public class MainLayoutPage : LayoutComponentBase
	{
		[Inject]
		[NotNull]
		public IGetRequestTokenService? tokenService { get; set; }

		[Inject]
		[NotNull]
		public IDQQAuth? auth { get; set; }

		[Inject]
		[NotNull]
		public NavigationManager? nav { get; set; }

		[Inject]
		[NotNull]
		public ILocalizationService? localizationService { get; set; }

		public async Task OnSelectedItemChanged(SelectedItem item)
		{
			await Task.CompletedTask;
			if (item.Value == CurrentLang)
			{
				return;
			}
			localizationService.SetDefaulCulture(item.Value, true);
			StateHasChanged();
		}

		public string? CurrentLang { get; set; }
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			CurrentLang = localizationService.LoadDefaulCulture();
			StateHasChanged();
			var isAuth = auth.IsAuth();
			if (isAuth)
			{
				if (tokenService is DQQGetRequestTokenService dqq)
				{
					var result = await dqq.CheckRequestTokenAsync();
					if (string.IsNullOrEmpty(result.token))
					{
						nav.NavigateTo("", true);
						return;
					}
				}
			}


		}
	}
}
