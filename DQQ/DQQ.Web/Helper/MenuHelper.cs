using BootstrapBlazor.Components;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Components.Stages.Maps;
using DQQ.Enums;
using DQQ.Web.Pages.DQQs;
using DQQ.Web.Pages;
using DQQ.Web.Resources.Chapters.C_0;
using DQQ.Web.Pages.DQQs.Characters;

namespace DQQ.Helper
{
  public static class MenuHelper
  {
    public static IEnumerable<MenuItem> GenerateMenuItem(this Character character,params EnumWebPage[] pages)
    {
      var result = new List<MenuItem>();
      result.AddRange(pages.Where(b => b.PageUnlock(character)).Select(b => b.GetMenuItem()).Where(b => b != null).Select(b => b!));
      return result;
    }


    public static MenuItem? GetMenuItem(this EnumWebPage page)
    {
      var result = new MenuItem();
      result.Url = $"?{nameof(HomePage.WebPage)}={page}";
      switch (page)
      {
        case EnumWebPage.Home:
          result.Text = "首页";
          result.Icon = "fas fa-house-chimney";
          return result;
        case EnumWebPage.Skills:
          result.Text = "技能";
          result.Icon = "fas fa-fire";
          return result;
        case EnumWebPage.Strategy:
          result.Text = "策略";
          result.Icon = "fas fa-bullseye";
          return result;
        case EnumWebPage.Map:
          result.Text = "战斗";
          result.Icon = "fas fa-map";
          return result;
        case EnumWebPage.Inventory:
          result.Text = "物品";
          result.Icon = "fas fa-gem";
          return result;
        case EnumWebPage.Setting:
          result.Text = "设定";
          result.Icon = "fas fa-gear";
          return result;
      }

      return null;
    }


    public static bool PageUnlock(this EnumWebPage page, Character? character)
    {
      switch (page)
      {
        case EnumWebPage.Home: return true;
        case EnumWebPage.Skills: return EnumProgress.SkillManagement.IsUnlocked(character);
        case EnumWebPage.Strategy: return EnumProgress.MainTargetPriority.IsUnlocked(character); ;
        case EnumWebPage.Map: return EnumProgress.CombatProgress.IsUnlocked(character); ;
        case EnumWebPage.Inventory: return EnumProgress.InventoryManagement.IsUnlocked(character); ;
        case EnumWebPage.Setting: return EnumProgress.CombatPlaySetting.IsUnlocked(character); ;
      }

      return false;


    }
  }
}
