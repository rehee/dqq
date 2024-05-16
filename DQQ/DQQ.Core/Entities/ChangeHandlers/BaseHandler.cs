using Microsoft.Extensions.DependencyInjection;
using ReheeCmf.Handlers.EntityChangeHandlers;
using ReheeCmf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Entities.ChangeHandlers
{
  public class BaseHandler<T> : EntityChangeHandler<T> where T : class
  {
    protected IAsyncQuery? query => sp?.GetService<IAsyncQuery>() ?? null;

    protected string? UserId => context?.User?.UserId;
  }
}
