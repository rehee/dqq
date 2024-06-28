using BootstrapBlazor.Components;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Chapters;
using DQQ.Services.ChapterServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Chapters
{
	public abstract class ChapterBasePage : DQQPageBase
	{
		[Inject]
		[NotNull]
		public IChapterService? ChapterService { get; set; }

		[Inject]
		[NotNull]
		public IComponentHtmlRenderer? Renderer { get; set; }
		public EnumChapter Chapter => SelectedCharacter?.Chapter ?? EnumChapter.None;
		public ChapterProfile? Profile => DQQPool.TryGet<ChapterProfile, EnumChapter>(Chapter);

		public async Task ShowComponent<T>() where T : DQQPageBase
		{
			await dialogService.ShowComponent<T>(
				new Dictionary<string, object?>
				{
					["SelectedCharacter"] = SelectedCharacter,
					["ParentRefreshEvent"] = ParentRefreshEvent,
				});
		}
		public virtual int StepIndex { get; set; }
		public virtual async Task Next()
		{
			await Task.Delay(1000);
			StepIndex++;
			StateHasChanged();
		}
		public virtual async Task ToEnd()
		{
			await Task.CompletedTask;
			await Task.Delay(1000);
		}
	}
}
