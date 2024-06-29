using DQQ.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace DQQ.Web.Services.LocalizationServices
{
	public class LocalizationService : ILocalizationService
	{
		private readonly IGameStatusService gameStatusService;
		private readonly NavigationManager nav;
		public const string LocalCultureKey = "localculture_key";

		public LocalizationService(IGameStatusService gameStatusService, NavigationManager nav)
		{
			this.gameStatusService = gameStatusService;
			this.nav = nav;
		}
		public async Task<string> LoadDefaulCulture()
		{
			var current = await gameStatusService.GetOrCreateGameStatus();
			var culture = current?.Content?.Culture;
			if (String.IsNullOrEmpty(culture))
			{
				culture = LocalizationHelper.GetAvaliableLang.Select(b => b.Value).FirstOrDefault();
			}
			if (CultureInfo.CurrentCulture.Name != culture)
			{
				await SetDefaulCulture(culture!);
			}

			return culture?? "";
		}

		public async Task SetDefaulCulture(string culture, bool reLoad = false)
		{
			var c = CultureInfo.GetCultureInfo(culture);
			var current = await gameStatusService.GetOrCreateGameStatus();
			if (current.Success)
			{
				current!.Content!.Culture = culture;
				await gameStatusService.UpdateGameStatus(current?.Content);
			}
			CultureInfo.CurrentCulture = c;
			CultureInfo.DefaultThreadCurrentCulture = c;
			CultureInfo.DefaultThreadCurrentUICulture = c;

			if (reLoad)
			{
				nav.NavigateTo("", true);
			}
		}
	}
}
