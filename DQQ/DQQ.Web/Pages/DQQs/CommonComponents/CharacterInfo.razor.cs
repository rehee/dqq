namespace DQQ.Web.Pages.DQQs.CommonComponents
{
	public class CharacterInfoPage: DQQPageBase
	{
		public bool IsBackdropOpen {  get; set; }

		public Task OpenDrawer()
		{
			IsBackdropOpen = !IsBackdropOpen;
			StateHasChanged();
			return Task.CompletedTask;
		}
	}
}