
using BootstrapBlazor.Components;
using DQQ.Components.Items;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Entities;
using DQQ.Enums;
using DQQ.Pools;
using DQQ.Profiles.Skills;
using DQQ.Services.ActorServices;
using DQQ.Services.ItemServices;
using DQQ.TickLogs;
using DQQ.Web.Layout;
using DQQ.Web.Pages.DQQs.Builds.Components;
using DQQ.Web.Pages.DQQs.Characters;
using DQQ.Web.Resources;
using DQQ.Web.Services.DQQAuthServices;
using DQQ.Web.Services.ItemServices;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using ReheeCmf.Commons.DTOs;
using ReheeCmf.Requests;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages
{
	public class HomePage : DQQPageBase
	{
		
		public EnumWebPage? WebPage { get; set; }

   

    public bool StartCombat { get; set; }
		public bool KeepCombat {  get; set; }
		public async Task OnJoinMap((bool, bool) joinCombat)
		{
			StartCombat = joinCombat.Item1;
			KeepCombat= joinCombat.Item2;
			await ChangePage(EnumWebPage.Combat);
		}

		public async Task ChangePage(EnumWebPage webPage)
		{
			await Task.CompletedTask;
			WebPage = webPage;
			StateHasChanged();
		}

		[Inject]
		[NotNull]
		public IDQQAuth? auth { get; set; }

		public string? UserId { get; set; }
    void GetQueryStringValues()
    {
      nav.TryGetQueryString<EnumWebPage>("WebPage", out var currentCount);
			WebPage = currentCount;
			StateHasChanged();
    }
    void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
    {
      GetQueryStringValues();
      StateHasChanged();
    }

    [Inject]
		[NotNull]
		public IComponentHtmlRenderer? ComponentHtmlRenderer { get; set; }
    [CascadingParameter]
    public MainLayoutPage? Layout { get; set; }

    protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			nav.LocationChanged += HandleLocationChanged;
			GetQueryStringValues();
			UserId = auth.GetAuth()?.UserId;
			

			RefreshEvent = new EventParameter();
			RefreshEvent.Event += refreshEvent;

			foreach (var profile in DQQPool.SkillPool.Select(b => b.Value))
			{
				profile.ExtureDiscription = await ComponentHtmlRenderer.RenderAsync<SkillDetailPage>(
					new Dictionary<string, object?>
					{
						["Profile"] = profile
					});
			}
			foreach (var profile in DQQPool.MobPool.Select(b => b.Value))
			{
				if (ResourceMapping.MobResourceTypes.TryGetValue(profile.ProfileNumber, out var resourceType))
				{
					profile.ExtureDiscription = await ComponentHtmlRenderer.RenderAsync(resourceType
					//new Dictionary<string, object?>
					//{
					//	["Profile"] = profile
					//}
					);
				}

			}
			await RefreshPage();

    }
		private void refreshEvent(object? sender, EventArgs e)
		{
			RefreshPage();
		}
		public async Task RefreshPage()
		{
			if(Layout== null)
			{
				return;
			}
			await Layout.RefreshPage();


			StateHasChanged();
    }
    public Character? ThisSelectedCharacter => Layout?.SelectedCharacter;
		public Guid?	ThisActorId=> Layout?.ActorId;
		public async Task SelectCharacter()
		{
			await Task.CompletedTask;
      Layout!.ActorId = null;
      Layout!.SelectedCharacter = null;
      characterService.SelectedCharacter(null);
			StateHasChanged();
		}

		public async Task OpenReadMe()
		{

			await dialogService.ShowComponent<Readme>(null, "About DQQ");
		}
		public async Task OpenTips()
		{

			await dialogService.ShowComponent<Tips>(null, "Tips");
		}
		public EnumMapNumber? SelectedMap { get; set; }

		public Task OnMapSelected(EnumMapNumber? map)
		{
			SelectedMap = map;
			StateHasChanged();
			return Task.CompletedTask;
		}

    protected override async Task OnDisposeAsync()
    {
      await base.OnDisposeAsync();
      RefreshEvent.Event -= refreshEvent;
      nav.LocationChanged -= HandleLocationChanged;
    }

    public ItemEntity[]? CharacterInventory { get; set; }


		[Inject]
		[NotNull]
		public IItemService? ItemService { get; set; }
		public async Task QueryInventory()
		{
			CharacterInventory = (await ItemService.ActorInventory(ThisSelectedCharacter?.DisplayId))?.ToArray();
		}
	}
}