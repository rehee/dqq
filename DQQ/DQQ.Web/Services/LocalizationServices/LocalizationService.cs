using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace DQQ.Web.Services.LocalizationServices
{
	public class LocalizationService : ILocalizationService
	{
		private readonly ILocalStorageService localStorage;
		private readonly NavigationManager nav;
		public const string LocalCultureKey = "localculture_key";

		public LocalizationService(ILocalStorageService localStorage, NavigationManager nav)
		{
			this.localStorage = localStorage;
			this.nav = nav;
		}
		public string LoadDefaulCulture()
		{
			var culture = localStorage.GetItem<String>(LocalCultureKey);
			if (String.IsNullOrEmpty(culture))
			{
				culture = LocalizationHelper.GetAvaliableLang.Select(b => b.Value).FirstOrDefault();
			}
			if (CultureInfo.CurrentCulture.Name != culture)
			{
				SetDefaulCulture(culture!);
			}

			return culture;
		}

		public void SetDefaulCulture(string culture, bool reLoad = false)
		{
			var c = CultureInfo.GetCultureInfo(culture);
			localStorage.SetItem<string>(LocalCultureKey, culture);

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
