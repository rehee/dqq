using DQQ.Web.Resources;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Chapters
{
	public class ChapterBodyPage : ChapterBasePage
	{
		public string? BodyComponent { get; set; }
		public RenderFragment? BodyFragement { get; set; }
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

		}

		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();
			if (ParentRefreshEvent == null)
			{
				return;
			}
			var relatedType = Chapter.GetChapterResourceTypes();
			if (relatedType != null)
			{
				BodyFragement = RenderService.RenderComponent(relatedType, new Dictionary<string, object?>()
				{
					["SelectedCharacter"] = SelectedCharacter,
					["ParentRefreshEvent"] = ParentRefreshEvent,
				});
			}
			StateHasChanged();
		}
	}
}