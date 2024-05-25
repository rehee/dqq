using Blazor.Serialization.Extensions;
using DQQ.Commons.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Pages.DQQs.Combats
{
  public class CombatCoCosPage : DQQPageBase
  {
    [Parameter]
    public CombatResultDTO? CombatResult { get; set; }
    [Inject]
    [NotNull]
    public IJSRuntime? jsRunningTime { get; set; }
    private async Task SendDataToGame()
    {
      var json = CombatResult?.ToJson();
      Console.WriteLine(json);
      await jsRunningTime.InvokeVoidAsync("receiveDataFromBlazor", json ?? "");
    }

    protected override void OnAfterRender(bool firstRender)
    {
      base.OnAfterRender(firstRender);
      if (firstRender)
      {
        CheckRunning();
      }
    }
    bool IsRunning = false;
    private async Task CheckRunning()
    {
      if (IsRunning)
      {
        return;
      }
      IsRunning = true;
      var count = 100;
      for (var i = 0; i < count; i++)
      {
        try
        {
          var result = await jsRunningTime.InvokeAsync<bool>("IsCoCosRunning");
          if (result)
          {
            await SendDataToGame();
            return;
          }
        }
        catch
        {

        }

        await Task.Delay(1000);
      }

    }
  }
}