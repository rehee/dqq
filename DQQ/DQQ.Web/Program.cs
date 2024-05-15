using DQQ.Pools;
using DQQ.Services.ActorServices;
using DQQ.Services.CombatServices;
using DQQ.Services.ItemServices;
using DQQ.Services.SkillServices;
using DQQ.Web;
using DQQ.Web.Services.Characters;
using DQQ.Web.Services.CombatServices;
using DQQ.Web.Services.DQQAuthServices;
using DQQ.Web.Services.ItemServices;
using DQQ.Web.Services.Requests;
using DQQ.Web.Services.SkillServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReheeCmf.Requests;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var url = builder.Configuration.GetValue<string>("ApiUrl");
DQQPool.InitPool();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });
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

builder.Services.AddBootstrapBlazor();
await builder.Build().RunAsync();
