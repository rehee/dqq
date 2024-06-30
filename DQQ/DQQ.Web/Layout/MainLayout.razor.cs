using BootstrapBlazor.Components;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Services;
using DQQ.Services.ActorServices;
using DQQ.Web.Enums;
using DQQ.Web.Services.DQQAuthServices;
using DQQ.Web.Services.LocalizationServices;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Requests;
using System.Diagnostics.CodeAnalysis;
using TG.Blazor.IndexedDB;

namespace DQQ.Web.Layout
{
	public class MainLayoutPage : LayoutComponentBase
	{
		[Inject]
		[NotNull]
		public IGameStatusService? statusService { get; set; }
		public GameStatus? Status { get; set; }

		public async Task SetGamePlay(EnumPlayMode gamePlayType)
		{
			Status.PlayMode = gamePlayType;
			await statusService.UpdateGameStatus(b=> b.PlayMode = gamePlayType);
			
			StateHasChanged();
			await Task.CompletedTask;
		}

		public bool UseTabSet { get; set; } = false;

    public string Theme { get; set; } = "";

    public bool IsOpen { get; set; }

    public bool IsFixedHeader { get; set; } = true;

    public bool IsFixedFooter { get; set; } = true;

    public bool IsFullSide { get; set; } = false;

    public bool ShowFooter { get; set; } = true;
		[Inject]
		[NotNull]
		public IGetRequestTokenService? tokenService { get; set; }

		[Inject]
		[NotNull]
		public IDQQAuth? auth { get; set; }

		[Inject]
		[NotNull]
		public NavigationManager? nav { get; set; }

		[Inject]
		[NotNull]
		public ILocalizationService? localizationService { get; set; }

		public async Task OnSelectedItemChanged(SelectedItem item)
		{
			await Task.CompletedTask;
			if (item.Value == CurrentLang)
			{
				return;
			}
			await localizationService.SetDefaulCulture(item.Value, true);

			StateHasChanged();
		}

		public string? CurrentLang { get; set; }
		public string? AccountName { get; set; }
		public bool IsAuth { get; set; }
		public bool IsOffline => Status?.PlayMode == EnumPlayMode.Offline && !String.IsNullOrEmpty(Status?.OwnerId);
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			Status = (await statusService.GetOrCreateGameStatus()).Content;

			CurrentLang = await localizationService.LoadDefaulCulture();
			StateHasChanged();
			IsAuth = await auth.IsAuth();
			if (!IsAuth)
			{
				return;
			}
      if (tokenService is DQQGetRequestTokenService dqq)
      {
        var result = await dqq.CheckRequestTokenAsync();
        if (string.IsNullOrEmpty(result.token))
        {
          nav.NavigateTo("", true);
					IsAuth = false;
          return;
        }
				AccountName = result.name;

			}
		

    }

		[Inject]
		[NotNull]
		public ICharacterService? characterService { get; set; }

    public async Task RefreshPage()
    {
			var gameStatus = await statusService.GetOrCreateGameStatus();
      ActorId = gameStatus?.Content?.CurrentCharId;
      SelectedCharacter = await characterService.GetCharacter(ActorId);
			if (SelectedCharacter == null)
			{
				ActorId = null;
			}
			MenuItems = SelectedCharacter?.GenerateMenuItem(
				EnumWebPage.Home,
				EnumWebPage.Skills,
				EnumWebPage.Strategy,
				EnumWebPage.Map,
				EnumWebPage.Inventory,
				EnumWebPage.Setting
				) ?? [];
      StateHasChanged();
    }

    public Guid? ActorId { get; set; }
    public Character? SelectedCharacter { get; set; }
    public IEnumerable<MenuItem>? MenuItems { get; set; }

		public bool IsCharacterDetailOpen {  get; set; }
    public static string GetIconSvg(string iconName)
    {
      return $"data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxMDAiIGhlaWdodD0iMTAwIiB2aWV3Qm94PSIwIDAgMTAwIDEwMCI+PHJlY3Qgd2lkdGg9IjEwMCIgaGVpZ2h0PSIxMDAiIGZpbGw9IiNlMGUwZTAiIC8+PHRleHQgeD0iNTAiIHk9IjUwIiBmb250LXNpemU9IjMwIiB0ZXh0LWFuY2hvcj0ibWlkZGxlIiBmaWxsPSIjYjBiMGIwIj5BVkFUQVI8L3RleHQ+PC9zdmc+";
    }
  }
}
