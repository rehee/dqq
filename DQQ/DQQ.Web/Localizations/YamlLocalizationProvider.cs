using Microsoft.AspNetCore.Components;
using YamlDotNet.Serialization;

namespace DQQ.Web.Localizations
{
  public class YamlLocalizationProvider
  {
    private static Dictionary<string, Dictionary<string, string>> _resources { get; set; } = new Dictionary<string, Dictionary<string, string>>();
    private readonly HttpClient _httpClient;
    public YamlLocalizationProvider(NavigationManager nav)
    {
      _httpClient = new HttpClient { BaseAddress = new Uri(nav.BaseUri) };
    }

    public string GetResource(string culture, string key)
    {
      if (_resources == null || _resources?.ContainsKey(culture) != true)
      {
        return key;
      }
      if (_resources[culture].TryGetValue(key, out var val))
      {
        return val;
      }
      return key;
    }
    public Dictionary<string, string> GetResources(string culture)
    {
      if (_resources == null || _resources?.ContainsKey(culture) != true)
      {
        return [];
      }
      return _resources[culture];
    }
    public async Task InitializeAsync()
    {
      var yamlContent = await _httpClient.GetStringAsync("Resources/general.yml");
      var deserializer = new DeserializerBuilder().Build();
      var resourceData = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(yamlContent);

      foreach (var entry in resourceData)
      {
        foreach (var lang in entry.Value)
        {
          if (!_resources.ContainsKey(lang.Key))
          {
            _resources[lang.Key] = new Dictionary<string, string>();
          }
          _resources[lang.Key][entry.Key] = lang.Value;
        }
      }
    }

  }
}