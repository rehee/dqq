
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
    public Character? SelectedCharacter { get; set; }
    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      UserId = auth.GetAuth()?.UserId;
      await RefreshPage();

      RefreshEvent = new EventParameter();
      RefreshEvent.Event += refreshEvent;
    }
    private void refreshEvent(object? sender, EventArgs e)
    {
      RefreshPage();
    }
    public async Task RefreshPage()
    {
      ActorId = characterService.GetSelectedCharacter();
      SelectedCharacter = await characterService.GetCharacter(ActorId);
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

    public async Task OpenReadMe()
    {

      await dialogService.ShowComponent<Readme>(null, "About DQQ");
    }
    public async Task OpenTips()
    {

      await dialogService.ShowComponent<Tips>(null, "Tips");
    }

    public override async ValueTask DisposeAsync()
    {
      await base.DisposeAsync();
      RefreshEvent.Event -= refreshEvent;
    }
  }
}