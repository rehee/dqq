using ReheeCmf.Requests;

namespace DQQ.Web.Services.Requests
{
  public class DQQGetHttpClient : IGetHttpClient
  {
    private readonly string baseUrl;

    public DQQGetHttpClient(string baseUrl)
    {
      this.baseUrl = baseUrl;
    }
    public HttpClient GetClient(string name)
    {
      return new HttpClient { BaseAddress = new Uri(baseUrl) };
    }
  }
}
