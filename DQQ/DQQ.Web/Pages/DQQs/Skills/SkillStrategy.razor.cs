using BootstrapBlazor.Components;
using DQQ.Commons.DTOs;
using DQQ.Enums;
using DQQ.Services.SkillServices;
using DQQ.Services.StrategyServices;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DQQ.Web.Pages.DQQs.Skills
{
  public class SkillStrategyPage : DQQPageBase
  {
    [Inject]
    [NotNull]
    public IStrategyService? strategyService { get; set; }
    [Parameter]
    public int? Slot { get; set; }
    [Parameter]
    public Guid? ActorId { get; set; }
    [Parameter]
    public SkillDTO? DTO { get; set; }



    protected override async Task OnParametersSetAsync()
    {
      await base.OnParametersSetAsync();
      
    }

    public async Task Add()
    {
      await Task.CompletedTask;
      DTO?.SkillStrategies?.Add(new DQQ.Strategies.SkillStrategies.SkillStrategy()
      {
        Priority = DTO?.SkillStrategies?.Count() ?? 0
      });
      StateHasChanged();
    }

    public async Task Remove(DQQ.Strategies.SkillStrategies.SkillStrategy s)
    {
      await Task.CompletedTask;
      if (DTO?.SkillStrategies?.Any() != true)
      {
        return;
      }
      var index = DTO?.SkillStrategies.IndexOf(s) ?? -1;
      if (index < 0)
      {
        return;
      }
      DTO?.SkillStrategies.Remove(s);
      var length = DTO?.SkillStrategies?.Count ?? 0;
      for (var i = 0; i < length; i++)
      {
        DTO.SkillStrategies[i].Priority = i;
      }
      StateHasChanged();
    }
    public async Task Up(DQQ.Strategies.SkillStrategies.SkillStrategy s, bool isUp = true)
    {
      await Task.CompletedTask;
      if (DTO?.SkillStrategies?.Any() != true)
      {
        return;
      }
      var index = DTO?.SkillStrategies.IndexOf(s) ?? -1;
      var length = DTO?.SkillStrategies?.Count ?? 0;
      if (isUp)
      {
        if (index <= 0)
        {
          return;
        }
        var preIndex = index - 1;
        var temp = DTO.SkillStrategies[preIndex];
        DTO.SkillStrategies[preIndex] = s;
        DTO.SkillStrategies[index] = temp;
      }
      else
      {
        if (index >= length - 1)
        {
          return;
        }
        var nextIndex = index + 1;
        var temp = DTO.SkillStrategies[nextIndex];
        DTO.SkillStrategies[nextIndex] = s;
        DTO.SkillStrategies[index] = temp;
      }
      for (var i = 0; i < length; i++)
      {
        DTO.SkillStrategies[i].Priority = i;
      }
      StateHasChanged();
    }

    public override async Task<bool> SaveFunction()
    {
      await base.SaveFunction();
      if (Slot == null)
      {
        return false;
      }
      var result = strategyService.SetActorSkillStrategy(ActorId, Slot.Value, DTO?.SkillStrategies);
      return false;
    }
  }
}