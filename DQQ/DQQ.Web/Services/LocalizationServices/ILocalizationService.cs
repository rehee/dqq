namespace DQQ.Web.Services.LocalizationServices
{
	public interface ILocalizationService
	{
		Task<string> LoadDefaulCulture();
		Task SetDefaulCulture(string culture, bool reLoad = false);
	}
}
