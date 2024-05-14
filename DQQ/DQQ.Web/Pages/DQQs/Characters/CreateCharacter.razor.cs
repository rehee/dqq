using DQQ.Components;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Services.ActorServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Characters
{
  public class CreateCharacterPage : DQQPageBase
  {
    public string? Name { get; set; }

    [Inject]
    [NotNull]
    public ICharacterService? characterService { get; set; }

    public override async Task<bool> SaveFunction()
    {
      if (String.IsNullOrEmpty(Name))
      {
        return false;
      }
      var chars = new Character
      {
        DisplayName = Name
      };
      var result = await characterService.CreateCharacter(chars);

      return result.Success;
    }
  }
}