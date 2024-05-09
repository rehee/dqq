using ReheeCmf.Commons.DTOs;

namespace DQQ.Web.Services.DQQAuthServices
{
  public interface IDQQAuth
  {
    bool IsAuth();
    TokenDTO? GetAuth();
    void SetAuth(TokenDTO? auth);
  }
}
