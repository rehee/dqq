using BootstrapBlazor.Components;
using DQQ.Entities;
using DQQ.Services.ItemServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Items
{
  public class ItemPickListPage : DQQPageBase
  {
    [Inject]
    [NotNull]
    public IItemService? itemService { get; set; }

    public IEnumerable<ItemEntity>? Items { get; set; }

    public HashSet<Guid>? ItemPicked { get; set; }
    [Parameter]
    public Guid? ActorId { get; set; }
    public async Task ItemPickSelected(CheckboxState state, Guid id)
    {
      try
      {
        if (state == CheckboxState.Checked)
        {
          ItemPicked!.Add(id);
        }
        else
        {
          ItemPicked!.Remove(id);
        }
      }
      catch
      {

      }

    }

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();

      if (ActorId != null)
      {
        Items = await itemService.PickableItems(ActorId.Value);

      }
      else
      {
        Items = null;
      }
      ItemPicked = new HashSet<Guid>();
      StateHasChanged();

    }

    public async override Task<bool> SaveFunction()
    {
      return (await itemService.PickItem(ActorId, ItemPicked.ToArray())).Success;
    }
  }
}