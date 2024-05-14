using DQQ.Web.Services.Requests;
using ReheeCmf.Requests;
using System.Diagnostics.CodeAnalysis;

namespace DQQ.Web.Services
{
  public abstract class ClientServiceBase
  {
    [NotNull]
    protected readonly RequestClient<DQQGetHttpClient>? client;

    protected ClientServiceBase(RequestClient<DQQGetHttpClient> client)
    {
      this.client = client;
    }
  }
}
