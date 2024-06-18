using BootstrapBlazor.Components;
using DQQ.Profiles.Skills;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Builds.Components
{
	public partial class SkillDetailPage
	{

		[Parameter]
		public SkillProfile? Profile { get; set; }

		[Parameter]
		public Color Color { get; set; } = Color.Dark;
	}
}