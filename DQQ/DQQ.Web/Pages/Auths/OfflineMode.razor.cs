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
			var status = await GameStatusService.GetOrCreateGameStatus();
			status!.Content!.OwnerId = Guid.Empty.ToString();
			status!.Content!.PlayMode = EnumPlayMode.Offline;
			status!.Content!.Token = new TokenDTO();
			await GameStatusService.UpdateGameStatus(status?.Content);
			nav.NavigateTo("", true);

		}
	}
}