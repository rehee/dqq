using DQQ.Components.Stages.Actors.Characters;
using DQQ.Enums;
using DQQ.Services.ActorServices;
using DQQ.Web.Pages.DQQs.Skills;
using DQQ.Web.Pages.DQQs.Strategies;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Characters
{
  public class SelectedCharacterPage : DQQPageBase
  {
    [Inject]
    [NotNull]
    public ICharacterService? characterService { get; set; }

    [Parameter]
    public Guid? CharacterId { get; set; }

    [Parameter]
    public Character? Character { get; set; }

    [Parameter]
    public Func<Task>? CleanSelectedChar { get; set; }

    private void parentRefresh(object sender, EventArgs e)
    {
      Refresh();
    }

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();

      if (ParentRefreshEvent != null)
      {
        ParentRefreshEvent.Event += parentRefresh;
      }
    }
    public override async ValueTask DisposeAsync()
    {
      await base.DisposeAsync();
      if (ParentRefreshEvent != null)
      {
        ParentRefreshEvent.Event -= parentRefresh;
      }
    }
    
  
    

    protected override async Task OnParametersSetAsync()
    {
      await base.OnParametersSetAsync();
      await Refresh();
    }
    public async Task Refresh2()
    {
      await Refresh();
      ParentRefreshEvent?.InvokeEvent(this, null);
      StateHasChanged();
    }
    public async Task Refresh()
    {

      StateHasChanged();
    }
  }
}