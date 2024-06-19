using DQQ.Profiles.Chapters;
using DQQ.Web.Resources;
using DQQ.Web.Services.RenderServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Chapters
{
	public partial class ChapterInfo
	{
		[Parameter]
		public ChapterProfile? Profile { get; set; }

		[Inject]
		[NotNull]
		IRenderService? RenderService { get; set; }

		RenderFragment? Fragment { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			
		}

		protected override async Task OnParametersSetAsync()
		{
			await base.OnParametersSetAsync();
			var type = Profile?.ProfileNumber.GetChapterInfoTypes();
			if (type != null)
			{
				Fragment = RenderService.RenderComponent(type, null);
			}
		}
	}
}