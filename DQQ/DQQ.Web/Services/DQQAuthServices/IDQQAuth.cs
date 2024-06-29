using ReheeCmf.Commons.DTOs;

namespace DQQ.Web.Services.DQQAuthServices
{
  public interface IDQQAuth
  {
    Task<bool> IsAuth();
		Task<TokenDTO?> GetAuth();
		Task SetAuth(TokenDTO? auth);
  }
}
