using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace DQQ.Helper
{
  public static class QueryStringHelper
  {
    public static bool TryGetQueryString<T>(this NavigationManager navManager, string key, out T? value)
    {
      var uri = navManager.ToAbsoluteUri(navManager.Uri);
      string queryString = uri.Query;
      if (String.IsNullOrEmpty(queryString))
      {
        value = default(T);
        return false;
      }
      try
      {
        var queryParameters = HttpUtility.ParseQueryString(queryString);
        var param1Value = queryParameters[key];
        if (typeof(T).IsEnum)
        {
          value = (T)Enum.Parse(typeof(T), param1Value?? "", true);
          return true;
        }
        if (typeof(T) == typeof(int) && int.TryParse(param1Value, out var valueAsInt))
        {
          value = (T)(object)valueAsInt;
          return true;
        }

        if (typeof(T) == typeof(string))
        {
          value = (T)(object)param1Value.ToString();
          return true;
        }

        if (typeof(T) == typeof(decimal) && decimal.TryParse(param1Value, out var valueAsDecimal))
        {
          value = (T)(object)valueAsDecimal;
          return true;
        }
      }
      catch
      {
       
      }

      value = default(T);
      return false;
    }
  }
}
