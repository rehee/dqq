using DQQ.Web.Pages.DQQs.Characters;
using Microsoft.AspNetCore.Components;

namespace DQQ.Web.Services.RenderServices
{
	public class RenderService : IRenderService
	{
		public RenderFragment RenderComponent<T>(Dictionary<string, object?>? parameter)
		{
			return RenderComponent(typeof(T), parameter);
		}
		public RenderFragment RenderComponent(Type type, Dictionary<string, object?>? parameter)
		{
			RenderFragment fragment = builder =>
			{
				builder.OpenComponent(0, type);
				int seq = 1;
				if (parameter != null)
				{
					foreach (var p in parameter)
					{
						builder.AddAttribute(seq++, p.Key, p.Value);
					}
				}
				builder.CloseComponent();
			};
			return fragment;
		}
	}
}
