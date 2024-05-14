
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Services.ActorServices;
using DQQ.TickLogs;
using DQQ.Web.Pages.DQQs.Characters;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ReheeCmf.Commons.DTOs;
using ReheeCmf.Requests;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages
{
  public class HomePage : DQQPageBase
  {

    [Inject]
    [NotNull]
    public ICharacterService? characterService { get; set; }

    public IEnumerable<Character>? Characters { get; set; }
    public Guid? SelectedCharId { get; set; }
    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      await Refresh();
    }
    public async Task Refresh()
    {
      Characters = await characterService.GetAllCharacters();
      SelectedCharId = characterService.GetSelectedCharacter();
      StateHasChanged();
    }
    public async Task ShowCreate()
    {
      await dialogService.ShowComponent<CreateCharacter>(
        null, "", true, async save => await Refresh());
    }
    public async Task SelectCharacter(Guid? Id)
    {
      characterService.SelectedCharacter(Id);
      nav.NavigateTo("Character");
    }
  }
}