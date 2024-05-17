using DQQ.Components.Stages.Actors.Characters;
using DQQ.Services.ActorServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Characters
{
  public class CharacterListPage : DQQPageBase
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
      await Task.CompletedTask;
      characterService.SelectedCharacter(Id);
      ParentRefreshEvent?.InvokeEvent(this, new EventArgs());
    }
  }
}