using ReheeCmf.ContextModule.Contexts;
using ReheeCmf.ContextModule.Entities;

namespace DQQ.Api.Data
{
  public class ApplicationDbContext : CmfIdentityContext<ReheeCmfBaseUser>
  {
    public ApplicationDbContext(IServiceProvider sp) : base(sp)
    {
      
    }
  }
}
