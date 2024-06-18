using static System.Net.Mime.MediaTypeNames;

namespace DQQ.Web.Pages.DQQs.Chapters.Components.Bodies.C0_0s
{
	public class C0_0Page : AbChapterBasePage
	{
		public override async Task ToEnd()
		{
			await base.ToEnd();
			await ChapterService.ProcessChapter(SelectedCharacter?.DisplayId);
			ParentRefreshEvent?.InvokeEvent(this, EventArgs.Empty);
			await OnSave.Close();
		}
	}
}