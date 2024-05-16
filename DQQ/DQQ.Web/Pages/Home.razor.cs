
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Services.ActorServices;
using DQQ.TickLogs;
using DQQ.Web.Pages.DQQs.Characters;
using DQQ.Web.Services.DQQAuthServices;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ReheeCmf.Commons.DTOs;
using ReheeCmf.Requests;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages
{
  public class HomePage : DQQPageBase
  {

    [Inject]
    [NotNull]
    public IDQQAuth? auth { get; set; }

    public string? UserId { get; set; }

    public Guid? ActorId { get; set; }
    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      UserId = auth.GetAuth()?.UserId;
      await RefreshPage();
      KeepRefresh();
      RefreshEvent = new EventParameter();
    }

    public async Task RefreshPage()
    {
      ActorId = characterService.GetSelectedCharacter();
      StateHasChanged();
      await Task.CompletedTask;

    }
    public async Task SelectCharacter()
    {
      await Task.CompletedTask;
      ActorId = null;
      characterService.SelectedCharacter(null);
      StateHasChanged();
    }
    public async Task KeepRefresh()
    {
      while (true)
      {
        await Task.Delay(1000);
        StateHasChanged();
      }
    }
    public async Task OpenReadMe()
    {

      await dialogService.ShowComponent<Readme>(null, "About DQQ");
    }
  }
}