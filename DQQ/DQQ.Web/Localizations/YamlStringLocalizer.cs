using Microsoft.Extensions.Localization;
using System.Globalization;

namespace DQQ.Web.Localizations
{
  public class YamlStringLocalizer : IStringLocalizer
  {
    private readonly YamlLocalizationProvider _provider;
    //private readonly string _culture;

    public YamlStringLocalizer(YamlLocalizationProvider provider)
    {
      _provider = provider;
     
    }
    public LocalizedString this[string name]
    {
      get
      {
        var value = _provider.GetResource(CultureInfo.CurrentCulture.Name, name);
        return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
      }
    }

    public LocalizedString this[string name, params object[] arguments] => this[name];

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
      var resources = _provider.GetResources(CultureInfo.CurrentCulture.Name);
      foreach (var resource in resources)
      {
        yield return new LocalizedString(resource.Key, resource.Value);
      }
    }


  }
}
