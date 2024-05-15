using BootstrapBlazor.Components;
using DQQ.Services.ActorServices;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components;
using ReheeCmf.Requests;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages
{
  public class DQQPageBase : ComponentBase
  {
    [Inject]
    [NotNull]
    public ICharacterService? characterService { get; set; }
    [Inject]
    [NotNull]
    public NavigationManager? nav { get; set; }

    [Inject]
    [NotNull]
    public RequestClient<DQQGetHttpClient>? client { get; set; }

    [Inject]
    public DialogService? dialogService { get; set; }


    [Parameter]
    public OnSaveDTO? OnSave { get; set; }

    [Parameter]
    public Func<Task>? ParentRefresh { get; set; }

    public virtual Task<bool> SaveFunction()
    {
      return Task.FromResult(true);
    }
    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      if (OnSave != null)
      {
        OnSave.OnSaveFunc = SaveFunction;
      }

    }

  }
}
