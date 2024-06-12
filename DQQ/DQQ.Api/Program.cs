using DQQ.Api;
using DQQ.Components;
using DQQ.Services;

var type = typeof(DQQComponent);
var d = DQQService.IsType;
await ReheeCmfServer.WebStartUp<DQQApiModule>(args);