namespace DQQ.Web.Pages.DQQs.Items
{
  public class ItemPagePage : DQQPageBase
  {

    public async Task ShowPickList()
    {
      await dialogService.ShowComponent<ItemPickList>(
        null, "", true);
    }
    public async Task ShowInventory()
    {
      await dialogService.ShowComponent<ActorInventory>(
        null, "");
    }
  }
}