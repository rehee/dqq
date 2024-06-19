using BootstrapBlazor.Components;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Chapters;
using DQQ.Services.ChapterServices;
using DQQ.Web.Pages;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Resources.Chapters
{
	public abstract class ChapterComponentBase : DQQPageBase
	{
		[Parameter]
		public Character? SelectedCharacter { get; set; }
		[Inject]
		[NotNull]
		public IChapterService? ChapterService { get; set; }

		[Inject]
		[NotNull]
		public IComponentHtmlRenderer? Renderer { get; set; }
		
		
		public async Task ShowComponent<C>() where C : DQQPageBase
		{
			await dialogService.ShowComponent<C>(
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
	public abstract class ChapterPageBase : ChapterComponentBase
	{
		public ChapterProfile? Profile => DQQPool.TryGet<ChapterProfile, EnumChapter>(Chapter);
		public abstract EnumChapter Chapter { get; }
	}
	public abstract class ChapterInfoBase : ChapterComponentBase
	{
		public ChapterProfile? Profile => DQQPool.TryGet<ChapterProfile, EnumChapter>(Chapter);
		public abstract EnumChapter Chapter { get; }
	}
}
