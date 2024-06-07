using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Pages.DQQs.Items
{
  public class ItemPagePage : DQQPageBase
  {
    [Parameter]
    public Guid? ActorId { get; set; }
    public async Task ShowPickList()
    {
      await dialogService.ShowComponent<ItemPickList>(
        new Dictionary<string, object?>
        {
          ["ActorId"] = ActorId,
        }, "");
    }
    public async Task ShowInventory()
    {
      await dialogService.ShowComponent<ActorInventory>(
        new Dictionary<string, object?>
        {
          ["ActorId"] = ActorId,
          ["ParentRefreshEvent"] = ParentRefreshEvent
        }, "");
    }
  }
}