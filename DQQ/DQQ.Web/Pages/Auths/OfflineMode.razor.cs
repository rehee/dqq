using BootstrapBlazor.Components;
using DQQ.Services;
using DQQ.Web.Enums;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Commons.DTOs;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.Auths
{
	public class OfflineModePage: AuthBasePage
	{
		public override EnumPlayMode PlayMode => EnumPlayMode.Offline;

		[Inject]
		[NotNull]
		public IGameStatusService? GameStatusService { get; set; }
		[Inject]
		[NotNull]
		public NavigationManager? nav { get; set; }
		public async Task OfflineLogin()
		{
			
			await GameStatusService.UpdateGameStatus(b =>
			{
				b.OwnerId = Guid.Empty.ToString();
				b.PlayMode = EnumPlayMode.Offline;
				b.Token = new TokenDTO();
			});
			nav.NavigateTo("", true);

		}
	}
}