
namespace DQQ.Web.Pages
{
  public class TestPagePage : DQQPageBase
  {

    public override async Task<bool> SaveFunction()
    {
      await Task.CompletedTask;
      return true;
    }
  }
}