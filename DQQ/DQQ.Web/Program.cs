using DQQ.Web;
using DQQ.Web.Services.DQQAuthServices;
using DQQ.Web.Services.Requests;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReheeCmf.Requests;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var url = builder.Configuration.GetValue<string>("ApiUrl");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });
builder.Services.AddLocalStorageServices();
builder.Services.AddScoped<IDQQAuth, DQQAuth>();
builder.Services.AddScoped<IGetHttpClient>(sp => new DQQGetHttpClient(url));
builder.Services.AddScoped<IGetRequestTokenService, DQQGetRequestTokenService>();
builder.Services.AddScoped<RequestClient<DQQGetHttpClient>>(sp =>
  new RequestClient<DQQGetHttpClient>(sp.GetService<IGetHttpClient>()!, sp.GetService<IGetRequestTokenService>()!, null));
await builder.Build().RunAsync();
