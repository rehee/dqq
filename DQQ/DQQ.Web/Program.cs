using DQQ.Consts;
using DQQ.Pools;
using DQQ.Services.ActorServices;
using DQQ.Services.BDServices;
using DQQ.Services.CombatServices;
using DQQ.Services.ItemServices;
using DQQ.Services.SkillServices;
using DQQ.Services.StrategyServices;
using DQQ.Web;
using DQQ.Web.Localizations;
using DQQ.Web.Services.BDServices;
using DQQ.Web.Services.Characters;
using DQQ.Web.Services.CombatServices;
using DQQ.Web.Services.DQQAuthServices;
using DQQ.Web.Services.ItemServices;
using DQQ.Web.Services.LocalizationServices;
using DQQ.Web.Services.Requests;
using DQQ.Web.Services.SkillServices;
using DQQ.Web.Services.StrategyServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Localization;
using ReheeCmf.Requests;
using System.Globalization;
using System.Text.RegularExpressions;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var url = builder.Configuration.GetValue<string>("ApiUrl");
WebConsts.URL = url;
DQQPool.InitPool();



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });
builder.Services.AddScoped<ILocalizationService, LocalizationService>();
builder.Services.AddLocalStorageServices();
builder.Services.AddScoped<IDQQAuth, DQQAuth>();
builder.Services.AddScoped<IGetHttpClient>(sp => new DQQGetHttpClient(url));
builder.Services.AddScoped<IGetRequestTokenService, DQQGetRequestTokenService>();
builder.Services.AddScoped<RequestClient<DQQGetHttpClient>>(sp =>
  new RequestClient<DQQGetHttpClient>(sp.GetService<IGetHttpClient>()!, sp.GetService<IGetRequestTokenService>()!, null));
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ICombatService, CombatService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IStrategyService, StrategyService>();
builder.Services.AddScoped<IBDService, BDService>();

builder.Services.AddScoped(sp =>
{
  var navigationManager = sp.GetRequiredService<NavigationManager>();
  return new YamlLocalizationProvider(navigationManager);
});
builder.Services.AddScoped<IStringLocalizer, YamlStringLocalizer>();

builder.Services.AddBootstrapBlazor();

var host = builder.Build();

var localizationProvider = host.Services.GetRequiredService<YamlLocalizationProvider>();



await localizationProvider.InitializeAsync();




await host.RunAsync();
