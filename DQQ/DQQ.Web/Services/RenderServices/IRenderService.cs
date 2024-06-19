using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Services.RenderServices
{
	public interface IRenderService
	{
		RenderFragment RenderComponent<T>(Dictionary<string, object?>? parameter);
		RenderFragment RenderComponent(Type type, Dictionary<string, object?>? parameter);
	}
}
