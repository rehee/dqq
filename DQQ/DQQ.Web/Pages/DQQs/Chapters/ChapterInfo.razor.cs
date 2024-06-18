using DQQ.Profiles.Chapters;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Chapters
{
	public partial class ChapterInfo
	{
		[Parameter]
		public ChapterProfile? Profile { get; set; }
	}
}