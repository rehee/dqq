namespace DQQ.Web.Services.LocalizationServices
{
	public interface ILocalizationService
	{
		string LoadDefaulCulture();
		void SetDefaulCulture(string culture, bool reLoad = false);
	}
}
