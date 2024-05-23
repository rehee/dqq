using BootstrapBlazor.Components;
using DQQ.Enums;

namespace DQQ.Helper
{
  public static class SelectedItemHelper
  {
    public static IEnumerable<SelectedItem>? GetSelectedItem<T>()
    {
      var checkType = typeof(T);
      if (checkType == typeof(bool))
      {
        return GetSelectedItemBool();
      }
      if (checkType.IsEnum)
      {
        var enumValues = (T[])Enum.GetValues(checkType);
        var enumResult = new List<SelectedItem>();
        foreach (var value in enumValues)
        {
          var valueString = $"{value}";
          enumResult.Add(new SelectedItem($"{value}", $"{value.GetStragyEnumString() ?? valueString}"));
        }
        return enumResult;
      }
      return Enumerable.Empty<SelectedItem>();
    }
    public static IEnumerable<SelectedItem>? GetSelectedItemBool()
    {
      return new SelectedItem[]
      {
        new SelectedItem("true","是"),
        new SelectedItem("false","否"),
      };
    }
    public static IEnumerable<SelectedItem>? GetSelectedItemEnum()
    {


      return new SelectedItem[]
      {
        new SelectedItem("true","是"),
        new SelectedItem("false","否"),
      };
    }
  }
}
