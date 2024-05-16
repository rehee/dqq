using DQQ.Api.Data;
using DQQ.Api.Services.Characters;
using DQQ.Api.Services.CombatServices;
using DQQ.Api.Services.Itemservices;
using DQQ.Api.Services.SkillServices;
using DQQ.Api.Services.StrategyServices;
using DQQ.Api.Workers;
using DQQ.Components.Stages.Maps;
using DQQ.Pools;
using DQQ.Services.ActorServices;
using DQQ.Services.CombatServices;
using DQQ.Services.ItemServices;
using DQQ.Services.MapServices;
using DQQ.Services.MapServices;
using DQQ.Services.SkillServices;
using DQQ.Services.StrategyServices;
using Google.Api;
using ReheeCmf;
using ReheeCmf.Commons.DTOs;
using ReheeCmf.ContextModule.Entities;
using ReheeCmf.Contexts;
using ReheeCmf.Helpers;
using ReheeCmf.Modules;
using ReheeCmf.Servers.Services;
using ReheeCmf.Services;
using ReheeCmf.Utility.CmfRegisters;

namespace DQQ.Api
{
  public class DQQApiModule : CmfApiModule<ApplicationDbContext, ReheeCmfBaseUser>
  {
    public override string ModuleTitle => "DQQApiModule";

    public override string ModuleName => "DQQApiModule";
    public override bool IsServiceModule => true;

    public override bool WithPermission => true;
    public override async Task<IEnumerable<string>> GetPermissions(IContext? db, TokenDTO? user, CancellationToken ct = default)
    {
      await Task.CompletedTask;
      return new[] { "regular", "admin" };
    }

    public override async Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {

      await base.ConfigureServicesAsync(context);
      DQQPool.InitPool();
      context.Services!.AddHostedService<SeedWorker>();
      context.Services!.AddHostedService<CleanTempWorker>();
      context.Services!.AddScoped<IMapService, MapService<Map>>();
      context.Services!.AddScoped<ITemporaryService, TemporaryService>();
      context.Services!.AddScoped<IItemService, ServerItemService>();
      context.Services!.AddScoped<ICharacterService, CharacterService>();
      context.Services!.AddScoped<ISkillService, SkillService>();
      context.Services!.AddScoped<ICombatService, CombatService>();
      context.Services!.AddScoped<IStrategyService, StrategyService>();
    }
    public override async Task BeforePreApplicationInitializationAsync(ServiceConfigurationContext context)
    {
      //context.App.UseCors("AllowAnyOrigin");
      await base.BeforePreApplicationInitializationAsync(context);
      context.App.UseCors(cors => cors
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()
        );
    }
    public override Task ApplicationInitializationAsync(ServiceConfigurationContext context)
    {

      var app = context.App;
      var env = context.Env;

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
        //app.UseReverseProxyHttpsEnforcer();
      }


      app.UseHttpsRedirection();
      //TODO
      app.CmfUseSwagger(app.Services);




      //缓存http请求为以后优化做准备
      //app.UseMiddleware<CashTestMiddleware>();
      //app.UseMiddleware<TenantCheckMiddleware>();







      return Task.CompletedTask;
    }
  }
}
